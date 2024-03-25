# Falco.Markup

[![NuGet Version](https://img.shields.io/nuget/v/Falco.Markup.svg)](https://www.nuget.org/packages/Falco.Markup)
[![build](https://github.com/pimbrouwers/Falco.Markup/actions/workflows/build.yml/badge.svg)](https://github.com/pimbrouwers/Falco.Markup/actions/workflows/build.yml)

```fsharp
open Falco.Markup

let doc =
    Elem.html [] [
        Elem.body [ Attr.class' "100-vh" ] [
            Text.h1 "Hello world!" ] ]

renderHtml doc
```

[Falco.Markup](https://github.com/pimbrouwers/Falco.Markup) is an XML markup module that can be used to produce any form of angle-bracket markup (i.e. HTML, SVG, XML etc.).

## Key Features

- Use native F# to produce any form of [angle-bracket markup](#html).
- Simple to create reusable blocks of code (i.e., partial views and components).
- Easily extended by creating [custom tags and attributes](#custom-elements--attributes).
- Compiled as part of your assembly, leading to improved [performance](#performance) and simpler deployments.
- Provides strongly typed functions matching the full HTML [spec](https://developer.mozilla.org/en-US/docs/Web/HTML/Reference).

## Design Goals

- Provide a tool to generate any form of [angle-bracket markup](#html).
- Must be [performant](#performance) and memory efficient.
- Should be simple, extensible and integrate with existing .NET libraries.
- Can be easily learned.
- Match HTML [spec](https://developer.mozilla.org/en-US/docs/Web/HTML/Reference) as closely as possible.

## Overview

Falco.Markup is broken down into three primary modules, `Elem`, `Attr` and `Text`, which are used to generate elements, attributes and text nodes respectively. Each module contain a suite of functions mapping to the various element/attribute/node names. But can also be extended to create custom elements and attributes.

Primary elements are broken down into two types, `ParentNode` or `SelfClosingNode`.

`ParentNode` elements are those that can contain other elements. Represented as functions that receive two inputs: attributes and optionally elements.

```fsharp
let markup =
    Elem.div [ Attr.class' "heading" ] [
        Text.h1 "Hello world!" ]
```

`SelfClosingNode` elements are self-closing tags. Represented as functions that receive one input: attributes.

```fsharp
let markup =
    Elem.div [ Attr.class' "divider" ] [
        Elem.hr [] ]
```

Text is represented using the `TextNode` and created using one of the functions in the `Text` module.

```fsharp
let markup =
    Elem.div [] [
        Text.comment "An HTML comment"
        Text.p "A paragraph"
        Elem.p [] [ Text.rawf "Hello %s" "Jim" ]
        Elem.code [] [ Text.enc "<div>Hello</div>" ] // HTML encodes text before rendering
    ]
```

Attributes contain two subtypes as well, `KeyValueAttr` which represent key/value attributes or `NonValueAttr` which represent boolean attributes.

```fsharp
let markup =
    Elem.input [ Attr.type' "text"; Attr.required ]
```

Most [JavaScript Events](https://developer.mozilla.org/en-US/docs/Web/Events) have also been mapped in the `Attr` module. All of these events are prefixed with the word "on" (i.e., `Attr.onclick`, `Attr.onfocus` etc.)

```fsharp
let markup =
    Elem.button [ Attr.onclick "console.log(\"hello world\")" ] [ Text.raw "Click me" ]
```

## HTML

Though Falco.Markup can be used to produce any markup. It is first and foremost an HTML library.

### Combining views to create complex output

```fsharp
open Falco.Markup

// Components
let divider =
    Elem.hr [ Attr.class' "divider" ]

// Template
let master (title : string) (content : XmlNode list) =
    Elem.html [ Attr.lang "en" ] [
        Elem.head [] [
            Elem.title [] [ Text.raw title ]
        ]
        Elem.body [] content
    ]

// Views
let homeView =
    master "Homepage" [
        Text.h1 "Homepage"
        divider
        Text.p "Lorem ipsum dolor sit amet, consectetur adipiscing."
    ]

let aboutView =
    master "About Us" [
        Text.h1 "About"
        divider
        Text.p "Lorem ipsum dolor sit amet, consectetur adipiscing."
    ]
```

### Strongly-typed views

```fsharp
open Falco.Markup

type Person =
    { FirstName : string
      LastName : string }

let doc (person : Person) =
    Elem.html [ Attr.lang "en" ] [
        Elem.head [] [
            Elem.title [] [ Text.raw "Sample App" ]
        ]
        Elem.body [] [
            Elem.main [] [
                Text.h1 "Sample App"
                Text.p $"{person.First} {person.Last}"
            ]
        ]
    ]
```

### Forms

Forms are the lifeblood of HTML applications. A basic form using the markup module would like the following:

```fsharp
Elem.form [ Attr.method "post"; Attr.action "/submit" ] [
    Elem.label [ Attr.for' "name" ] [ Text.raw "Name" ]
    Elem.input [ Attr.id "name"; Attr.name "name"; Attr.typeText ]

    Elem.input [ Attr.typeSubmit ]
]
```

Expanding on this, we can create a more complex form involving multiple inputs and input types as follows:

```fsharp
Elem.form [ Attr.method "post"; Attr.action "/submit" ] [
    Elem.label [ Attr.for' "name" ] [ Text.raw "Name" ]
    Elem.input [ Attr.id "name"; Attr.name "name" ]

    Elem.label [ Attr.for' "bio" ] [ Text.raw "Bio" ]
    Elem.textarea [ Attr.name "id"; Attr.name "bio" ] []

    Elem.label [ Attr.for' "hobbies" ] [ Text.raw "Hobbies" ]
    Elem.select [ Attr.id "hobbies"; Attr.name "hobbies"; Attr.multiple ] [
        Elem.option [ Attr.value "programming" ] [ Text.raw "Programming" ]
        Elem.option [ Attr.value "diy" ] [ Text.raw "DIY" ]
        Elem.option [ Attr.value "basketball" ] [ Text.raw "Basketball" ]
    ]

    Elem.fieldset [] [
        Elem.legend [] [ Text.raw "Do you like chocolate?" ]
        Elem.label [] [
            Text.raw "Yes"
            Elem.input [ Attr.typeRadio; Attr.name "chocolate"; Attr.value "yes" ] ]
        Elem.label [] [
            Text.raw "No"
            Elem.input [ Attr.typeRadio; Attr.name "chocolate"; Attr.value "no" ] ]
    ]

    Elem.fieldset [] [
        Elem.legend [] [ Text.raw "Subscribe to our newsletter" ]
        Elem.label [] [
            Text.raw "Receive updates about product"
            Elem.input [ Attr.typeCheckbox; Attr.name "newsletter"; Attr.value "product" ] ]
        Elem.label [] [
            Text.raw "Receive updates about company"
            Elem.input [ Attr.typeCheckbox; Attr.name "newsletter"; Attr.value "company" ] ]
    ]

    Elem.input [ Attr.typeSubmit ]
]
```

A simple but useful _meta_-element `Elem.control` can reduce the verbosity required to create form outputs. The same form would look like:

```fsharp
Elem.form [ Attr.method "post"; Attr.action "/submit" ] [
    Elem.control "name" [] [ Text.raw "Name" ]

    Elem.controlTextarea "bio" [] [ Text.raw "Bio" ] []

    Elem.controlSelect "hobbies" [ Attr.multiple ] [ Text.raw "Hobbies" ] [
        Elem.option [ Attr.value "programming" ] [ Text.raw "Programming" ]
        Elem.option [ Attr.value "diy" ] [ Text.raw "DIY" ]
        Elem.option [ Attr.value "basketball" ] [ Text.raw "Basketball" ]
    ]

    Elem.fieldset [] [
        Elem.legend [] [ Text.raw "Do you like chocolate?" ]
        Elem.control "chocolate" [ Attr.id "chocolate_yes"; Attr.typeRadio ] [ Text.raw "yes" ]
        Elem.control "chocolate" [ Attr.id "chocolate_no"; Attr.typeRadio ] [ Text.raw "no" ]
    ]

    Elem.fieldset [] [
        Elem.legend [] [ Text.raw "Subscribe to our newsletter" ]
        Elem.control "newsletter" [ Attr.id "newsletter_product"; Attr.typeCheckbox ] [ Text.raw "Receive updates about product" ]
        Elem.control "newsletter" [ Attr.id "newsletter_company"; Attr.typeCheckbox ] [ Text.raw "Receive updates about company" ]
    ]

    Elem.input [ Attr.typeSubmit ]
]
```

### Merging Attributes

The markup module allows you to easily create components, an excellent way to reduce code repetition in your UI. To support runtime customization, it is advisable to ensure components (or reusable markup blocks) retain a similar function "shape" to standard elements. That being, `XmlAttribute list -> XmlNode list -> XmlNode`.

This means that you will inevitably end up needing to combine your predefined `XmlAttribute list` with a list provided at runtime. To facilitate this, the `Attr.merge` function will group attributes by key, and intelligently concatenate the values in the case of additive attributes (i.e., `class`, `style` and `accept`).

```fsharp
open Falco.Markup

// Components
let heading (attrs : XmlAttribute list) (content : XmlNode list) =
    // safely combine the default XmlAttribute list with those provided
    // at runtime
    let attrs' =
        Attr.merge [ Attr.class' "text-large" ] attrs

    Elem.div [] [
        Elem.h1 [ attrs' ] content
    ]

// Template
let master (title : string) (content : XmlNode list) =
    Elem.html [ Attr.lang "en" ] [
        Elem.head [] [
            Elem.title [] [ Text.raw title ]
        ]
        Elem.body [] content
    ]

// Views
let homepage =
    master "Homepage" [
        heading [ Attr.class' "red" ] [ Text.raw "Welcome to the homepage" ]
        Text.p "Lorem ipsum dolor sit amet, consectetur adipiscing."
    ]

let homepage =
    master "About Us" [
        heading [ Attr.class' "purple" ] [ Text.raw "This is what we're all about" ]
        Text.p "Lorem ipsum dolor sit amet, consectetur adipiscing."
    ]
```

## Custom Elements & Attributes

Every effort has been taken to ensure the HTML and SVG specs are mapped to functions in the module. In the event an element or attribute you need is missing, you can either file an [issue](https://github.com/pimbrouwers/Falco.Markup/issues), or more simply extend the module in your project.

An example creating custom XML elements and using them to create a structured XML document:

```fsharp
open Falco.Makrup

module Elem =
    let books = Elem.create "books"
    let book = Elem.create "book"
    let name = Elem.create "name"

module Attr =
    let soldOut = Attr.createBool "soldOut"

let xmlDoc =
    Elem.books [] [
        Elem.book [ Attr.soldOut ] [
            Elem.name [] [ Text.raw "To Kill A Mockingbird" ]
        ]
    ]

let xml = renderXml xmlDoc
```

## SVG

Much of the SVG spec has been mapped to element and attributes functions. There is also an SVG template to help initialize a new drawing with a valid viewbox.

```fsharp
open Falco.Markup
open Falco.Markup.Svg

// https://developer.mozilla.org/en-US/docs/Web/SVG/Element/text#example
let svgDrawing =
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

let svg = renderNode svgDrawing
```

## Performance

You'll find the result of a simple [benchmark](benchmarks/) below, where Falco.Markup is compared to native `StringBuilder` usage as well as some other markup libraries.

```shell
BenchmarkDotNet=v0.13.1, OS=Windows 10.0.19044.2604 (21H2)
Intel Core i7-7500U CPU 2.70GHz (Kaby Lake), 1 CPU, 4 logical and 2 physical cores
.NET SDK=7.0.201
  [Host]     : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT DEBUG
  DefaultJob : .NET 6.0.14 (6.0.1423.7309), X64 RyuJIT


|        Method |      Mean |     Error |    StdDev | Ratio | RatioSD |   Gen 0 | Allocated |
|-------------- |----------:|----------:|----------:|------:|--------:|--------:|----------:|
| StringBuilder |  2.419 us | 0.0481 us | 0.0591 us |  1.00 |    0.00 |  6.6643 |     14 KB |
|         Falco |  3.829 us | 0.0338 us | 0.0300 us |  1.58 |    0.04 |  8.1253 |     17 KB |
|       Giraffe |  7.402 us | 0.0735 us | 0.0688 us |  3.04 |    0.08 |  9.0027 |     18 KB |
|       Scriban | 26.125 us | 0.3734 us | 0.2915 us | 10.73 |    0.38 | 16.5405 |     34 KB |
```

## Find a bug?

There's an [issue](https://github.com/pimbrouwers/Falco.Markup/issues) for that.

## License

Built with ♥ by [Pim Brouwers](https://github.com/pimbrouwers) in Toronto, ON. Licensed under [Apache License 2.0](https://github.com/pimbrouwers/Falco.Markup/blob/master/LICENSE).
