using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

// From https://stackoverflow.com/a/61871235
public static class ColorUtils
{
    public static readonly Regex _regex = new(@"(?<=.*:.*)(#(?:[0-9a-f]{2}){2,4}\b|(#[0-9a-f]{3})\b|\b(rgb|hsl|hwb)a?\(((\d+(%|deg)?|none)(,|,\s*|\s*)){2}(\d+(%|deg)?|none){1}\s*[\/\d\.]*%?\s*([\d\.]*)*\)|(?<=[\s""'])(black|silver|gray|whitesmoke|maroon|red|purple|fuchsia|green|lime|olivedrab|yellow|navy|blue|teal|aquamarine|orange|aliceblue|antiquewhite|aqua|azure|beige|bisque|blanchedalmond|blueviolet|brown|burlywood|cadetblue|chartreuse|chocolate|coral|cornflowerblue|cornsilk|crimson|darkblue|darkcyan|darkgoldenrod|darkgray|darkgreen|darkgrey|darkkhaki|darkmagenta|darkolivegreen|darkorange|darkorchid|darkred|darksalmon|darkseagreen|darkslateblue|darkslategray|darkslategrey|darkturquoise|darkviolet|deeppink|deepskyblue|dimgray|dimgrey|dodgerblue|firebrick|floralwhite|forestgreen|gainsboro|ghostwhite|goldenrod|gold|greenyellow|grey|honeydew|hotpink|indianred|indigo|ivory|khaki|lavenderblush|lavender|lawngreen|lemonchiffon|lightblue|lightcoral|lightcyan|lightgoldenrodyellow|lightgray|lightgreen|lightgrey|lightpink|lightsalmon|lightseagreen|lightskyblue|lightslategray|lightslategrey|lightsteelblue|lightyellow|limegreen|linen|mediumaquamarine|mediumblue|mediumorchid|mediumpurple|mediumseagreen|mediumslateblue|mediumspringgreen|mediumturquoise|mediumvioletred|midnightblue|mintcream|mistyrose|moccasin|navajowhite|oldlace|olive|orangered|orchid|palegoldenrod|palegreen|paleturquoise|palevioletred|papayawhip|peachpuff|peru|pink|plum|powderblue|rosybrown|royalblue|saddlebrown|salmon|sandybrown|seagreen|seashell|sienna|skyblue|slateblue|slategray|slategrey|snow|springgreen|steelblue|tan|thistle|tomato|transparent|turquoise|violet|wheat|white|yellowgreen|rebeccapurple))", RegexOptions.Compiled | RegexOptions.IgnoreCase);

    public static Color HtmlToColor(string htmlColor)
    {
        try
        {
            // RGB
            if (htmlColor.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                return ArgbToColor(htmlColor);
            }

            // HSL
            if (htmlColor.StartsWith("hsl", StringComparison.OrdinalIgnoreCase))
            {
                return HslToColor(htmlColor);
            }

            // HWB
            if (htmlColor.StartsWith("hwb", StringComparison.OrdinalIgnoreCase))
            {
                return HwbToColor(htmlColor);
            }

            // HEX
            if (htmlColor.StartsWith("#", StringComparison.Ordinal))
            {
                return HexToColor(htmlColor);
            }

            // Named colors
            if (htmlColor.Equals("rebeccapurple", StringComparison.OrdinalIgnoreCase))
            {
                htmlColor = "#663399";
            }

            return ColorTranslator.FromHtml(htmlColor);

        }
        catch
        {
            return Color.Empty;
        }
    }

    public static MatchCollection MatchesColor(string html)
    {
        return _regex.Matches(html);
    }

    private static Color HexToColor(string htmlColor)
    {
        int len = htmlColor.Length;

        // #RGB
        if (len == 4)
        {
            int r = Convert.ToInt32(htmlColor.Substring(1, 1), 16);
            int g = Convert.ToInt32(htmlColor.Substring(2, 1), 16);
            int b = Convert.ToInt32(htmlColor.Substring(3, 1), 16);

            return Color.FromArgb(r + (r * 16), g + (g * 16), b + (b * 16));
        }

        // #RGBA
        else if (len == 5)
        {
            int r = Convert.ToInt32(htmlColor.Substring(1, 1), 16);
            int g = Convert.ToInt32(htmlColor.Substring(2, 1), 16);
            int b = Convert.ToInt32(htmlColor.Substring(3, 1), 16);
            int a = Convert.ToInt32(htmlColor.Substring(4, 1), 16);

            return Color.FromArgb(a + (a * 16), r + (r * 16), g + (g * 16), b + (b * 16));
        }

        // #RRGGBB
        else if (len == 7)
        {
            return Color.FromArgb(
                Convert.ToInt32(htmlColor.Substring(1, 2), 16),
                Convert.ToInt32(htmlColor.Substring(3, 2), 16),
                Convert.ToInt32(htmlColor.Substring(5, 2), 16));
        }

        // #RRGGBBAA
        else if (len == 9)
        {
            return Color.FromArgb(
                Convert.ToInt32(htmlColor.Substring(7, 2), 16),
                Convert.ToInt32(htmlColor.Substring(1, 2), 16),
                Convert.ToInt32(htmlColor.Substring(3, 2), 16),
                Convert.ToInt32(htmlColor.Substring(5, 2), 16));
        }

        return Color.Empty;
    }

