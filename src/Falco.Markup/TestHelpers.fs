namespace Falco.Markup

open System
open System.Globalization
open System.IO
open Falco.Markup.XmlNodeSerializer

module TestHelpers =
    module internal XmlNodeSerializer =
        let [<Literal>] private _ampersand = '&'

        let [<Literal>] private _inputTag = "input"
        let [<Literal>] private _textareaTag = "textarea"
        let [<Literal>] private _selectTag = "select"
        let [<Literal>] private _optionTag = "option"
        let [<Literal>] private _optgroupTag = "optgroup"

        let [<Literal>] private _typeAttr = "type"
        let [<Literal>] private _nameAttr = "name"
        let [<Literal>] private _valueAttr = "value"
        let [<Literal>] private _checkedAttr = "checked"
        let [<Literal>] private _selectedAttr = "selected"

        let [<Literal>] private _typeAttrCheckbox = "checkbox"
        let [<Literal>] private _typeAttrRadio = "radio"

        let private _formTags = [|"input";"select";"textarea"|]

        type XmlAttribute with
            static member Name x =
                match x with
                | NonValueAttr attrName -> attrName
                | KeyValueAttr (attrName, _) -> attrName

            static member Value x =
                match x with
                | NonValueAttr _ -> String.Empty
                | KeyValueAttr (_, attrValue) -> attrValue

        let inline strEquals (equals : string) (str : string) =
            String.Equals(str, equals, StringComparison.OrdinalIgnoreCase)

        let serializeNameValues (w : StringWriter, xml : XmlNode) =
            let getType attrs =
                attrs
                |> List.tryFind (XmlAttribute.Name >> strEquals _typeAttr)
                |> Option.map XmlAttribute.Value
                |> Option.defaultValue "text"

            let getName attrs =
                attrs
                |> List.tryFind (XmlAttribute.Name >> strEquals _nameAttr)
                |> Option.map XmlAttribute.Value
                // |> Option.iter (fun attr -> w.Write (XmlAttribute.Value attr))

            let getValue attrs =
                attrs
                |> List.tryFind (XmlAttribute.Name >> strEquals _valueAttr)
                |> Option.map XmlAttribute.Value
                // |> Option.iter (fun attr -> w.Write (XmlAttribute.Value attr))

            let isChecked attrs =
                attrs
                |> List.exists (XmlAttribute.Name >> strEquals _checkedAttr)

            let isSelected attrs =
                attrs
                |> List.exists (XmlAttribute.Name >> strEquals _selectedAttr)

            let writeNameValue (name : string option, value : string option) =
                match name with
                | None -> ()
                | Some name' ->
                    w.Write name'
                    w.Write _equals

                    match value with
                    | Some value' -> w.Write value'
                    | None -> ()

                    w.Write _ampersand

            let rec buildNameValues tag =
                match tag with
                // <input>
                | SelfClosingNode (tag, attrs) when strEquals _inputTag tag ->
                    let name = getName attrs
                    let type' = getType attrs

                    if (strEquals type' _typeAttrCheckbox || strEquals type' _typeAttrRadio) then
                        if isChecked attrs then
                            writeNameValue (name, getValue attrs)
                    else
                        writeNameValue (name, getValue attrs)

                // <textarea></textarea>
                | ParentNode ((tag, attrs), children) when strEquals _textareaTag tag ->
                    let name = getName attrs
                    let value =
                        match List.tryHead children with
                        | Some (TextNode text) -> Some text
                        | Some _
                        | None -> None

                    writeNameValue (name, value)

                // <select></select>
                | ParentNode ((tag, attrs), children) when strEquals _selectTag tag ->
                    let name = getName attrs

                    let selected =
                        [ for c in children do
                            match c with
                            // <optgroup></optgroup>
                            | ParentNode ((tag, attrs), grandchildren) when strEquals _optgroupTag tag ->
                                for gc in grandchildren do
                                    match gc with
                                    // <option></option>
                                    | ParentNode ((tag, attrs), _) when strEquals _optionTag tag && isSelected attrs ->
                                        name, (getValue attrs)

                                    | ParentNode _
                                    | SelfClosingNode _
                                    | TextNode _ -> ()

                            // <option></option>
                            | ParentNode ((tag, attrs), _) when strEquals _optionTag tag && isSelected attrs ->
                                name, (getValue attrs)

                            | ParentNode _
                            | SelfClosingNode _
                            | TextNode _ -> () ]

                    if selected.Length = 0 then
                        writeNameValue (name, None)
                    else
                        for (name, value) in selected do
                            writeNameValue (name, value)

                | ParentNode (_, children) ->
                    for c in children do
                        buildNameValues c

                | SelfClosingNode _
                | TextNode _ -> ()

            buildNameValues xml

            let nameValues = StringBuilderCache.GetString(w.GetStringBuilder())

            if nameValues.Length > 0 && nameValues[nameValues.Length - 1] = '&' then
                nameValues.Substring(0, nameValues.Length - 1)
            else
                nameValues

    /// Render XmlNode recursively to name/value representation, useful for
    /// testing Falco query and form mappings
    let renderNameValues (tag : XmlNode) =
        let sb = StringBuilderCache.Acquire()
        let w = new StringWriter(sb, CultureInfo.InvariantCulture)
        XmlNodeSerializer.serializeNameValues(w, tag)
