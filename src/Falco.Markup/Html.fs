module Falco.Markup.Html

open System
open System.Collections.Generic

type _a(key : string, ?value : string) =
    member _.value =
        match value with
        | Some v -> Attr.create key v
        | None -> Attr.createBool key

type attr([<ParamArray>]attrs : _a array) =
    static member empty = attr()
    member internal _.attributes = attrs |> Seq.map _.value

let private selfClosingTags : HashSet<string> =
    HashSet<string>(seq { "base";"link";"meta";"hr";"br";"wbr";"img";"track";"embed";"source";"col";"input" })

[<AbstractClass>]
type HtmlNode =
    val value: XmlNode
    new (node) = { value = node }

type text =
    inherit HtmlNode
    new (str : string) = { inherit HtmlNode(TextNode str) }

type elem =
    inherit HtmlNode
    new (tag : string, attrs : attr, [<ParamArray>] children : HtmlNode array) =
        let a = attrs.attributes |> List.ofSeq
        let c = children |> Seq.map _.value |> List.ofSeq
        let node =
            match selfClosingTags.Contains tag with
            | false -> Elem.create tag a c
            | true -> Elem.createSelfClosing tag a
        { inherit HtmlNode(node) }
    new (tag : string) = elem(tag, attr.empty, Array.empty)
    new (tag : string, [<ParamArray>] attrs : _a array) = elem(tag, attr(attrs), Array.empty)
    new (tag : string, [<ParamArray>] c : HtmlNode array) = elem(tag, attr.empty, c)

