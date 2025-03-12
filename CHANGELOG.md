# Changelog

All notable changes to this project will be documented in this file.

## [1.1.1] 2024-03-28

###

- `Attr.valueEmpty`, alias for `value=""`.
- `Attr.valueString`, alias for `value={str}` by invoking `_.ToString()` on input.
- `Attr.valueStringf`, ar `value={str}` by invoking `_.ToString(format)` on input.
- `Attr.valueDate`, alias for `Attr.valueStringf "yyyy-MM-dd" dt`.
- `Attr.valueDatetimeLocal`, alias for `Attr.valueStringf "s" dt`.
- `Attr.valueMonth`, alias for `Attr.valueStringf "yyyy-MM"`.
- `Attr.valueTime`, alias for `Attr.valueStringf "hh\:mm" time` .
- `Attr.valueWeek`, alias for `value={yyyy-W#}` (ex: `value=1986-W50`).

## [1.1.0] 2024-03-25

### Added

- `Text` module shortcuts for [block and inline text elements](https://github.com/pimbrouwers/Falco.Markup/commit/698dc18c0cff68963d940acad6f3ef6236408aa3).
    - Ex: `Elem.p [] [ Text.raw "here" ]` becomes `Text.p "here"`.
- [`Elem.control` function](https://github.com/pimbrouwers/Falco.Markup/commit/d37781f00b8a03e81d6d91f2b5d3de8d3ea3b06b#diff-818bc527331da89889c0b1327a3ab4246c46da1765f3316c3d7524e25d9e47f1R367) to created [labelled controls](https://developer.mozilla.org/en-US/docs/Web/HTML/Element/label).
- Aliases for common value-restricted attributes.
    - [Input type aliases](https://github.com/pimbrouwers/Falco.Markup/commit/c178c2b32b21945c64efb6fbfbcb4063249c5926#diff-77db9ea3c76df965322b32745ae581fa169b279d8ee42f882e76787c1e9a4e44R443)
    - [Form attribute aliases](https://github.com/pimbrouwers/Falco.Markup/commit/9f8a50623b1b7004f8ced3357b1656048be053d5)
    - [Target aliases](https://github.com/pimbrouwers/Falco.Markup/commit/118e8fd47d6418143ef76b904413634976f76448#diff-77db9ea3c76df965322b32745ae581fa169b279d8ee42f882e76787c1e9a4e44R443).

### Fixed

- Optimized `Attr.merge` to [smartly combine attributes](https://github.com/pimbrouwers/Falco.Markup/commit/c178c2b32b21945c64efb6fbfbcb4063249c5926), concatenating additive attributes (i.e., class, style etc.) replacing when not.

## [1.0.2] 2023-02-03

### Fixed

- [MaciekWin3](https://github.com/pimbrouwers/Falco.Markup/pull/4) - Missing "on" prefix for JavaScript handlers.

## [1.0.1] 2022-09-14

### Changed

- Minimum FSharp.Core version dropped to 4.5.2.

## [1.0.0] 2022-09-07

- Exported from core [Falco](https://github.com/pimbrouwers/Falco) library.