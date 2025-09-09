module Falco.Tests.TestHelpersTests

open System
open Falco.Markup
open FsUnit.Xunit
open Xunit

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
    let ``List of form inputs should return multiple name/value pair`` () =
        let xml = NodeList [
            Elem.input [ Attr.name "name"; Attr.value "falco" ]
            Elem.input [ Attr.name "version"; Attr.value "5"; Attr.typeNumber ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco&version=5"


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
            Elem.input [ Attr.name "version"; Attr.value "5"; Attr.typeNumber ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco&version=5"

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
            Elem.input [ Attr.name "version"; Attr.value "5"; Attr.typeNumber ] ]

        let nameValues = renderNameValues xml

        nameValues |> should equal "name=falco&version=5"

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
            Elem.h1 [] [ Text.raw "HJERE" ]
            Elem.input [ Attr.name "first_name"; Attr.value "first_name_value" ]

            Elem.textarea [ Attr.name "long_text" ] [ Text.raw "long_text_value" ]

            Elem.div [] [
                Elem.input [ Attr.typeRadio; Attr.name "radio"; Attr.value "value1"; Attr.checked' ]
                Elem.input [ Attr.typeRadio; Attr.name "radio"; Attr.value "value2" ]
                Elem.input [ Attr.typeRadio; Attr.name "radio"; Attr.value "value3" ]
            ]

            Elem.div [] [
                Elem.input [ Attr.typeCheckbox; Attr.name "checkbox"; Attr.value "value1"; Attr.checked' ]
                Elem.input [ Attr.typeCheckbox; Attr.name "checkbox"; Attr.value "value2"; Attr.checked' ]
                Elem.input [ Attr.typeCheckbox; Attr.name "checkbox"; Attr.value "value3" ]
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

            Elem.input [ Attr.typeSubmit ]
        ]

        let nameValues = renderNameValues xml

        nameValues
        |> should equal "first_name=first_name_value&long_text=long_text_value&radio=value1&checkbox=value1&checkbox=value2&select=option2&multiselect=option2&multiselect=option3"
