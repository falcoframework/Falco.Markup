module Falco.Tests.Text

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
let ``Text shortcuts should produce valid XmlNode`` () =
    Text.h1 "falco" |> renderNode |> should equal "<h1>falco</h1>"
    Text.h2 "falco" |> renderNode |> should equal "<h2>falco</h2>"
    Text.h3 "falco" |> renderNode |> should equal "<h3>falco</h3>"
    Text.h4 "falco" |> renderNode |> should equal "<h4>falco</h4>"
    Text.h5 "falco" |> renderNode |> should equal "<h5>falco</h5>"
    Text.h6 "falco" |> renderNode |> should equal "<h6>falco</h6>"
    Text.p "falco" |> renderNode |> should equal "<p>falco</p>"
    Text.dd "falco" |> renderNode |> should equal "<dd>falco</dd>"
    Text.dt "falco" |> renderNode |> should equal "<dt>falco</dt>"
    Text.abbr "falco" |> renderNode |> should equal "<abbr>falco</abbr>"
    Text.b "falco" |> renderNode |> should equal "<b>falco</b>"
    Text.bdi "falco" |> renderNode |> should equal "<bdi>falco</bdi>"
    Text.bdo "falco" |> renderNode |> should equal "<bdo>falco</bdo>"
    Text.cite "falco" |> renderNode |> should equal "<cite>falco</cite>"
    Text.code "falco" |> renderNode |> should equal "<code>falco</code>"
    Text.data "falco" |> renderNode |> should equal "<data>falco</data>"
    Text.dfn "falco" |> renderNode |> should equal "<dfn>falco</dfn>"
    Text.em "falco" |> renderNode |> should equal "<em>falco</em>"
    Text.i "falco" |> renderNode |> should equal "<i>falco</i>"
    Text.kbd "falco" |> renderNode |> should equal "<kbd>falco</kbd>"
    Text.mark "falco" |> renderNode |> should equal "<mark>falco</mark>"
    Text.q "falco" |> renderNode |> should equal "<q>falco</q>"
    Text.rp "falco" |> renderNode |> should equal "<rp>falco</rp>"
    Text.rt "falco" |> renderNode |> should equal "<rt>falco</rt>"
    Text.ruby "falco" |> renderNode |> should equal "<ruby>falco</ruby>"
    Text.s "falco" |> renderNode |> should equal "<s>falco</s>"
    Text.samp "falco" |> renderNode |> should equal "<samp>falco</samp>"
    Text.small "falco" |> renderNode |> should equal "<small>falco</small>"
    Text.span "falco" |> renderNode |> should equal "<span>falco</span>"
    Text.strong "falco" |> renderNode |> should equal "<strong>falco</strong>"
    Text.sub "falco" |> renderNode |> should equal "<sub>falco</sub>"
    Text.sup "falco" |> renderNode |> should equal "<sup>falco</sup>"
    Text.time "falco" |> renderNode |> should equal "<time>falco</time>"
    Text.u "falco" |> renderNode |> should equal "<u>falco</u>"
    Text.var "falco" |> renderNode |> should equal "<var>falco</var>"