[<AutoOpen>]
module HtmlElem =
    type base'([<ParamArray>]attrs : _a array) = inherit elem("base", attr(attrs), Array.empty)
    type link([<ParamArray>]attrs : _a array) = inherit elem("link", attr(attrs), Array.empty)
    type meta([<ParamArray>]attrs : _a array) = inherit elem("meta", attr(attrs), Array.empty)
    type hr([<ParamArray>]attrs : _a array) = inherit elem("hr", attr(attrs), Array.empty)
    type br([<ParamArray>]attrs : _a array) = inherit elem("br", attr(attrs), Array.empty)
    type wbr([<ParamArray>]attrs : _a array) = inherit elem("wbr", attr(attrs), Array.empty)
    type img([<ParamArray>]attrs : _a array) = inherit elem("img", attr(attrs), Array.empty)
    type track([<ParamArray>]attrs : _a array) = inherit elem("track", attr(attrs), Array.empty)
    type embed([<ParamArray>]attrs : _a array) = inherit elem("embed", attr(attrs), Array.empty)
    type source([<ParamArray>]attrs : _a array) = inherit elem("source", attr(attrs), Array.empty)
    type col([<ParamArray>]attrs : _a array) = inherit elem("col", attr(attrs), Array.empty)
    type input([<ParamArray>]attrs : _a array) = inherit elem("input", attr(attrs), Array.empty)

    type html =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("html", attr, children) }
        new ([<ParamArray>] children) = html(attr.empty, children)

    type head =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("head", attr, children) }
        new ([<ParamArray>] children) = head(attr.empty, children)

    type style =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("style", attr, children) }
        new ([<ParamArray>] children) = style(attr.empty, children)

    type title =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("title", attr, children) }
        new ([<ParamArray>] children) = title(attr.empty, children)

    type body =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("body", attr, children) }
        new ([<ParamArray>] children) = body(attr.empty, children)

    type address =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("address", attr, children) }
        new ([<ParamArray>] children) = address(attr.empty, children)

    type article =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("article", attr, children) }
        new ([<ParamArray>] children) = article(attr.empty, children)

    type aside =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("aside", attr, children) }
        new ([<ParamArray>] children) = aside(attr.empty, children)

    type footer =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("footer", attr, children) }
        new ([<ParamArray>] children) = footer(attr.empty, children)

    type header =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("header", attr, children) }
        new ([<ParamArray>] children) = header(attr.empty, children)

    type h1 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h1", attr, children) }
        new ([<ParamArray>] children) = h1(attr.empty, children)

    type h2 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h2", attr, children) }
        new ([<ParamArray>] children) = h2(attr.empty, children)

    type h3 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h3", attr, children) }
        new ([<ParamArray>] children) = h3(attr.empty, children)

    type h4 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h4", attr, children) }
        new ([<ParamArray>] children) = h4(attr.empty, children)

    type h5 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h5", attr, children) }
        new ([<ParamArray>] children) = h5(attr.empty, children)

    type h6 =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("h6", attr, children) }
        new ([<ParamArray>] children) = h6(attr.empty, children)

    type main =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("main", attr, children) }
        new ([<ParamArray>] children) = main(attr.empty, children)

    type nav =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("nav", attr, children) }
        new ([<ParamArray>] children) = nav(attr.empty, children)

    type section =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("section", attr, children) }
        new ([<ParamArray>] children) = section(attr.empty, children)

    type blockquote =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("blockquote", attr, children) }
        new ([<ParamArray>] children) = blockquote(attr.empty, children)

    type dd =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("dd", attr, children) }
        new ([<ParamArray>] children) = dd(attr.empty, children)

    type div =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("div", attr, children) }
        new ([<ParamArray>] children) = div(attr.empty, children)

    type dl =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("dl", attr, children) }
        new ([<ParamArray>] children) = dl(attr.empty, children)

    type dt =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("dt", attr, children) }
        new ([<ParamArray>] children) = dt(attr.empty, children)

    type figcaption =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("figcaption", attr, children) }
        new ([<ParamArray>] children) = figcaption(attr.empty, children)

    type figure =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("figure", attr, children) }
        new ([<ParamArray>] children) = figure(attr.empty, children)

    type li =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("li", attr, children) }
        new ([<ParamArray>] children) = li(attr.empty, children)

    type menu =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("menu", attr, children) }
        new ([<ParamArray>] children) = menu(attr.empty, children)

    type ol =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("ol", attr, children) }
        new ([<ParamArray>] children) = ol(attr.empty, children)

    type p =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("p", attr, children) }
        new ([<ParamArray>] children) = p(attr.empty, children)

    type pre =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("pre", attr, children) }
        new ([<ParamArray>] children) = pre(attr.empty, children)

    type ul =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("ul", attr, children) }
        new ([<ParamArray>] children) = ul(attr.empty, children)

    type a =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("a", attr, children) }
        new ([<ParamArray>] children) = a(attr.empty, children)

    type abbr =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("abbr", attr, children) }
        new ([<ParamArray>] children) = abbr(attr.empty, children)

    type b =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("b", attr, children) }
        new ([<ParamArray>] children) = b(attr.empty, children)

    type bdi =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("bdi", attr, children) }
        new ([<ParamArray>] children) = bdi(attr.empty, children)

    type bdo =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("bdo", attr, children) }
        new ([<ParamArray>] children) = bdo(attr.empty, children)

    type cite =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("cite", attr, children) }
        new ([<ParamArray>] children) = cite(attr.empty, children)

    type code =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("code", attr, children) }
        new ([<ParamArray>] children) = code(attr.empty, children)

    type data =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("data", attr, children) }
        new ([<ParamArray>] children) = data(attr.empty, children)

    type dfn =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("dfn", attr, children) }
        new ([<ParamArray>] children) = dfn(attr.empty, children)

    type em =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("em", attr, children) }
        new ([<ParamArray>] children) = em(attr.empty, children)

    type i =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("i", attr, children) }
        new ([<ParamArray>] children) = i(attr.empty, children)

    type kbd =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("kbd", attr, children) }
        new ([<ParamArray>] children) = kbd(attr.empty, children)

    type mark =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("mark", attr, children) }
        new ([<ParamArray>] children) = mark(attr.empty, children)

    type q =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("q", attr, children) }
        new ([<ParamArray>] children) = q(attr.empty, children)

    type rp =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("rp", attr, children) }
        new ([<ParamArray>] children) = rp(attr.empty, children)

    type rt =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("rt", attr, children) }
        new ([<ParamArray>] children) = rt(attr.empty, children)

    type ruby =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("ruby", attr, children) }
        new ([<ParamArray>] children) = ruby(attr.empty, children)

    type s =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("s", attr, children) }
        new ([<ParamArray>] children) = s(attr.empty, children)

    type samp =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("samp", attr, children) }
        new ([<ParamArray>] children) = samp(attr.empty, children)

    type small =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("small", attr, children) }
        new ([<ParamArray>] children) = small(attr.empty, children)

    type span =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("span", attr, children) }
        new ([<ParamArray>] children) = span(attr.empty, children)

    type strong =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("strong", attr, children) }
        new ([<ParamArray>] children) = strong(attr.empty, children)

    type sub =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("sub", attr, children) }
        new ([<ParamArray>] children) = sub(attr.empty, children)

    type sup =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("sup", attr, children) }
        new ([<ParamArray>] children) = sup(attr.empty, children)

    type time =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("time", attr, children) }
        new ([<ParamArray>] children) = time(attr.empty, children)

    type u =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("u", attr, children) }
        new ([<ParamArray>] children) = u(attr.empty, children)

    type var =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("var", attr, children) }
        new ([<ParamArray>] children) = var(attr.empty, children)

    type area =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("area", attr, children) }
        new ([<ParamArray>] children) = area(attr.empty, children)

    type audio =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("audio", attr, children) }
        new ([<ParamArray>] children) = audio(attr.empty, children)

    type map =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("map", attr, children) }
        new ([<ParamArray>] children) = map(attr.empty, children)

    type video =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("video", attr, children) }
        new ([<ParamArray>] children) = video(attr.empty, children)

    type iframe =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("iframe", attr, children) }
        new ([<ParamArray>] children) = iframe(attr.empty, children)

    type object =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("object", attr, children) }
        new ([<ParamArray>] children) = object(attr.empty, children)

    type picture =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("picture", attr, children) }
        new ([<ParamArray>] children) = picture(attr.empty, children)

    type portal =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("portal", attr, children) }
        new ([<ParamArray>] children) = portal(attr.empty, children)

    type canvas =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("canvas", attr, children) }
        new ([<ParamArray>] children) = canvas(attr.empty, children)

    type noscript =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("noscript", attr, children) }
        new ([<ParamArray>] children) = noscript(attr.empty, children)

    type script =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("script", attr, children) }
        new ([<ParamArray>] children) = script(attr.empty, children)

    type del =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("del", attr, children) }
        new ([<ParamArray>] children) = del(attr.empty, children)

    type ins =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("ins", attr, children) }
        new ([<ParamArray>] children) = ins(attr.empty, children)

    type caption =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("caption", attr, children) }
        new ([<ParamArray>] children) = caption(attr.empty, children)

    type colgroup =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("colgroup", attr, children) }
        new ([<ParamArray>] children) = colgroup(attr.empty, children)

    type table =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("table", attr, children) }
        new ([<ParamArray>] children) = table(attr.empty, children)

    type tbody =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("tbody", attr, children) }
        new ([<ParamArray>] children) = tbody(attr.empty, children)

    type td =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("td", attr, children) }
        new ([<ParamArray>] children) = td(attr.empty, children)

    type tfoot =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("tfoot", attr, children) }
        new ([<ParamArray>] children) = tfoot(attr.empty, children)

    type th =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("th", attr, children) }
        new ([<ParamArray>] children) = th(attr.empty, children)

    type thead =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("thead", attr, children) }
        new ([<ParamArray>] children) = thead(attr.empty, children)

    type tr =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("tr", attr, children) }
        new ([<ParamArray>] children) = tr(attr.empty, children)

    type button =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("button", attr, children) }
        new ([<ParamArray>] children) = button(attr.empty, children)

    type datalist =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("datalist", attr, children) }
        new ([<ParamArray>] children) = datalist(attr.empty, children)

    type fieldset =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("fieldset", attr, children) }
        new ([<ParamArray>] children) = fieldset(attr.empty, children)

    type form =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("form", attr, children) }
        new ([<ParamArray>] children) = form(attr.empty, children)

    type label =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("label", attr, children) }
        new ([<ParamArray>] children) = label(attr.empty, children)

    type legend =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("legend", attr, children) }
        new ([<ParamArray>] children) = legend(attr.empty, children)

    type meter =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("meter", attr, children) }
        new ([<ParamArray>] children) = meter(attr.empty, children)

    type optgroup =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("optgroup", attr, children) }
        new ([<ParamArray>] children) = optgroup(attr.empty, children)

    type option =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("option", attr, children) }
        new ([<ParamArray>] children) = option(attr.empty, children)

    type output =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("output", attr, children) }
        new ([<ParamArray>] children) = output(attr.empty, children)

    type progress =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("progress", attr, children) }
        new ([<ParamArray>] children) = progress(attr.empty, children)

    type select =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("select", attr, children) }
        new ([<ParamArray>] children) = select(attr.empty, children)

    type textarea =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("textarea", attr, children) }
        new ([<ParamArray>] children) = textarea(attr.empty, children)

    type details =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("details", attr, children) }
        new ([<ParamArray>] children) = details(attr.empty, children)

    type dialog =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("dialog", attr, children) }
        new ([<ParamArray>] children) = dialog(attr.empty, children)

    type summary =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("summary", attr, children) }
        new ([<ParamArray>] children) = summary(attr.empty, children)

    type slot =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("slot", attr, children) }
        new ([<ParamArray>] children) = slot(attr.empty, children)

    type template =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("template", attr, children) }
        new ([<ParamArray>] children) = template(attr.empty, children)

    type math =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("math", attr, children) }
        new ([<ParamArray>] children) = math(attr.empty, children)

    type svg =
        inherit elem
        new (attr, [<ParamArray>] children) = { inherit elem("svg", attr, children) }
        new ([<ParamArray>] children) = svg(attr.empty, children)


