[marketplace]: https://marketplace.visualstudio.com/items?itemName=MadsKristensen.ColorPreview
[vsixgallery]: http://vsixgallery.com/extension/EditorColorPreview.06059b78-ceae-4188-905d-be8877234e35/
[repo]: https://github.com/madskristensen/EditorColorPreview

# Color Preview for Visual Studio

[![Build](https://github.com/madskristensen/EditorColorPreview/actions/workflows/build.yaml/badge.svg)](https://github.com/madskristensen/EditorColorPreview/actions/workflows/build.yaml)

Download this extension from the [Visual Studio Marketplace][marketplace]
or get the [CI build][vsixgallery].

---

Shows a color preview in front of all named colors, hex, rgb and hsl values in CSS and JavaScript files.

![color preview](art/screenshot.png)<br />
**\*Figure 1**: Color preview in light theme and dark theme\*

## Supported colors

These color formats are supported:

- Named colors (e.g. `blue`)
- Hex 3 digits (e.g. `#ff0`)
- Hex 6 digits (e.g. `#ffff00`)
- Hex 8 digits (e.g. `#ffff00cc`)
- RGB
  - `rgb(255, 165, 0)`
  - `rgb(0% 50% 0%)`
  - `rgb(0 128.0 0)`
  - `rgb(0% 50% 0% / 0.25)` (Alpha channel)
- RGBA (e.g.
  - `rgba(255, 165, 0)`
  - `rgba(0% 50% 0%)`
  - `rgba(0 128.0 0)`
  - `rgba(0% 50% 0% / 0.25)`
- HSL
  - `hsl(9, 100%, 64%)`
  - `hsl(120 100% 25%)`
  - `hsl(120deg 100% 25%)`
  - `hsl(120 100% 25% / 0.25)`
  - `hsl(120 none none)`
- HSLA
  - `hsla(9, 100%, 64%, 0.7)`
  - `hsla(120 100% 25%)`
  - `hsla(120deg 100% 25%)`
  - `hsla(120 100% 25% / 0.25)`
  - `hsla(120 none none)`
- HWB
  - `hwb(120 0% 49.8039%)`
  - `hwb(0 0% 100%)`
  - `hwb(0 100% 100%)`
  - `hwb(120 30% 50% / 0.5)`
  - `hwb(none none none)`
- Lab (Colors are converted to sRGB. Some colors might not convert properly) [^1]
  - `lab(46.2775% -47.5621 48.5837)`
  - `lab(100% 0 0)`
  - `lab(70% -45 0)`
  - `lab(86.6146% -106.5599 102.8717)`
- LCH (Colors are converted to sRGB. Some colors might might not convert properly) [^1]
  - `lch(46.2775% 67.9892 134.3912)`
  - `lch(0% 0 0)`
  - `lch(50% 50 0)`
  - `lch(70% 45 -180)`
- OKLab (Colors are converted to sRGB. Some colors might not convert properly) [^1]
  - `oklab(51.975% -0.1403 0.10768)`
  - `oklab(0% 0 0)`
  - `oklab(100% 0 0)`
  - `oklab(50% 0.05 0)`
- OKLCH (Colors are converted to sRGB. Some colors might not convert properly) [^1]
  - `oklch(51.975% 0.17686 142.495)`
  - `oklch(0% 0 0)`
  - `oklch(100% 0 0)`
  - `oklch(50% 0.2 0)`

[^1]: A color may be a valid color but still be outside the range of colors that can be produced by an output device (a screen, projector, or printer). It is said to be out of gamut for that color space.

## How can I help?

If you enjoy using the extension, please give it a ★★★★★ rating on the [Visual Studio Marketplace][marketplace].

Should you encounter bugs or if you have feature requests, head on over to the [GitHub repo][repo] to open an issue if one doesn't already exist.

Pull requests are also very welcome, since I can't always get around to fixing all bugs myself. This is a personal passion project, so my time is limited.

Another way to help out is to [sponsor me on GitHub](https://github.com/sponsors/madskristensen).
