namespace Falco.Markup

open System
open System.Net

module Text =
    /// Empty Text node
    let empty =
        TextNode String.Empty

    /// Encoded-text XmlNode constructor
    let enc (content : string) =
        TextNode (WebUtility.HtmlEncode content)

    /// Text XmlNode constructor
    let raw (content : string) =
        TextNode content

    /// Text XmlNode constructor that will invoke "sprintf template"
    let rawf (template : Printf.StringFormat<'T, XmlNode>) =
        Printf.kprintf raw template

    /// HTML Comment Text XmlNode construction
    let comment (content : string) =
        rawf "<!-- %s -->" content

    /// Shortcut for `<h1>` element (i.e., `Elem.h1 [] [ Text.raw content]`)
    let h1 content = Elem.h1 [] [ raw content ]

    /// Shortcut for `<h2>` element (i.e., `Elem.h2 [] [ Text.raw content]`)
    let h2 content = Elem.h2 [] [ raw content ]

    /// Shortcut for `<h3>` element (i.e., `Elem.h3 [] [ Text.raw content]`)
    let h3 content = Elem.h3 [] [ raw content ]

    /// Shortcut for `<h4>` element (i.e., `Elem.h4 [] [ Text.raw content]`)
    let h4 content = Elem.h4 [] [ raw content ]

    /// Shortcut for `<h5>` element (i.e., `Elem.h5 [] [ Text.raw content]`)
    let h5 content = Elem.h5 [] [ raw content ]

    /// Shortcut for `<h6>` element (i.e., `Elem.h6 [] [ Text.raw content]`)
    let h6 content = Elem.h6 [] [ raw content ]

    /// Shortcut for `<p>` element (i.e., `Elem.p [] [ Text.raw content]`)
    let p content = Elem.p [] [ raw content ]

    /// Shortcut for `<dd>` element (i.e., `Elem.dd [] [ Text.raw content]`)
    let dd content = Elem.dd [] [ raw content ]

    /// Shortcut for `<dt>` element (i.e., `Elem.dt [] [ Text.raw content]`)
    let dt content = Elem.dt [] [ raw content ]

    /// Shortcut for `<abbr>` element
    let abbr content = Elem.abbr [] [ raw content ]

    /// Shortcut for `<b>` element. Alias for `Elem.b [] [ Text.raw content ]`.
    let b content = Elem.b [] [ raw content ]

    /// Shortcut for `<bdi>` element. Alias for `Elem.bdi [] [ Text.raw content ]`.
    let bdi content = Elem.bdi [] [ raw content ]

    /// Shortcut for `<bdo>` element. Alias for `Elem.bdo [] [ Text.raw content ]`.
    let bdo content = Elem.bdo [] [ raw content ]

    /// Shortcut for `<cite>` element. Alias for `Elem.cite [] [ Text.raw content ]`.
    let cite content = Elem.cite [] [ raw content ]

    /// Shortcut for `<code>` element. Alias for `Elem.code [] [ Text.raw content ]`.
    let code content = Elem.code [] [ raw content ]

    /// Shortcut for `<data>` element. Alias for `Elem.data [] [ Text.raw content ]`.
    let data content = Elem.data [] [ raw content ]

    /// Shortcut for `<dfn>` element. Alias for `Elem.dfn [] [ Text.raw content ]`.
    let dfn content = Elem.dfn [] [ raw content ]

    /// Shortcut for `<em>` element. Alias for `Elem.em [] [ Text.raw content ]`.
    let em content = Elem.em [] [ raw content ]

    /// Shortcut for `<i>` element. Alias for `Elem.i [] [ Text.raw content ]`.
    let i content = Elem.i [] [ raw content ]

    /// Shortcut for `<kbd>` element. Alias for `Elem.kbd [] [ Text.raw content ]`.
    let kbd content = Elem.kbd [] [ raw content ]

    /// Shortcut for `<mark>` element. Alias for `Elem.mark [] [ Text.raw content ]`.
    let mark content = Elem.mark [] [ raw content ]

    /// Shortcut for `<q>` element. Alias for `Elem.q [] [ Text.raw content ]`.
    let q content = Elem.q [] [ raw content ]

    /// Shortcut for `<rp>` element. Alias for `Elem.rp [] [ Text.raw content ]`.
    let rp content = Elem.rp [] [ raw content ]

    /// Shortcut for `<rt>` element. Alias for `Elem.rt [] [ Text.raw content ]`.
    let rt content = Elem.rt [] [ raw content ]

    /// Shortcut for `<ruby>` element. Alias for `Elem.ruby [] [ Text.raw content ]`.
    let ruby content = Elem.ruby [] [ raw content ]

    /// Shortcut for `<s>` element. Alias for `Elem.s [] [ Text.raw content ]`.
    let s content = Elem.s [] [ raw content ]

    /// Shortcut for `<samp>` element. Alias for `Elem.samp [] [ Text.raw content ]`.
    let samp content = Elem.samp [] [ raw content ]

    /// Shortcut for `<small>` element. Alias for `Elem.small [] [ Text.raw content ]`.
    let small content = Elem.small [] [ raw content ]

    /// Shortcut for `<span>` element. Alias for `Elem.span [] [ Text.raw content ]`.
    let span content = Elem.span [] [ raw content ]

    /// Shortcut for `<strong>` element. Alias for `Elem.strong [] [ Text.raw content ]`.
    let strong content = Elem.strong [] [ raw content ]

    /// Shortcut for `<sub>` element. Alias for `Elem.sub [] [ Text.raw content ]`.
    let sub content = Elem.sub [] [ raw content ]

    /// Shortcut for `<sup>` element. Alias for `Elem.sup [] [ Text.raw content ]`.
    let sup content = Elem.sup [] [ raw content ]

    /// Shortcut for `<time>` element. Alias for `Elem.time [] [ Text.raw content ]`.
    let time content = Elem.time [] [ raw content ]

    /// Shortcut for `<u>` element. Alias for `Elem.u [] [ Text.raw content ]`.
    let u content = Elem.u [] [ raw content ]

    /// Shortcut for `<var>` element. Alias for `Elem.var [] [ Text.raw content ]`.
    let var content = Elem.var [] [ raw content ]
