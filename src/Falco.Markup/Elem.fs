namespace Falco.Markup

module Elem =
    /// Standard XmlNode constructor
    let create (tag : string) (attr : XmlAttribute list) (children : XmlNode list) =
        ParentNode ((tag, attr), children)

    /// Self-closing XmlNode constructor
    let createSelfClosing (tag : string) (attr : XmlAttribute list) =
        SelfClosingNode (tag, attr)

    //
    // Main root

    /// `<html>` element
    let html = create "html"

    //
    // Document metadata

    /// `<base>` element
    let base' = createSelfClosing "base"

    /// `<head>` element
    let head = create "head"

    /// `<link>` element
    let link = createSelfClosing "link"

    /// `<meta>` element
    let meta = createSelfClosing "meta"

    /// `<style>` element
    let style = create "style"

    /// `<title>` element
    let title = create "title"

    //
    // Sectioning root

    /// `<body>` element
    let body = create "body"

    //
    // Content sectioning

    /// `<address>` element
    let address = create "address"

    /// `<article>` element
    let article = create "article"

    /// `<aside>` element
    let aside = create "aside"

    /// `<footer>` element
    let footer = create "footer"

    /// `<header>` element
    let header = create "header"

    /// `<h1>` element
    let h1 = create "h1"

    /// `<h2>` element
    let h2 = create "h2"

    /// `<h3>` element
    let h3 = create "h3"

    /// `<h4>` element
    let h4 = create "h4"

    /// `<h5>` element
    let h5 = create "h5"

    /// `<h6>` element
    let h6 = create "h6"

    /// `<main>` element
    let main = create "main"

    /// `<nav>` element
    let nav = create "nav"

    /// `<section>` element
    let section = create "section"

    //
    // Text content

    /// `<blockquote>` element
    let blockquote = create "blockquote"

    /// `<dd>` element
    let dd = create "dd"

    /// `<div>` element
    let div = create "div"

    /// `<dl>` element
    let dl = create "dl"

    /// `<dt>` element
    let dt = create "dt"

    /// `<figcaption>` element
    let figcaption = create "figcaption"

    /// `<figure>` element
    let figure = create "figure"

    /// `<hr>` element
    let hr = createSelfClosing "hr"

    /// `<li>` element
    let li = create "li"

    /// `<menu>` element
    let menu = create "menu"

    /// `<ol>` element
    let ol = create "ol"

    /// `<p>` element
    let p = create "p"

    /// `<pre>` element
    let pre = create "pre"

    /// `<ul>` element
    let ul = create "ul"

    //
    // Inline text semantics

    /// `<a>` element
    let a = create "a"

    /// `<abbr>` element
    let abbr = create "abbr"

    /// `<b>` element
    let b = create "b"

    /// `<bdi>` element
    let bdi = create "bdi"

    /// `<bdo>` element
    let bdo = create "bdo"

    /// `<br>` element
    let br = createSelfClosing "br"

    /// `<cite>` element
    let cite = create "cite"

    /// `<code>` element
    let code = create "code"

    /// `<data>` element
    let data = create "data"

    /// `<dfn>` element
    let dfn = create "dfn"

    /// `<em>` element
    let em = create "em"

    /// `<i>` element
    let i = create "i"

    /// `<kbd>` element
    let kbd = create "kbd"

    /// `<mark>` element
    let mark = create "mark"

    /// `<q>` element
    let q = create "q"

    /// `<rp>` element
    let rp = create "rp"

    /// `<rt>` element
    let rt = create "rt"

    /// `<ruby>` element
    let ruby = create "ruby"

    /// `<s>` element
    let s = create "s"

    /// `<samp>` element
    let samp = create "samp"

    /// `<small>` element
    let small = create "small"

    /// `<span>` element
    let span = create "span"

    /// `<strong>` element
    let strong = create "strong"

    /// `<sub>` element
    let sub = create "sub"

    /// `<sup>` element
    let sup = create "sup"

    /// `<time>` element
    let time = create "time"

    /// `<u>` element
    let u = create "u"

    /// `<var>` element
    let var = create "var"

    /// `<wbr>` element
    let wbr = createSelfClosing "wbr"

    //
    // Image and multimedia

    /// `<area>` element
    let area = create "area"

    /// `<audio>` element
    let audio = create "audio"

    /// `<img>` element
    let img = createSelfClosing "img"

    /// `<map>` element
    let map = create "map"

    /// `<track>` element
    let track = createSelfClosing "track"

    /// `<video>` element
    let video = create "video"

    //
    // Embedded content

    /// `<embed>` element
    let embed = createSelfClosing "embed"

    /// `<iframe>` element
    let iframe = create "iframe"

    /// `<object>` element
    let object = create "object"

    /// `<picture>` element
    let picture = create "picture"

    /// `<portal>` element
    let portal = create "portal"

    /// `<source>` element
    let source = createSelfClosing "source"


    //
    // Scripting

    /// `<canvas>` element
    let canvas = create "canvas"

    /// `<noscript>` element
    let noscript = create "noscript"

    /// `<script>` element
    let script = create "script"

    //
    // Demarcating edits

    /// `<del>` element
    let del = create "del"

    /// `<ins>` element
    let ins = create "ins"

    //
    // Table content

    /// `<caption>` element
    let caption = create "caption"

    /// `<col>` element
    let col = createSelfClosing "col"

    /// `<colgroup>` element
    let colgroup = create "colgroup"

    /// `<table>` element
    let table = create "table"

    /// `<tbody>` element
    let tbody = create "tbody"

    /// `<td>` element
    let td = create "td"

    /// `<tfoot>` element
    let tfoot = create "tfoot"

    /// `<th>` element
    let th = create "th"

    /// `<thead>` element
    let thead = create "thead"

    /// `<tr>` element
    let tr = create "tr"

    //
    // Forms

    /// `<button>` element
    let button = create "button"

    /// `<datalist>` element
    let datalist = create "datalist"

    /// `<fieldset>` element
    let fieldset = create "fieldset"

    /// `<form>` element
    let form = create "form"

    /// `<input>` element
    let input = createSelfClosing "input"

    /// `<label>` element
    let label = create "label"

    /// `<legend>` element
    let legend = create "legend"

    /// `<meter>` element
    let meter = create "meter"

    /// `<optgroup>` element
    let optgroup = create "optgroup"

    /// `<option>` element
    let option = create "option"

    /// `<output>` element
    let output = create "output"

    /// `<progress>` element
    let progress = create "progress"

    /// `<select>` element
    let select = create "select"

    /// `<textarea>` element
    let textarea = create "textarea"

    let private control'
        (node : XmlAttribute list -> XmlNode)
        (name : string)
        (attr : XmlAttribute list)
        (labelElement : XmlNode list) =
        label [ Attr.for' name ] [
            yield! labelElement
            node <| Attr.merge [ Attr.id name; Attr.name name ] attr ]

    /// Labelled control element (i.e., `<label for="name">Name<input id="name" name="name" /></label>`).
    let control = control' input

    /// Labelled select element (i.e., `<label for="item">Item<select>...</select></label>`)
    let controlSelect name attr labelElement options = control' (fun attr -> select attr options) name attr labelElement

    /// Labelled textarea element (i.e., `<label for="item">Item<textarea>...</textarea></label>`)
    let controlTextarea name attr labelElement content = control' (fun attr -> textarea attr content) name attr labelElement

    //
    // Interactive elements

    /// `<details>` element
    let details = create "details"

    /// `<dialog>` element
    let dialog = create "dialog"

    /// `<summary>` element
    let summary = create "summary"

    //
    // Web Components

    /// `<slot>` element
    let slot = create "slot"

    /// `<template>` element
    let template = create "template"

    //
    // SVG and MathML

    /// `<math>` element
    let math = create "math"

    /// `<svg>` element
    let svg = create "svg"