    private static Color ArgbToColor(string htmlColor)
    {
        string[] parts = ColorParts(htmlColor);

        double r = GetNumberOrPercentage(parts[0]);
        double g = GetNumberOrPercentage(parts[1]);
        double b = GetNumberOrPercentage(parts[2]);

        double a = parts.Length > 3 ? GetNumberOrPercentage(parts[3]) : 1.0;

        if (parts[0].Contains('%') || parts[1].Contains('%') || parts[2].Contains('%'))
        {
            return FromArgb(a, r, g, b);
        }
        else
        {
            return FromArgb(a, (int)r, (int)g, (int)b);
        }
    }

    private static Color HslToColor(string htmlColor)
    {
        string[] parts = ColorParts(htmlColor);

        double h = GetNumberOrPercentage(parts[0]);
        double s = GetNumberOrPercentage(parts[1]);
        double l = GetNumberOrPercentage(parts[2]);

        double[] rgb = HslToRgb(h, s, l);

        if (parts.Length == 3)
        {
            return FromArgb(rgb[0], rgb[1], rgb[2]);
        }
        else if (parts.Length == 4)
        {
            float a = float.Parse(parts[3], CultureInfo.InvariantCulture);

            return FromArgb(a, rgb[0], rgb[1], rgb[2]);
        }

        return Color.Empty;
    }

    private static Color HwbToColor(string htmlColor)
    {
        string[] parts = ColorParts(htmlColor);

        double hue = GetNumberOrPercentage(parts[0]);
        double white = GetNumberOrPercentage(parts[1]);
        double black = GetNumberOrPercentage(parts[2]);
        
        double a = parts.Length > 3 ? GetNumberOrPercentage(parts[3]) : 1.0;

        if (white + black >= 1)
        {
            double gray = white / (white + black);
            return FromArgb(a, gray, gray, gray);
        }

        double[] rgb = HslToRgb(hue, 1, .5);
        float[] rgb2 = HslToRgb(hue, 1, .5).Select(v => (float)v).ToArray();

        for (int i = 0; i < 3; i++)
        {
            rgb[i] *= Math.Round((1 - white - black), 2);
            rgb[i] += white;
        }

        return FromArgb(a, rgb[0], rgb[1], rgb[2]);
    }

    private static double[] HslToRgb(double hue, double sat, double light)
    {
        hue %= 360.0;

        if (hue < 0)
        {
            hue += 360;
        }

        double f(double n)
        {
            double k = (n + hue / 30.0) % 12.0;
            double a = sat * Math.Min(light, 1 - light);
            return light - a * Math.Max(-1, Math.Min(Math.Min(k - 3.0, 9.0 - k), 1));
        };

        return new double[] { f(0), f(8), f(4) };
    }

    private static string[] ColorParts(string htmlColor)
    {
        int left = htmlColor.IndexOf('(');
        int right = htmlColor.IndexOf(')');

        if (left < 0 || right < 0)
        {
            return new string[] { };
        }

        string noBrackets = htmlColor.Substring(left + 1, right - left - 1).Replace("deg", "");

        char seperator = htmlColor.Contains(',') ? ',' : ' ';

        string[] parts;
        string[] alphaChannel = noBrackets.Split('/');
        if (alphaChannel.Length == 2)
        {
            parts = alphaChannel[0].Trim().Split(seperator).Append(alphaChannel[1]).ToArray();
        }
        else
        {
            parts = alphaChannel[0].Split(seperator);
        }

        return parts.Select(p => p.Trim() == "none" ? "0" : p.Trim()).ToArray(); ;
    }

    private static double GetNumberOrPercentage(string part)
    {
        string raw = part.TrimEnd('%');

        if (double.TryParse(raw, out double d))
        {
            if (part.Contains('%') && d > 1)
            {
                return d / 100.0;
            }
        }

        return d;
    }

    public static Color FromArgb(double a, int r, int g, int b)
    {
        a = Math.Round(ClampAlpha(a) * 255.0, MidpointRounding.AwayFromZero);
        return Color.FromArgb((int)a, ClampRgb(r), ClampRgb(g), ClampRgb(b));
    }

    public static Color FromArgb(double r, double g, double b)
    {
        return FromArgb(1.0, r, g, b);
    }

    public static Color FromArgb(double a, double r, double g, double b)
    {
        a = Math.Round(ClampAlpha(a) * 255.0, MidpointRounding.AwayFromZero);
        r = Math.Round(r * 255.0, MidpointRounding.AwayFromZero);
        g = Math.Round(g * 255.0, MidpointRounding.AwayFromZero);
        b = Math.Round(b * 255.0, MidpointRounding.AwayFromZero);
        return Color.FromArgb(ClampRgb((int)a), ClampRgb((int)r), ClampRgb((int)g), ClampRgb((int)b));
    }

    public static int ClampRgb(int value)
    {
        return value < 0 ? 0 : value > 255 ? 255 : value;
        
    }

    public static double ClampAlpha(double value)
    {
        return value < 0.0 ? 0.0 : value > 1.0 ? 1.0 : value;

    }
}