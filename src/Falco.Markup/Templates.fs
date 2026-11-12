namespace Falco.Markup

module Templates =
    let private metaCharset = Elem.meta [ Attr.charset "UTF-8" ]
    let private metaCompatibility = Elem.meta [ Attr.httpEquiv "X-UA-Compatible"; Attr.content "IE=edge, chrome=1" ]
    let private metaViewport = Elem.meta [ Attr.name "viewport"; Attr.content "width=device-width, initial-scale=1" ]

    /// HTML 5 template with customizable <head> and <body>
    let html5 (langCode : string) (head : XmlNode list) (body : XmlNode list) =
        Elem.html [ Attr.lang langCode; ] [
            Elem.head [] (metaCharset :: metaCompatibility :: metaViewport :: head)
            Elem.body [] body
        ]

    /// SVG Version 1.0 template with customizable viewBox width/height
    let svg (x : int, y : int, w : int, h : int) (content : XmlNode list) =
        Elem.svg [
            Svg.Attr.viewBox (sprintf "%i %i %i %i" x y w h)
            Svg.Attr.xmlns "http://www.w3.org/2000/svg"
        ] content