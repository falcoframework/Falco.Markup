namespace Falco.Markup

open System
open System.Globalization
open System.IO
open System.Text

/// Specifies an XML-style attribute
type XmlAttribute =
    | KeyValueAttr of string * string
    | NonValueAttr of string

/// Represents an XML-style element containing attributes
type XmlElement =
    string * XmlAttribute list

/// Describes the different XML-style node patterns
type XmlNode =
    | TextNode        of string
    | SelfClosingNode of XmlElement
    | ParentNode      of XmlElement * XmlNode list

module internal XmlNodeSerializer =
    let [<Literal>] _openChar = '<'
    let [<Literal>] _closeChar = '>'
    let [<Literal>] _term = '/'
    let [<Literal>] _space = ' '
    let [<Literal>] _equals = '='
    let [<Literal>] _quote = '"'

    let private writeAttributes (w : TextWriter) (attrs : XmlAttribute list) =
        let mutable rest = attrs
        while not rest.IsEmpty do
            w.Write _space

            match rest.Head with
            | NonValueAttr attrName ->
                w.Write attrName

            | KeyValueAttr (attrName, attrValue) ->
                w.Write attrName
                w.Write _equals
                w.Write _quote
                w.Write attrValue
                w.Write _quote

            rest <- rest.Tail

    let rec private writeNode (w : TextWriter) (node : XmlNode) =
        match node with
        | TextNode text ->
            w.Write text

        | SelfClosingNode (tag, attrs) ->
            w.Write _openChar
            w.Write tag
            writeAttributes w attrs
            w.Write _space
            w.Write _term
            w.Write _closeChar

        | ParentNode ((tag, attrs), children) ->
            w.Write _openChar
            w.Write tag
            writeAttributes w attrs
            w.Write _closeChar

            let mutable rest = children
            while not rest.IsEmpty do
                writeNode w rest.Head
                rest <- rest.Tail

            w.Write _openChar
            w.Write _term
            w.Write tag
            w.Write _closeChar

    let serialize (w : TextWriter, xml : XmlNode) =
        writeNode w xml

module internal XmlNodeStringSerializer =
    let private writeAttributes (sb : StringBuilder) (attrs : XmlAttribute list) =
        let mutable rest = attrs
        while not rest.IsEmpty do
            sb.Append(' ') |> ignore

            match rest.Head with
            | NonValueAttr attrName ->
                sb.Append(attrName) |> ignore
            | KeyValueAttr (attrName, attrValue) ->
                sb.Append(attrName).Append('=').Append('"').Append(attrValue).Append('"') |> ignore

            rest <- rest.Tail

    let rec private writeNode (sb : StringBuilder) (node : XmlNode) =
        match node with
        | TextNode text ->
            sb.Append(text) |> ignore
        | SelfClosingNode (tag, attrs) ->
            sb.Append('<').Append(tag) |> ignore
            writeAttributes sb attrs
            sb.Append(" />") |> ignore
        | ParentNode ((tag, attrs), children) ->
            sb.Append('<').Append(tag) |> ignore
            writeAttributes sb attrs
            sb.Append('>') |> ignore

            let mutable rest = children
            while not rest.IsEmpty do
                writeNode sb rest.Head
                rest <- rest.Tail

            sb.Append("</").Append(tag).Append('>') |> ignore

    let serialize (sb : StringBuilder, xml : XmlNode) =
        writeNode sb xml

/// Caches one StringWriter (and its StringBuilder) per thread
[<AbstractClass; Sealed>]
type internal StringWriterCache internal() =
    // Larger writers are dropped so a single huge page doesn't pin memory per thread
    [<Literal>]
    static let _maxCapacity = 65536

    [<ThreadStatic; DefaultValue>]
    static val mutable private cachedInstance : StringWriter

    static member Acquire () =
        let w = StringWriterCache.cachedInstance
        if isNull w then
            new StringWriter(CultureInfo.InvariantCulture)
        else
            StringWriterCache.cachedInstance <- null
            w.GetStringBuilder().Clear() |> ignore
            w

    static member GetString (w : StringWriter) =
        let sb = w.GetStringBuilder()
        let result = sb.ToString()
        if sb.Capacity <= _maxCapacity then StringWriterCache.cachedInstance <- w
        result

[<AutoOpen>]
module XmlNodeRenderer =
    let private renderTo (w : TextWriter) (tag : XmlNode) (header : string) =
        if not (isNull header) then w.Write header
        XmlNodeSerializer.serialize(w, tag)

    let private render (tag : XmlNode) (header : string) =
        let w = StringWriterCache.Acquire()
        let sb = w.GetStringBuilder()
        if not (isNull header) then sb.Append(header) |> ignore
        XmlNodeStringSerializer.serialize(sb, tag)
        StringWriterCache.GetString w

    [<Literal>]
    let private _htmlHeader = "<!DOCTYPE html>"

    [<Literal>]
    let private _xmlHeader = "<?xml version=\"1.0\" encoding=\"UTF-8\"?>"

    /// Render XmlNode as string
    let renderNode (tag : XmlNode) =
        render tag null

    /// Render XmlNode as HTML string
    let renderHtml (tag : XmlNode) =
        render tag _htmlHeader

    /// Render XmlNode as XML string
    let renderXml (tag : XmlNode) =
        render tag _xmlHeader

    /// Render XmlNode directly to a TextWriter (e.g., a response stream writer)
    let renderNodeTo (writer : TextWriter) (tag : XmlNode) =
        renderTo writer tag null

    /// Render XmlNode as HTML directly to a TextWriter
    let renderHtmlTo (writer : TextWriter) (tag : XmlNode) =
        renderTo writer tag _htmlHeader

    /// Render XmlNode as XML directly to a TextWriter
    let renderXmlTo (writer : TextWriter) (tag : XmlNode) =
        renderTo writer tag _xmlHeader

    /// Render a fragment of XmlNode by id as string
    let renderFragment (tag : XmlNode) (id : string) =
        let isIdMatch attr =
            match attr with
            | KeyValueAttr ("id", v) when v = id -> true
            | _ -> false

        let rec findId tag =
            match tag with
            | TextNode _ ->
                None

            | SelfClosingNode ((_, attrs)) ->
                attrs
                |> List.tryFind isIdMatch
                |> Option.map (fun _ -> tag)

            | ParentNode ((_, attrs), children) ->
                attrs
                |> List.tryFind isIdMatch
                |> Option.map (fun _ -> tag)
                |> Option.orElse (children |> List.tryPick findId)

        match findId tag with
        | Some node -> render node null
        | None      -> String.Empty
