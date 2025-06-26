namespace Falco.Markup

open System
open System.Collections.Generic
open System.Globalization

module Attr =
    /// XmlAttribute KeyValueAttr constructor
    let create (key : string) (value : string) =
        KeyValueAttr (key, value)

    /// XmlAttribute NonValueAttr constructor
    let createBool (key : string) =
        NonValueAttr key

    let private additiveAttributes =
        dict [
            "class", " "
            "style", "; "
            "accept", ", " ]

    /// Merge two XmlAttribute lists
    let merge (attrs1 : XmlAttribute list) (attrs2 : XmlAttribute list) =

        let joinValues (sep : string) v2 v1 = String.Concat([| v1; sep; v2 |])

        // convert left list into dictionary
        let merged = Dictionary<string, string option>()

        for attr in attrs1 do
            match attr with
            | NonValueAttr name          -> merged.Add(name, None)
            | KeyValueAttr (name, value) -> merged.Add(name, Some value)

        // check right list against dictionary, updating as appropriate
        for attr in attrs2 do
            match attr with
            | NonValueAttr name ->
                if not (merged.ContainsKey name) then
                    merged.Add(name, None)

            | KeyValueAttr (name, value) ->
                if merged.ContainsKey(name) && additiveAttributes.ContainsKey(name) then
                    let newValue = Option.map (joinValues additiveAttributes[name] value) merged[name]
                    merged[name] <- newValue
                elif merged.ContainsKey(name) then
                    merged[name] <- Some value
                else
                    merged.Add(name, Some value)

        // inputs are now merged, convert dictionary back into of XmlAttribute
        [
            for KeyValue (name, values) in merged do
                match values with
                | None ->
                    NonValueAttr name
                | Some value ->
                    KeyValueAttr (name, value)
        ]

    /// `accept={value}` attribute
    let accept = create "accept"

    /// `accept={value}` attribute
    let acceptCharset = create "accept-charset"

    /// `accesskey={value}` attribute
    let accesskey = create "accesskey"

    /// `action={value}` attribute
    let action = create "action"

    /// `align={value}` attribute
    let align = create "align"

    /// `allow={value}` attribute
    let allow = create "allow"

    /// `alt={value}` attribute
    let alt = create "alt"

    /// `async` attribute
    let async = createBool "async"

    /// `autocapitalize={value}` attribute
    let autocapitalize = create "autocapitalize"

    /// `autocomplete={value}` attribute
    let autocomplete = create "autocomplete"

    /// `autofocus` attribute
    let autofocus = createBool "autofocus"

    /// `autoplay` attribute
    let autoplay = createBool "autoplay"

    /// `background={value}` attribute
    let background = create "background"

    /// `bgcolor={value}` attribute
    let bgcolor = create "bgcolor"

    /// `border={value}` attribute
    let border = create "border"

    /// `buffered={value}` attribute
    let buffered = create "buffered"

    /// `capture={value}` attribute
    let capture = create "capture"

    /// `challenge={value}` attribute
    let challenge = create "challenge"

    /// `charset={value}` attribute
    let charset = create "charset"

    /// `checked` attribute
    let checked' = createBool "checked"

    /// `cite={value}` attribute
    let cite = create "cite"

    /// `class={value}` attribute
    let class' = create "class"

    /// `code={value}` attribute
    let code = create "code"

    /// `codebase={value}` attribute
    let codebase = create "codebase"

    /// `color={value}` attribute
    let color = create "color"

    /// `cols={value}` attribute
    let cols = create "cols"

    /// `colspan={value}` attribute
    let colspan = create "colspan"

    /// `content={value}` attribute
    let content = create "content"

    /// `contenteditable={value}` attribute
    let contenteditable = create "contenteditable"

    /// `contextmenu={value}` attribute
    let contextmenu = create "contextmenu"

    /// `controls` attribute
    let controls = createBool "controls"

    /// `coords={value}` attribute
    let coords = create "coords"

    /// `crossorigin={value}` attribute
    let crossorigin = create "crossorigin"

    /// `csp={value}` attribute
    let csp = create "csp"

    /// `data={value}` attribute
    let data = create "data"
    let dataAttr name = create (String.Concat [ "data-"; name ])

    /// `datetime={value}` attribute
    let datetime = create "datetime"

    /// `decoding={value}` attribute
    let decoding = create "decoding"

    /// `default` attribute
    let default' = createBool "default"

    /// `defer` attribute
    let defer = createBool "defer"

    /// `dir={value}` attribute
    let dir = create "dir"

    /// `dirname={value}` attribute
    let dirname = create "dirname"

    /// `disabled` attribute
    let disabled = createBool "disabled"

    /// `download={value}` attribute
    let download = create "download"

    /// `draggable={value}` attribute
    let draggable = create "draggable"

    /// `enctype={value}` attribute
    let enctype = create "enctype"

    /// `enterkeyhint={value}` attribute
    let enterkeyhint = create "enterkeyhint"

    /// `for={value}` attribute
    let for' = create "for"

    /// `form={value}` attribute
    let form = create "form"

    /// `formaction={value}` attribute
    let formaction = create "formaction"

    /// `formenctype={value}` attribute
    let formenctype = create "formenctype"

    /// `formmethod={value}` attribute
    let formmethod = create "formmethod"

    /// `formnovalidate` attribute
    let formnovalidate = createBool "formnovalidate"

    /// `formtarget={value}` attribute
    let formtarget = create "formtarget"

    /// `headers={value}` attribute
    let headers = create "headers"

    /// `height={value}` attribute
    let height = create "height"

    /// `hidden` attribute
    let hidden = createBool "hidden"

    /// `high={value}` attribute
    let high = create "high"

    /// `href={value}` attribute
    let href = create "href"

    /// `hreflang={value}` attribute
    let hreflang = create "hreflang"

    /// `http={value}` attribute
    let httpEquiv = create "http-equiv"

    /// `icon={value}` attribute
    let icon = create "icon"

    /// `id={value}` attribute
    let id = create "id"

    /// `importance={value}` attribute
    let importance = create "importance"

    /// `integrity={value}` attribute
    let integrity = create "integrity"

    /// `inputmode={value}` attribute
    let inputmode = create "inputmode"

    /// `ismap` attribute
    let ismap = createBool "ismap"

    /// `itemprop={value}` attribute
    let itemprop = create "itemprop"

    /// `keytype={value}` attribute
    let keytype = create "keytype"

    /// `kind={value}` attribute
    let kind = create "kind"

    /// `label={value}` attribute
    let label = create "label"

    /// `lang={value}` attribute
    let lang = create "lang"

    /// `loading={value}` attribute
    let loading = create "loading"

    /// `list={value}` attribute
    let list = create "list"

    /// `loop` attribute
    let loop = createBool "loop"

    /// `low={value}` attribute
    let low = create "low"

    /// `max={value}` attribute
    let max = create "max"

    /// `maxlength={value}` attribute
    let maxlength = create "maxlength"

    /// `minlength={value}` attribute
    let minlength = create "minlength"

    /// `media={value}` attribute
    let media = create "media"

    /// `method={value}` attribute
    let method = create "method"

    /// `min={value}` attribute
    let min = create "min"

    /// `multiple` attribute
    let multiple = createBool "multiple"

    /// `muted` attribute
    let muted = createBool "muted"

    /// `name={value}` attribute
    let name = create "name"

    /// `novalidate` attribute
    let novalidate = createBool "novalidate"

    /// `open={value}` attribute
    let open' = create "open"

    /// `optimum={value}` attribute
    let optimum = create "optimum"

    /// `pattern={value}` attribute
    let pattern = create "pattern"

    /// `ping={value}` attribute
    let ping = create "ping"

    /// `placeholder={value}` attribute
    let placeholder = create "placeholder"

    /// `poster={value}` attribute
    let poster = create "poster"

    /// `preload={value}` attribute
    let preload = create "preload"

    /// `radiogroup={value}` attribute
    let radiogroup = create "radiogroup"

    /// `readonly` attribute
    let readonly = createBool "readonly"

    /// `referrerpolicy={value}` attribute
    let referrerpolicy = create "referrerpolicy"

    /// `rel={value}` attribute
    let rel = create "rel"

    /// `required` attribute
    let required = createBool "required"

    /// `reversed` attribute
    let reversed = createBool "reversed"

    /// `role={value}` attribute
    let role = create "role"

    /// `rows={value}` attribute
    let rows = create "rows"

    /// `rowspan={value}` attribute
    let rowspan = create "rowspan"

    /// `sandbox={value}` attribute
    let sandbox = create "sandbox"

    /// `scope={value}` attribute
    let scope = create "scope"

    /// `selected` attribute
    let selected = createBool "selected"

    /// `shape={value}` attribute
    let shape = create "shape"

    /// `size={value}` attribute
    let size = create "size"

    /// `sizes={value}` attribute
    let sizes = create "sizes"

    /// `slot={value}` attribute
    let slot = create "slot"

    /// `span={value}` attribute
    let span = create "span"

    /// `spellcheck={value}` attribute
    let spellcheck = create "spellcheck"

    /// `src={value}` attribute
    let src = create "src"

    /// `srcdoc={value}` attribute
    let srcdoc = create "srcdoc"

    /// `srclang={value}` attribute
    let srclang = create "srclang"

    /// `srcset={value}` attribute
    let srcset = create "srcset"

    /// `start={value}` attribute
    let start = create "start"

    /// `step={value}` attribute
    let step = create "step"

    /// `style={value}` attribute
    let style = create "style"

    /// `tabindex={value}` attribute
    let tabindex = create "tabindex"

    /// `target={value}` attribute
    let target = create "target"

    /// `title={value}` attribute
    let title = create "title"

    /// `translate={value}` attribute
    let translate = create "translate"

    /// `type={value}` attribute
    let type' = create "type"

    /// `usemap={value}` attribute
    let usemap = create "usemap"

    /// `value={value}` attribute
    let value = create "value"

    /// `width={value}` attribute
    let width = create "width"

    /// `wrap={value}` attribute
    let wrap = create "wrap"


    //
    // Aliases

    /// Alias for `enctype=application/x-www-form-urlencoded`
    let enctypeForm = enctype "application/x-www-form-urlencoded"

    /// Alias for `enctype=multipart/form-data`
    let enctypeMultipart = enctype "multipart/form-data"

    // Alias for `method=post`
    let methodPost = method "post"

    // Alias for `method=get`
    let methodGet = method "get"

    // Alias for `method=dialog`
    let methodDialog = method "dialog"

    /// Alias for `target=_self`
    let targetSelf = target "_self"

    /// Alias for `target=_blank`
    let targetBlank = target "_blank"

    /// Alias for `target=_parent`
    let targetParent = target "_parent"

    /// Alias for `target=_top`
    let targetTop = target "_top"

    /// Alias for `target=_unfencedTop`
    let targetUnfencedTop = target "_unfencedTop"

    /// Alias for `type=button`
    let typeButton = type' "button"

    /// Alias for `type=checkbox`
    let typeCheckbox = type' "checkbox"

    /// Alias for `type=color`
    let typeColor = type' "color"

    /// Alias for `type=date`
    let typeDate = type' "date"

    /// Alias for `type=datetime-local`
    let typeDatetimeLocal = type' "datetime-local"

    /// Alias for `type=email`
    let typeEmail = type' "email"

    /// Alias for `type=file`
    let typeFile = type' "file"

    /// Alias for `type=hidden`
    let typeHidden = type' "hidden"

    /// Alias for `type=image`
    let typeImage = type' "image"

    /// Alias for `type=month`
    let typeMonth = type' "month"

    /// Alias for `type=number`
    let typeNumber = type' "number"

    /// Alias for `type=password`
    let typePassword = type' "password"

    /// Alias for `type=radio`
    let typeRadio = type' "radio"

    /// Alias for `type=range`
    let typeRange = type' "range"

    /// Alias for `type=reset`
    let typeReset = type' "reset"

    /// Alias for `type=search`
    let typeSearch = type' "search"

    /// Alias for `type=submit`
    let typeSubmit = type' "submit"

    /// Alias for `type=tel`
    let typeTel = type' "tel"

    /// Alias for `type=text`
    let typeText = type' "text"

    /// Alias for `type=time`
    let typeTime = type' "time"

    /// Alias for `type=url`
    let typeUrl = type' "url"

    /// Alias for `type=week`
    let typeWeek = type' "week"

    /// Alias for `value=""`.
    let valueEmpty = value ""

    /// Alias for `value={str}` by invoking `_.ToString()` on input
    let inline valueString x =
        value (x.ToString())

    /// Alias for `value={str}` by invoking `_.ToString(format)` on input
    let inline valueStringf format (x : ^T when ^T : (member ToString : string -> string)) =
        value (x.ToString(format))

    /// Alias for `Attr.valueStringf "yyyy-MM-dd" dt`
    let valueDate (dt : DateTime) =
        valueStringf "yyyy-MM-dd" dt

    /// Alias for `Attr.valueStringf "s" dt`
    let valueDatetimeLocal (dt : DateTime) =
        valueStringf "s" dt

    /// Alias for `Attr.valueStringf "yyyy-MM"`
    let valueMonth (dt : DateTime) =
        valueStringf "yyyy-MM" dt

    /// Alias for `Attr.valueStringf "hh\:mm" time`
    let valueTime (time : TimeSpan) =
        valueStringf "hh\:mm" time

    let private cal = GregorianCalendar(GregorianCalendarTypes.USEnglish)

    /// Alias for `value={yyyy-W#}` (ex: `value=1986-W50`)
    let valueWeek (dt : DateTime) =
        let wk = cal.GetWeekOfYear(dt, CalendarWeekRule.FirstDay, DayOfWeek.Monday).ToString("00")
        let yr = dt.ToString("yyyy")
        value (sprintf "%s-W%s" yr wk)

    /// Invoke `fn` if `Some` otherwise return `value=""`
    let valueOption fn opt =
        match opt with Some x -> fn x | None -> valueEmpty

    //
    // Events

    /// `onabort={value}` event
    let onabort = create "onabort"

    /// `afterprint={value}` event
    let onafterprint = create "afterprint"

    /// `animationend={value}` event
    let onanimationend = create "animationend"

    /// `animationiteration={value}` event
    let onanimationiteration = create "animationiteration"

    /// `animationstart={value}` event
    let onanimationstart = create "animationstart"

    /// `beforeprint={value}` event
    let onbeforeprint = create "beforeprint"

    /// `beforeunload={value}` event
    let onbeforeunload = create "beforeunload"

    /// `onblur={value}` event
    let onblur = create "onblur"

    /// `oncanplay={value}` event
    let oncanplay = create "oncanplay"

    /// `oncanplaythrough={value}` event
    let oncanplaythrough = create "oncanplaythrough"

    /// `onchange={value}` event
    let onchange = create "onchange"

    /// `onclick={value}` event
    let onclick = create "onclick"

    /// `oncontextmenu={value}` event
    let oncontextmenu = create "oncontextmenu"

    /// `copy={value}` event
    let oncopy = create "copy"

    /// `cut={value}` event
    let oncut = create "cut"

    /// `ondblclick={value}` event
    let ondblclick = create "ondblclick"

    /// `ondrag={value}` event
    let ondrag = create "ondrag"

    /// `ondragend={value}` event
    let ondragend = create "ondragend"

    /// `ondragenter={value}` event
    let ondragenter = create "ondragenter"

    /// `ondragleave={value}` event
    let ondragleave = create "ondragleave"

    /// `ondragover={value}` event
    let ondragover = create "ondragover"

    /// `ondragstart={value}` event
    let ondragstart = create "ondragstart"

    /// `ondrop={value}` event
    let ondrop = create "ondrop"

    /// `ondurationchange={value}` event
    let ondurationchange = create "ondurationchange"

    /// `onemptied={value}` event
    let onemptied = create "onemptied"

    /// `onended={value}` event
    let onended = create "onended"

    /// `onerror={value}` event
    let onerror = create "onerror"

    /// `onfocus={value}` event
    let onfocus = create "onfocus"

    /// `onformchange={value}` event
    let onformchange = create "onformchange"

    /// `onforminput={value}` event
    let onforminput = create "onforminput"

    /// `focusin={value}` event
    let onfocusin = create "focusin"

    /// `focusout={value}` event
    let onfocusout = create "focusout"

    /// `fullscreenchange={value}` event
    let onfullscreenchange = create "fullscreenchange"

    /// `fullscreenerror={value}` event
    let onfullscreenerror = create "fullscreenerror"

    /// `hashchange={value}` event
    let onhashchange = create "hashchange"

    /// `oninput={value}` event
    let oninput = create "oninput"

    /// `oninvalid={value}` event
    let oninvalid = create "oninvalid"

    /// `onkeydown={value}` event
    let onkeydown = create "onkeydown"

    /// `onkeypress={value}` event
    let onkeypress = create "onkeypress"

    /// `onkeyup={value}` event
    let onkeyup = create "onkeyup"

    /// `onload={value}` event
    let onload = create "onload"

    /// `onloadeddata={value}` event
    let onloadeddata = create "onloadeddata"

    /// `onloadedmetadata={value}` event
    let onloadedmetadata = create "onloadedmetadata"

    /// `onloadstart={value}` event
    let onloadstart = create "onloadstart"

    /// `message={value}` event
    let onmessage = create "message"

    /// `onmousedown={value}` event
    let onmousedown = create "onmousedown"

    /// `onmouseenter={value}` event
    let onmouseenter = create "onmouseenter"

    /// `onmouseleave={value}` event
    let onmouseleave = create "onmouseleave"

    /// `onmousemove={value}` event
    let onmousemove = create "onmousemove"

    /// `onmouseover={value}` event
    let onmouseover = create "onmouseover"

    /// `onmouseout={value}` event
    let onmouseout = create "onmouseout"

    /// `onmouseup={value}` event
    let onmouseup = create "onmouseup"

    /// `onmousewheel={value}` event
    let onmousewheel = create "onmousewheel"

    /// `offline={value}` event
    let onoffline = create "offline"

    /// `online={value}` event
    let ononline = create "online"

    /// `open={value}` event
    let onopen = create "open"

    /// `pagehide={value}` event
    let onpagehide = create "pagehide"

    /// `pageshow={value}` event
    let onpageshow = create "pageshow"

    /// `paste={value}` event
    let onpaste = create "paste"

    /// `onpause={value}` event
    let onpause = create "onpause"

    /// `onplay={value}` event
    let onplay = create "onplay"

    /// `onplaying={value}` event
    let onplaying = create "onplaying"

    /// `popstate={value}` event
    let onpopstate = create "popstate"

    /// `onprogress={value}` event
    let onprogress = create "onprogress"

    /// `onratechange={value}` event
    let onratechange = create "onratechange"

    /// `onresize={value}` event
    let onresize = create "onresize"

    /// `onreset={value}` event
    let onreset = create "onreset"

    /// `onscroll={value}` event
    let onscroll = create "onscroll"

    /// `search={value}` event
    let onsearch = create "search"

    /// `onseeked={value}` event
    let onseeked = create "onseeked"

    /// `onseeking={value}` event
    let onseeking = create "onseeking"

    /// `onselect={value}` event
    let onselect = create "onselect"

    /// `onshow={value}` event
    let onshow = create "onshow"

    /// `onstalled={value}` event
    let onstalled = create "onstalled"

    /// `storage={value}` event
    let onstorage = create "storage"

    /// `onsubmit={value}` event
    let onsubmit = create "onsubmit"

    /// `onsuspend={value}` event
    let onsuspend = create "onsuspend"

    /// `ontimeupdate={value}` event
    let ontimeupdate = create "ontimeupdate"

    /// `toggle={value}` event
    let ontoggle = create "toggle"

    /// `touchcancel={value}` event
    let ontouchcancel = create "touchcancel"

    /// `touchend={value}` event
    let ontouchend = create "touchend"

    /// `touchmove={value}` event
    let ontouchmove = create "touchmove"

    /// `touchstart={value}` event
    let ontouchstart = create "touchstart"

    /// `transitionend={value}` event
    let ontransitionend = create "transitionend"

    /// `unload={value}` event
    let onunload = create "unload"

    /// `onvolumechange={value}` event
    let onvolumechange = create "onvolumechange"

    /// `onwaiting={value}` event
    let onwaiting = create "onwaiting"

    /// `wheel={value}` event
    let onwheel = create "wheel"
