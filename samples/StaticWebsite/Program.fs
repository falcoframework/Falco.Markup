open System
open System.IO
open Falco.Markup

// Components
let divider =
    _hr [ _class_ "divider" ]

// Template
let master (title : string) (content : XmlNode list) =
    _html [ _lang_ "en" ] [
        _head [] [
            _title [] [ _text "Sample App" ]
        ]
        _body [] content
    ]

// Views
let homeView =
    master "Homepage" [
        _h1' "Homepage"
        divider
        _p' "Lorem ipsum dolor sit amet."
    ]

let aboutView =
    master "About Us" [
        _h1' "About"
        divider
        _p' "Lorem ipsum dolor sit amet."
    ]

// Generate website
let writeHtmlToFile (filename : string) (html : XmlNode) =
    File.WriteAllText(
        Path.Join(__SOURCE_DIRECTORY__, filename),
        renderHtml html)

writeHtmlToFile "_homepage.html" homeView
writeHtmlToFile "_about.html" aboutView