[<AutoOpen>]
module HtmlAttr =
    let _async = _a("async")
    let _autofocus = _a("autofocus")
    let _autoplay = _a("autoplay")
    let _checked = _a("checked")
    let _controls = _a("controls")
    let _default = _a("default")
    let _defer = _a("defer")
    let _disabled = _a("disabled")
    let _formnovalidate = _a("formnovalidate")
    let _hidden = _a("hidden")
    let _ismap = _a("ismap")
    let _loop = _a("loop")
    let _multiple = _a("multiple")
    let _muted = _a("muted")
    let _novalidate = _a("novalidate")
    let _readonly = _a("readonly")
    let _required = _a("required")
    let _reversed = _a("reversed")
    let _selected = _a("selected")

    let _accept value = _a("accept", value)
    let _acceptCharset value = _a("accept-charset", value)
    let _accesskey value = _a("accesskey", value)
    let _action value = _a("action", value)
    let _align value = _a("align", value)
    let _allow value = _a("allow", value)
    let _alt value = _a("alt", value)
    let _autocapitalize value = _a("autocapitalize", value)
    let _autocomplete value = _a("autocomplete", value)
    let _background value = _a("background", value)
    let _bgcolor value = _a("bgcolor", value)
    let _border value = _a("border", value)
    let _buffered value = _a("buffered", value)
    let _capture value = _a("capture", value)
    let _challenge value = _a("challenge", value)
    let _charset value = _a("charset", value)
    let _cite value = _a("cite", value)
    let _class value = _a("class", value)
    let _code value = _a("code", value)
    let _codebase value = _a("codebase", value)
    let _color value = _a("color", value)
    let _cols value = _a("cols", value)
    let _colspan value = _a("colspan", value)
    let _content value = _a("content", value)
    let _contenteditable value = _a("contenteditable", value)
    let _contextmenu value = _a("contextmenu", value)
    let _coords value = _a("coords", value)
    let _crossorigin value = _a("crossorigin", value)
    let _csp value = _a("csp", value)
    let _data value = _a("data", value)
    let _datetime value = _a("datetime", value)
    let _decoding value = _a("decoding", value)
    let _dir value = _a("dir", value)
    let _dirname value = _a("dirname", value)
    let _download value = _a("download", value)
    let _draggable value = _a("draggable", value)
    let _enctype value = _a("enctype", value)
    let _enterkeyhint value = _a("enterkeyhint", value)
    let _for value = _a("for", value)
    let _form value = _a("form", value)
    let _formaction value = _a("formaction", value)
    let _formenctype value = _a("formenctype", value)
    let _formmethod value = _a("formmethod", value)
    let _formtarget value = _a("formtarget", value)
    let _headers value = _a("headers", value)
    let _height value = _a("height", value)
    let _high value = _a("high", value)
    let _href value = _a("href", value)
    let _hreflang value = _a("hreflang", value)
    let _httpEquiv value = _a("http-equiv", value)
    let _icon value = _a("icon", value)
    let _id value = _a("id", value)
    let _importance value = _a("importance", value)
    let _integrity value = _a("integrity", value)
    let _inputmode value = _a("inputmode", value)
    let _itemprop value = _a("itemprop", value)
    let _keytype value = _a("keytype", value)
    let _kind value = _a("kind", value)
    let _label value = _a("label", value)
    let _lang value = _a("lang", value)
    let _loading value = _a("loading", value)
    let _list value = _a("list", value)
    let _low value = _a("low", value)
    let _max value = _a("max", value)
    let _maxlength value = _a("maxlength", value)
    let _minlength value = _a("minlength", value)
    let _media value = _a("media", value)
    let _method value = _a("method", value)
    let _min value = _a("min", value)
    let _name value = _a("name", value)
    let _open value = _a("open", value)
    let _optimum value = _a("optimum", value)
    let _pattern value = _a("pattern", value)
    let _ping value = _a("ping", value)
    let _placeholder value = _a("placeholder", value)
    let _poster value = _a("poster", value)
    let _preload value = _a("preload", value)
    let _radiogroup value = _a("radiogroup", value)
    let _referrerpolicy value = _a("referrerpolicy", value)
    let _rel value = _a("rel", value)
    let _role value = _a("role", value)
    let _rows value = _a("rows", value)
    let _rowspan value = _a("rowspan", value)
    let _sandbox value = _a("sandbox", value)
    let _scope value = _a("scope", value)
    let _shape value = _a("shape", value)
    let _size value = _a("size", value)
    let _sizes value = _a("sizes", value)
    let _slot value = _a("slot", value)
    let _span value = _a("span", value)
    let _spellcheck value = _a("spellcheck", value)
    let _src value = _a("src", value)
    let _srcdoc value = _a("srcdoc", value)
    let _srclang value = _a("srclang", value)
    let _srcset value = _a("srcset", value)
    let _start value = _a("start", value)
    let _step value = _a("step", value)
    let _style value = _a("style", value)
    let _tabindex value = _a("tabindex", value)
    let _target value = _a("target", value)
    let _title value = _a("title", value)
    let _translate value = _a("translate", value)
    let _type value = _a("type", value)
    let _usemap value = _a("usemap", value)
    let _value value = _a("value", value)
    let _width value = _a("width", value)
    let _wrap value = _a("wrap", value)