module Falco.Tests.Elem

open Falco.Markup
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Self-closing tag should render with trailing slash`` () =
    let t = Elem.createSelfClosing "hr" []
    renderNode t |> should equal "<hr />"

[<Fact>]
let ``Self-closing tag with attrs should render with trailing slash`` () =
    let t = Elem.createSelfClosing "hr" [ _class_ "my-class" ]
    renderNode t |> should equal "<hr class=\"my-class\" />"

[<Fact>]
let ``Standard tag should render with multiple attributes`` () =
    let t = Elem.create "div" [ Attr.create "class" "my-class"; _autofocus_; Attr.create "data-bind" "slider" ] []
    renderNode t |> should equal "<div class=\"my-class\" autofocus data-bind=\"slider\"></div>"


[<Fact>]
let ``Can render NodeList with items``() =
    Elem.createFragment [
        for i = 1 to 3 do
            yield _div [] [ _textf "%i" i ]
    ]
    |> renderNode
    |> should equal "<div>1</div><div>2</div><div>3</div>"

[<Fact>]
let ``Can render empty NodeList``() =
    Elem.empty
    |> renderNode
    |> should equal ""

[<Fact>]
let ``Script should contain src, lang and async`` () =
    let t = _script [ _src_ "http://example.org/example.js";  _lang_ "javascript"; _async_ ] []
    renderNode t |> should equal "<script src=\"http://example.org/example.js\" lang=\"javascript\" async></script>"

[<Fact>]
let ``Should produce valid html doc`` () =
    let doc =
        _html [] [
            _body [] [
                _div [ _class_ "my-class" ] [
                    _h1 [] [ _text "hello" ] ] ] ]
    renderHtml doc |> should equal "<!DOCTYPE html><html><body><div class=\"my-class\"><h1>hello</h1></div></body></html>"

[<Fact>]
let ``_control should render label with nested input`` () =
    let name = "email_address"
    let label = "Email Address"
    let expected = _label [ _for_ name ] [
        _span [ _class_ "form-label" ] [ _text label ]
        _input [ _id_ name; _name_ name; _typeEmail_; _required_ ] ]

    _control name [ _typeEmail_; _required_  ] [
        _span [ _class_ "form-label" ] [ _text label ] ]
    |> should equal expected

[<Fact>]
let ``Should create valid html button`` () =
    let doc = _button [ _onclick_ "console.log(\"test\")"] [ _text "click me" ]
    renderNode doc |> should equal "<button onclick=\"console.log(\"test\")\">click me</button>";

[<Fact>]
let ``Should produce valid xml doc`` () =
    let doc =
        Elem.create "books" [] [
            Elem.create "book" [] [
                Elem.create "name" [] [ _text "To Kill A Mockingbird" ]
            ]
        ]

    renderXml doc |> should equal "<?xml version=\"1.0\" encoding=\"UTF-8\"?><books><book><name>To Kill A Mockingbird</name></book></books>"

type Product =
    { Name : string
      Price : float
      Description : string }

[<Fact>]
let ``Should produce valid html doc for large result`` () =
    let lorem = "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum";

    let products =
        [ 1..25000 ]
        |> List.map (fun i -> { Name = sprintf "Name %i" i; Price = i |> float; Description = lorem})

    let elem product =
        _li [] [
            _h2 [] [ _text product.Name ]
            _textf "Only %f" product.Price
            _text product.Description ]

    let productElems =
        products
        |> List.map elem
        |> _ul [ _id_ "products" ]

    let doc =
        _html [] [
            _body [] [
                _div [ _class_ "my-class" ] [ productElems ] ] ]

    let render = renderHtml doc
    render |> fun s -> s.Substring(0, 27) |> should equal "<!DOCTYPE html><html><body>"
    render |> fun s -> s.Substring(s.Length - 14, 14) |> should equal "</body></html>"

