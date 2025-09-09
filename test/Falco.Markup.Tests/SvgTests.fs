module Falco.Tests.Svg

open Falco.Markup
open Falco.Markup.Svg
open FsUnit.Xunit
open Xunit

[<Fact>]
let ``Should produce valid svg`` () =
    // https://developer.mozilla.org/en-US/docs/Web/SVG/Element/text#example
    let doc =
        Templates.svg (0, 0, 240, 80) [
            _style [] [
                _text ".small { font: italic 13px sans-serif; }"
                _text ".heavy { font: bold 30px sans-serif; }"
                _text ".Rrrrr { font: italic 40px serif; fill: red; }"
            ]
            Elem.text [ Attr.x "20"; Attr.y "35"; Attr.class' "small" ] [ _text "My" ]
            Elem.text [ Attr.x "40"; Attr.y "35"; Attr.class' "heavy" ] [ _text "cat" ]
            Elem.text [ Attr.x "55"; Attr.y "55"; Attr.class' "small" ] [ _text "is" ]
            Elem.text [ Attr.x "65"; Attr.y "55"; Attr.class' "Rrrrr" ] [ _text "Grumpy!" ]
        ]

    renderNode doc |> should equal "<svg viewBox=\"0 0 240 80\" xmlns=\"http://www.w3.org/2000/svg\"><style>.small { font: italic 13px sans-serif; }.heavy { font: bold 30px sans-serif; }.Rrrrr { font: italic 40px serif; fill: red; }</style><text x=\"20\" y=\"35\" class=\"small\">My</text><text x=\"40\" y=\"35\" class=\"heavy\">cat</text><text x=\"55\" y=\"55\" class=\"small\">is</text><text x=\"65\" y=\"55\" class=\"Rrrrr\">Grumpy!</text></svg>"
