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
    let t = Elem.createSelfClosing "hr" [ Attr.class' "my-class" ]
    renderNode t |> should equal "<hr class=\"my-class\" />"

[<Fact>]
let ``Standard tag should render with multiple attributes`` () =
    let t = Elem.create "div" [ Attr.create "class" "my-class"; Attr.autofocus; Attr.create "data-bind" "slider" ] []
    renderNode t |> should equal "<div class=\"my-class\" autofocus data-bind=\"slider\"></div>"

[<Fact>]
let ``Script should contain src, lang and async`` () =
    let t = Elem.script [ Attr.src "http://example.org/example.js";  Attr.lang "javascript"; Attr.async ] []
    renderNode t |> should equal "<script src=\"http://example.org/example.js\" lang=\"javascript\" async></script>"

[<Fact>]
let ``Should produce valid html doc`` () =
    let doc =
        Elem.html [] [
            Elem.body [] [
                Elem.div [ Attr.class' "my-class" ] [
                    Elem.h1 [] [ Text.raw "hello" ] ] ] ]
    renderHtml doc |> should equal "<!DOCTYPE html><html><body><div class=\"my-class\"><h1>hello</h1></div></body></html>"

[<Fact>]
let ``Elem.control should render label with nested input`` () =
    let name = "email_address"
    let label = "Email Address"
    let expected = Elem.label [ Attr.for' name ] [
        Elem.span [ Attr.class' "form-label" ] [ Text.raw label ]
        Elem.input [ Attr.id name; Attr.name name; Attr.typeEmail; Attr.required ] ]

    Elem.control name [ Attr.typeEmail; Attr.required  ] [
        Elem.span [ Attr.class' "form-label" ] [ Text.raw label ] ]
    |> should equal expected

[<Fact>]
let ``Should create valid html button`` () =
    let doc = Elem.button [ Attr.onclick "console.log(\"test\")"] [ Text.raw "click me" ]
    renderNode doc |> should equal "<button onclick=\"console.log(\"test\")\">click me</button>";

[<Fact>]
let ``Should produce valid xml doc`` () =
    let doc =
        Elem.create "books" [] [
            Elem.create "book" [] [
                Elem.create "name" [] [ Text.raw "To Kill A Mockingbird" ]
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
        Elem.li [] [
            Elem.h2 [] [ Text.raw product.Name ]
            Text.rawf "Only %f" product.Price
            Text.raw product.Description ]

    let productElems =
        products
        |> List.map elem
        |> Elem.ul [ Attr.id "products" ]

    let doc =
        Elem.html [] [
            Elem.body [] [
                Elem.div [ Attr.class' "my-class" ] [ productElems ] ] ]

    let render = renderHtml doc
    render |> fun s -> s.Substring(0, 27) |> should equal "<!DOCTYPE html><html><body>"
    render |> fun s -> s.Substring(s.Length - 14, 14) |> should equal "</body></html>"

[<Fact>]
let ``HTML Spec`` () =
    Elem.html [] [] |> renderNode |> should equal "<html></html>"
    Elem.base' [] |> renderNode |> should equal "<base />"
    Elem.head [] [] |> renderNode |> should equal "<head></head>"
    Elem.link [] |> renderNode |> should equal "<link />"
    Elem.meta [] |> renderNode |> should equal "<meta />"
    Elem.style [] [] |> renderNode |> should equal "<style></style>"
    Elem.title [] [] |> renderNode |> should equal "<title></title>"
    Elem.body [] [] |> renderNode |> should equal "<body></body>"
    Elem.address [] [] |> renderNode |> should equal "<address></address>"
    Elem.article [] [] |> renderNode |> should equal "<article></article>"
    Elem.aside [] [] |> renderNode |> should equal "<aside></aside>"
    Elem.footer [] [] |> renderNode |> should equal "<footer></footer>"
    Elem.header [] [] |> renderNode |> should equal "<header></header>"
    Elem.h1 [] [] |> renderNode |> should equal "<h1></h1>"
    Elem.h2 [] [] |> renderNode |> should equal "<h2></h2>"
    Elem.h3 [] [] |> renderNode |> should equal "<h3></h3>"
    Elem.h4 [] [] |> renderNode |> should equal "<h4></h4>"
    Elem.h5 [] [] |> renderNode |> should equal "<h5></h5>"
    Elem.h6 [] [] |> renderNode |> should equal "<h6></h6>"
    Elem.main [] [] |> renderNode |> should equal "<main></main>"
    Elem.nav [] [] |> renderNode |> should equal "<nav></nav>"
    Elem.section [] [] |> renderNode |> should equal "<section></section>"
    Elem.blockquote [] [] |> renderNode |> should equal "<blockquote></blockquote>"
    Elem.dd [] [] |> renderNode |> should equal "<dd></dd>"
    Elem.div [] [] |> renderNode |> should equal "<div></div>"
    Elem.dl [] [] |> renderNode |> should equal "<dl></dl>"
    Elem.dt [] [] |> renderNode |> should equal "<dt></dt>"
    Elem.figcaption [] [] |> renderNode |> should equal "<figcaption></figcaption>"
    Elem.figure [] [] |> renderNode |> should equal "<figure></figure>"
    Elem.hr [] |> renderNode |> should equal "<hr />"
    Elem.li [] [] |> renderNode |> should equal "<li></li>"
    Elem.menu [] [] |> renderNode |> should equal "<menu></menu>"
    Elem.ol [] [] |> renderNode |> should equal "<ol></ol>"
    Elem.p [] [] |> renderNode |> should equal "<p></p>"
    Elem.pre [] [] |> renderNode |> should equal "<pre></pre>"
    Elem.ul [] [] |> renderNode |> should equal "<ul></ul>"
    Elem.a [] [] |> renderNode |> should equal "<a></a>"
    Elem.abbr [] [] |> renderNode |> should equal "<abbr></abbr>"
    Elem.b [] [] |> renderNode |> should equal "<b></b>"
    Elem.bdi [] [] |> renderNode |> should equal "<bdi></bdi>"
    Elem.bdo [] [] |> renderNode |> should equal "<bdo></bdo>"
    Elem.br [] |> renderNode |> should equal "<br />"
    Elem.cite [] [] |> renderNode |> should equal "<cite></cite>"
    Elem.code [] [] |> renderNode |> should equal "<code></code>"
    Elem.data [] [] |> renderNode |> should equal "<data></data>"
    Elem.dfn [] [] |> renderNode |> should equal "<dfn></dfn>"
    Elem.em [] [] |> renderNode |> should equal "<em></em>"
    Elem.i [] [] |> renderNode |> should equal "<i></i>"
    Elem.kbd [] [] |> renderNode |> should equal "<kbd></kbd>"
    Elem.mark [] [] |> renderNode |> should equal "<mark></mark>"
    Elem.q [] [] |> renderNode |> should equal "<q></q>"
    Elem.rp [] [] |> renderNode |> should equal "<rp></rp>"
    Elem.rt [] [] |> renderNode |> should equal "<rt></rt>"
    Elem.ruby [] [] |> renderNode |> should equal "<ruby></ruby>"
    Elem.s [] [] |> renderNode |> should equal "<s></s>"
    Elem.samp [] [] |> renderNode |> should equal "<samp></samp>"
    Elem.small [] [] |> renderNode |> should equal "<small></small>"
    Elem.span [] [] |> renderNode |> should equal "<span></span>"
    Elem.strong [] [] |> renderNode |> should equal "<strong></strong>"
    Elem.sub [] [] |> renderNode |> should equal "<sub></sub>"
    Elem.sup [] [] |> renderNode |> should equal "<sup></sup>"
    Elem.time [] [] |> renderNode |> should equal "<time></time>"
    Elem.u [] [] |> renderNode |> should equal "<u></u>"
    Elem.var [] [] |> renderNode |> should equal "<var></var>"
    Elem.wbr [] |> renderNode |> should equal "<wbr />"
    Elem.area [] [] |> renderNode |> should equal "<area></area>"
    Elem.audio [] [] |> renderNode |> should equal "<audio></audio>"
    Elem.img [] |> renderNode |> should equal "<img />"
    Elem.map [] [] |> renderNode |> should equal "<map></map>"
    Elem.track [] |> renderNode |> should equal "<track />"
    Elem.video [] [] |> renderNode |> should equal "<video></video>"
    Elem.embed [] |> renderNode |> should equal "<embed />"
    Elem.iframe [] [] |> renderNode |> should equal "<iframe></iframe>"
    Elem.object [] [] |> renderNode |> should equal "<object></object>"
    Elem.picture [] [] |> renderNode |> should equal "<picture></picture>"
    Elem.portal [] [] |> renderNode |> should equal "<portal></portal>"
    Elem.source [] |> renderNode |> should equal "<source />"
    Elem.canvas [] [] |> renderNode |> should equal "<canvas></canvas>"
    Elem.noscript [] [] |> renderNode |> should equal "<noscript></noscript>"
    Elem.script [] [] |> renderNode |> should equal "<script></script>"
    Elem.del [] [] |> renderNode |> should equal "<del></del>"
    Elem.ins [] [] |> renderNode |> should equal "<ins></ins>"
    Elem.caption [] [] |> renderNode |> should equal "<caption></caption>"
    Elem.col [] |> renderNode |> should equal "<col />"
    Elem.colgroup [] [] |> renderNode |> should equal "<colgroup></colgroup>"
    Elem.table [] [] |> renderNode |> should equal "<table></table>"
    Elem.tbody [] [] |> renderNode |> should equal "<tbody></tbody>"
    Elem.td [] [] |> renderNode |> should equal "<td></td>"
    Elem.tfoot [] [] |> renderNode |> should equal "<tfoot></tfoot>"
    Elem.th [] [] |> renderNode |> should equal "<th></th>"
    Elem.thead [] [] |> renderNode |> should equal "<thead></thead>"
    Elem.tr [] [] |> renderNode |> should equal "<tr></tr>"
    Elem.button [] [] |> renderNode |> should equal "<button></button>"
    Elem.datalist [] [] |> renderNode |> should equal "<datalist></datalist>"
    Elem.fieldset [] [] |> renderNode |> should equal "<fieldset></fieldset>"
    Elem.form [] [] |> renderNode |> should equal "<form></form>"
    Elem.input [] |> renderNode |> should equal "<input />"
    Elem.label [] [] |> renderNode |> should equal "<label></label>"
    Elem.legend [] [] |> renderNode |> should equal "<legend></legend>"
    Elem.meter [] [] |> renderNode |> should equal "<meter></meter>"
    Elem.optgroup [] [] |> renderNode |> should equal "<optgroup></optgroup>"
    Elem.option [] [] |> renderNode |> should equal "<option></option>"
    Elem.output [] [] |> renderNode |> should equal "<output></output>"
    Elem.progress [] [] |> renderNode |> should equal "<progress></progress>"
    Elem.select [] [] |> renderNode |> should equal "<select></select>"
    Elem.textarea [] [] |> renderNode |> should equal "<textarea></textarea>"
    Elem.details [] [] |> renderNode |> should equal "<details></details>"
    Elem.dialog [] [] |> renderNode |> should equal "<dialog></dialog>"
    Elem.summary [] [] |> renderNode |> should equal "<summary></summary>"
    Elem.slot [] [] |> renderNode |> should equal "<slot></slot>"
    Elem.template [] [] |> renderNode |> should equal "<template></template>"
    Elem.math [] [] |> renderNode |> should equal "<math></math>"
    Elem.svg [] [] |> renderNode |> should equal "<svg></svg>"