[<Fact>]
let ``HTML Spec`` () =
    _html [] [] |> renderNode |> should equal "<html></html>"
    _base [] |> renderNode |> should equal "<base />"
    _head [] [] |> renderNode |> should equal "<head></head>"
    _link [] |> renderNode |> should equal "<link />"
    _meta [] |> renderNode |> should equal "<meta />"
    _style [] [] |> renderNode |> should equal "<style></style>"
    _title [] [] |> renderNode |> should equal "<title></title>"
    _body [] [] |> renderNode |> should equal "<body></body>"
    _address [] [] |> renderNode |> should equal "<address></address>"
    _article [] [] |> renderNode |> should equal "<article></article>"
    _aside [] [] |> renderNode |> should equal "<aside></aside>"
    _footer [] [] |> renderNode |> should equal "<footer></footer>"
    _header [] [] |> renderNode |> should equal "<header></header>"
    _h1 [] [] |> renderNode |> should equal "<h1></h1>"
    _h2 [] [] |> renderNode |> should equal "<h2></h2>"
    _h3 [] [] |> renderNode |> should equal "<h3></h3>"
    _h4 [] [] |> renderNode |> should equal "<h4></h4>"
    _h5 [] [] |> renderNode |> should equal "<h5></h5>"
    _h6 [] [] |> renderNode |> should equal "<h6></h6>"
    _main [] [] |> renderNode |> should equal "<main></main>"
    _nav [] [] |> renderNode |> should equal "<nav></nav>"
    _section [] [] |> renderNode |> should equal "<section></section>"
    _blockquote [] [] |> renderNode |> should equal "<blockquote></blockquote>"
    _dd [] [] |> renderNode |> should equal "<dd></dd>"
    _div [] [] |> renderNode |> should equal "<div></div>"
    _dl [] [] |> renderNode |> should equal "<dl></dl>"
    _dt [] [] |> renderNode |> should equal "<dt></dt>"
    _figcaption [] [] |> renderNode |> should equal "<figcaption></figcaption>"
    _figure [] [] |> renderNode |> should equal "<figure></figure>"
    _hr [] |> renderNode |> should equal "<hr />"
    _li [] [] |> renderNode |> should equal "<li></li>"
    _menu [] [] |> renderNode |> should equal "<menu></menu>"
    _ol [] [] |> renderNode |> should equal "<ol></ol>"
    _p [] [] |> renderNode |> should equal "<p></p>"
    _pre [] [] |> renderNode |> should equal "<pre></pre>"
    _ul [] [] |> renderNode |> should equal "<ul></ul>"
    _a [] [] |> renderNode |> should equal "<a></a>"
    _abbr [] [] |> renderNode |> should equal "<abbr></abbr>"
    _b [] [] |> renderNode |> should equal "<b></b>"
    _bdi [] [] |> renderNode |> should equal "<bdi></bdi>"
    _bdo [] [] |> renderNode |> should equal "<bdo></bdo>"
    _br [] |> renderNode |> should equal "<br />"
    _cite [] [] |> renderNode |> should equal "<cite></cite>"
    _code [] [] |> renderNode |> should equal "<code></code>"
    _data [] [] |> renderNode |> should equal "<data></data>"
    _dfn [] [] |> renderNode |> should equal "<dfn></dfn>"
    _em [] [] |> renderNode |> should equal "<em></em>"
    _i [] [] |> renderNode |> should equal "<i></i>"
    _kbd [] [] |> renderNode |> should equal "<kbd></kbd>"
    _mark [] [] |> renderNode |> should equal "<mark></mark>"
    _q [] [] |> renderNode |> should equal "<q></q>"
    _rp [] [] |> renderNode |> should equal "<rp></rp>"
    _rt [] [] |> renderNode |> should equal "<rt></rt>"
    _ruby [] [] |> renderNode |> should equal "<ruby></ruby>"
    _s [] [] |> renderNode |> should equal "<s></s>"
    _samp [] [] |> renderNode |> should equal "<samp></samp>"
    _small [] [] |> renderNode |> should equal "<small></small>"
    _span [] [] |> renderNode |> should equal "<span></span>"
    _strong [] [] |> renderNode |> should equal "<strong></strong>"
    _sub [] [] |> renderNode |> should equal "<sub></sub>"
    _sup [] [] |> renderNode |> should equal "<sup></sup>"
    _time [] [] |> renderNode |> should equal "<time></time>"
    _u [] [] |> renderNode |> should equal "<u></u>"
    _var [] [] |> renderNode |> should equal "<var></var>"
    _wbr [] |> renderNode |> should equal "<wbr />"
    _area [] [] |> renderNode |> should equal "<area></area>"
    _audio [] [] |> renderNode |> should equal "<audio></audio>"
    _img [] |> renderNode |> should equal "<img />"
    _map [] [] |> renderNode |> should equal "<map></map>"
    _track [] |> renderNode |> should equal "<track />"
    _video [] [] |> renderNode |> should equal "<video></video>"
    _embed [] |> renderNode |> should equal "<embed />"
    _iframe [] [] |> renderNode |> should equal "<iframe></iframe>"
    _object [] [] |> renderNode |> should equal "<object></object>"
    _picture [] [] |> renderNode |> should equal "<picture></picture>"
    _portal [] [] |> renderNode |> should equal "<portal></portal>"
    _source [] |> renderNode |> should equal "<source />"
    _canvas [] [] |> renderNode |> should equal "<canvas></canvas>"
    _noscript [] [] |> renderNode |> should equal "<noscript></noscript>"
    _script [] [] |> renderNode |> should equal "<script></script>"
    _del [] [] |> renderNode |> should equal "<del></del>"
    _ins [] [] |> renderNode |> should equal "<ins></ins>"
    _caption [] [] |> renderNode |> should equal "<caption></caption>"
    _col [] |> renderNode |> should equal "<col />"
    _colgroup [] [] |> renderNode |> should equal "<colgroup></colgroup>"
    _table [] [] |> renderNode |> should equal "<table></table>"
    _tbody [] [] |> renderNode |> should equal "<tbody></tbody>"
    _td [] [] |> renderNode |> should equal "<td></td>"
    _tfoot [] [] |> renderNode |> should equal "<tfoot></tfoot>"
    _th [] [] |> renderNode |> should equal "<th></th>"
    _thead [] [] |> renderNode |> should equal "<thead></thead>"
    _tr [] [] |> renderNode |> should equal "<tr></tr>"
    _button [] [] |> renderNode |> should equal "<button></button>"
    _datalist [] [] |> renderNode |> should equal "<datalist></datalist>"
    _fieldset [] [] |> renderNode |> should equal "<fieldset></fieldset>"
    _form [] [] |> renderNode |> should equal "<form></form>"
    _input [] |> renderNode |> should equal "<input />"
    _label [] [] |> renderNode |> should equal "<label></label>"
    _legend [] [] |> renderNode |> should equal "<legend></legend>"
    _meter [] [] |> renderNode |> should equal "<meter></meter>"
    _optgroup [] [] |> renderNode |> should equal "<optgroup></optgroup>"
    _option [] [] |> renderNode |> should equal "<option></option>"
    _output [] [] |> renderNode |> should equal "<output></output>"
    _progress [] [] |> renderNode |> should equal "<progress></progress>"
    _select [] [] |> renderNode |> should equal "<select></select>"
    _textarea [] [] |> renderNode |> should equal "<textarea></textarea>"
    _details [] [] |> renderNode |> should equal "<details></details>"
    _dialog [] [] |> renderNode |> should equal "<dialog></dialog>"
    _summary [] [] |> renderNode |> should equal "<summary></summary>"
    _slot [] [] |> renderNode |> should equal "<slot></slot>"
    _template [] [] |> renderNode |> should equal "<template></template>"
    _math [] [] |> renderNode |> should equal "<math></math>"
    _svg [] [] |> renderNode |> should equal "<svg></svg>"
