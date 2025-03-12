module Falco.Tests.Markup

open System
open Falco.Markup
open Falco.Markup.Svg
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Text.empty should be empty`` () =
    renderNode Text.empty |> should equal String.Empty

[<Fact>]
let ``Text.raw should not be encoded`` () =
    let rawText = Text.raw "<div>"
    renderNode rawText |> should equal "<div>"

[<Fact>]
let ``Text.raw should not be encoded, but template applied`` () =
    let rawText = Text.rawf "<div>%s</div>" "falco"
    renderNode rawText |> should equal "<div>falco</div>"

[<Fact>]
let ``Text.enc should be encoded`` () =
    let encodedText = Text.enc "<div>"
    renderNode encodedText |> should equal "&lt;div&gt;"

[<Fact>]
let ``Text.comment should equal HTML comment`` () =
    let rawText = Text.comment "test comment"
    renderNode rawText |> should equal "<!-- test comment -->"

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

[<Fact>]
let ``Should produce valid svg`` () =
    // https://developer.mozilla.org/en-US/docs/Web/SVG/Element/text#example
    let doc =
        Templates.svg (0, 0, 240, 80) [
            Elem.style [] [
                Text.raw ".small { font: italic 13px sans-serif; }"
                Text.raw ".heavy { font: bold 30px sans-serif; }"
                Text.raw ".Rrrrr { font: italic 40px serif; fill: red; }"
            ]
            Elem.text [ Attr.x "20"; Attr.y "35"; Attr.class' "small" ] [ Text.raw "My" ]
            Elem.text [ Attr.x "40"; Attr.y "35"; Attr.class' "heavy" ] [ Text.raw "cat" ]
            Elem.text [ Attr.x "55"; Attr.y "55"; Attr.class' "small" ] [ Text.raw "is" ]
            Elem.text [ Attr.x "65"; Attr.y "55"; Attr.class' "Rrrrr" ] [ Text.raw "Grumpy!" ]
        ]

    renderNode doc |> should equal "<svg viewBox=\"0 0 240 80\" xmlns=\"http://www.w3.org/2000/svg\"><style>.small { font: italic 13px sans-serif; }.heavy { font: bold 30px sans-serif; }.Rrrrr { font: italic 40px serif; fill: red; }</style><text x=\"20\" y=\"35\" class=\"small\">My</text><text x=\"40\" y=\"35\" class=\"heavy\">cat</text><text x=\"55\" y=\"55\" class=\"small\">is</text><text x=\"65\" y=\"55\" class=\"Rrrrr\">Grumpy!</text></svg>"

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
let ``Attr.merge should combine two XmlAttribute lists`` () =
    Attr.merge
        [ Attr.class' "ma2" ]
        [ Attr.id "some-el"; Attr.class' "bg-red"; Attr.readonly ]
    |> should equal [ Attr.class' "ma2 bg-red"; Attr.id "some-el"; Attr.readonly ]

[<Fact>]
let ``Attr.merge should work with bogus "class" NonValeAttr`` () =
    Attr.merge
        [ Attr.class' "ma2" ]
        [ Attr.id "some-el"; Attr.class' "bg-red"; NonValueAttr("class") ]
    |> should equal [ Attr.class' "ma2 bg-red"; Attr.id "some-el" ]

module TestHelpersTests =
    open Falco.Markup.TestHelpers

    [<Fact>]
    let ``No form values should return empty string`` () =
        let xml = Elem.div [] []
        let nameValues = renderNameValues xml

        nameValues |> should equal String.Empty

    [<Fact>]
    let ``Single form input should return one name/value pair`` () =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco" ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco"

    [<Fact>]
    let ``Nested form input should return one name/value pair`` () =
        let xml = Elem.form [] [
            Elem.section [] [
                Elem.div [] [
                    Elem.fieldset [] [
                        Elem.input [ Attr.name "name"; Attr.value "falco" ] ] ] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco"

    [<Fact>]
    let ``Non-relevant attributes should not be output`` () =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco"; Attr.id "some-input" ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco"

    [<Fact>]
    let ``Multiple form inputs should return name/value pairs`` () =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco" ]
            Elem.input [ Attr.name "age"; Attr.value "3"; Attr.type' "number" ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco&age=3"

    [<Fact>]
    let ``Single textarea should return one name/value pair`` () =
        let xml = Elem.form [] [
            Elem.textarea [ Attr.name "name" ] [ Text.raw "falco"] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco"

    [<Fact>]
    let ``Unnamed input(s) should not affect name/value pairs`` () =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco" ]
            Elem.input [ Attr.value "bad" ]
            Elem.textarea [] [ Text.raw "bad" ]
            Elem.input [ Attr.name "age"; Attr.value "3"; Attr.type' "number" ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco&age=3"

    [<Theory>]
    [<InlineData("radio")>]
    [<InlineData("checkbox")>]
    let ``Single radio/checkbox input that is checked should return one name/value pair`` (tagType : string) =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco"; Attr.type' tagType; Attr.checked' ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco"

    [<Theory>]
    [<InlineData("radio")>]
    [<InlineData("checkbox")>]
    let ``Single radio input that is NOT checked should return nothing`` (tagType : string) =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco"; Attr.type' tagType ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal ""

    [<Theory>]
    [<InlineData("radio")>]
    [<InlineData("checkbox")>]
    let ``Multiple radio/checkbox inputs should return name/value pairs for checked inputs`` (tagType : string) =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "name"; Attr.value "falco1"; Attr.type' tagType; ]
            Elem.input [ Attr.name "name"; Attr.value "falco2"; Attr.type' tagType; Attr.checked' ]
            Elem.input [ Attr.name "name"; Attr.value "falco3"; Attr.type' tagType; Attr.checked' ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco2&name=falco3"

    [<Theory>]
    [<InlineData(true)>]
    [<InlineData(false)>]
    let ``Select with nothing selected should return one name with no value`` (multiple : bool) =
        let xml = Elem.form [] [
            Elem.select [ Attr.name "name"; if multiple then Attr.multiple ] [
                Elem.option [ Attr.value "option1" ] [ Text.raw "Option 1"] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name="

    [<Theory>]
    [<InlineData(true)>]
    [<InlineData(false)>]
    let ``Select with one selection should return one name/value pair`` (multiple : bool) =
        let xml = Elem.form [] [
            Elem.select [ Attr.name "name"; if multiple then Attr.multiple ] [
                Elem.option [ Attr.value "option1"; Attr.selected ] [ Text.raw "Option 1"] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=option1"

    [<Fact>]
    let ``Multiselect with selections should return name/value pairs`` () =
        let xml = Elem.form [] [
            Elem.select [ Attr.name "name"; Attr.multiple ] [
                Elem.option [ Attr.value "option1" ] [ Text.raw "Option 1"]
                Elem.option [ Attr.value "option2"; Attr.selected ] [ Text.raw "Option 2"]
                Elem.option [ Attr.value "option3"; Attr.selected ] [ Text.raw "Option 3"] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=option2&name=option3"

    [<Theory>]
    [<InlineData(true)>]
    [<InlineData(false)>]
    let ``Select including optgroup with nothing selected should return one name with no value`` (multiple : bool) =
        let xml = Elem.form [] [
            Elem.select [ Attr.name "name"; if multiple then Attr.multiple ] [
                Elem.optgroup [] [
                    Elem.option [ Attr.value "option1" ] [ Text.raw "Option 1"] ]
                Elem.optgroup [] [
                    Elem.option [ Attr.value "option2" ] [ Text.raw "Option 2"] ] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name="

    [<Theory>]
    [<InlineData(true)>]
    [<InlineData(false)>]
    let ``Select including optgroup with selections should return name/value pairs`` (multiple : bool) =
        let xml = Elem.form [] [
            Elem.select [ Attr.name "name"; if multiple then Attr.multiple ] [
                Elem.optgroup [] [
                    Elem.option [ Attr.value "option1"; Attr.selected ] [ Text.raw "Option 1"] ]
                Elem.optgroup [] [
                    Elem.option [ Attr.value "option2" ] [ Text.raw "Option 2"] ]
                Elem.optgroup [] [
                    Elem.option [ Attr.value "option3"; Attr.selected ] [ Text.raw "Option 3"] ] ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=option1&name=option3"

    [<Fact>]
    let ``Kitchen sink`` () =
        let xml = Elem.form [] [
            Elem.input [ Attr.name "first_name"; Attr.value "first_name_value" ]

            Elem.textarea [ Attr.name "long_text" ] [ Text.raw "long_text_value" ]

            Elem.div [] [
                Elem.input [ Attr.type' "radio"; Attr.name "radio"; Attr.value "value1"; Attr.checked' ]
                Elem.input [ Attr.type' "radio"; Attr.name "radio"; Attr.value "value2" ]
                Elem.input [ Attr.type' "radio"; Attr.name "radio"; Attr.value "value3" ]
            ]

            Elem.div [] [
                Elem.input [ Attr.type' "checkbox"; Attr.name "checkbox"; Attr.value "value1"; Attr.checked' ]
                Elem.input [ Attr.type' "checkbox"; Attr.name "checkbox"; Attr.value "value2"; Attr.checked' ]
                Elem.input [ Attr.type' "checkbox"; Attr.name "checkbox"; Attr.value "value3" ]
            ]

            Elem.select [ Attr.name "select" ] [
                Elem.option [ Attr.value "option1" ] [ Text.raw "Option 1" ]
                Elem.option [ Attr.value "option2"; Attr.selected ] [ Text.raw "Option 2" ]
                Elem.option [ Attr.value "option3" ] [ Text.raw "Option 3" ]
            ]

            Elem.select [ Attr.name "multiselect"; Attr.multiple ] [
                Elem.option [ Attr.value "option1" ] [ Text.raw "Option 1" ]
                Elem.option [ Attr.value "option2"; Attr.selected ] [ Text.raw "Option 2" ]
                Elem.option [ Attr.value "option3"; Attr.selected ] [ Text.raw "Option 3" ]
            ]

            Elem.input [ Attr.type' "submit" ]
        ]

        let nameValues = renderNameValues xml

        nameValues
        |> should equal "first_name=first_name_value&long_text=long_text_value&radio=value1&checkbox=value1&checkbox=value2&select=option2&multiselect=option2&multiselect=option3"
