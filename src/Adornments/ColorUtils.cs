using System.Drawing;
using System.Globalization;
using Microsoft.VisualStudio.Imaging;

// From https://stackoverflow.com/a/61871235
public static class ColorUtils
{
    public static Color HtmlToColor(string htmlColor)
    {
        if (htmlColor.Equals("rebeccapurple", StringComparison.OrdinalIgnoreCase))
        {
            htmlColor = "#663399";
        }

        try
        {
            if (htmlColor.StartsWith("rgb", StringComparison.OrdinalIgnoreCase))
            {
                return ArgbToColor(htmlColor);
            }
            else if (htmlColor.StartsWith("hsl", StringComparison.OrdinalIgnoreCase))
            {
                return HslToColor(htmlColor);
            }
            else if (htmlColor.StartsWith("#", StringComparison.Ordinal))
            {
                return HexToColor(htmlColor);
            }
            else
            {
                // Fallback to ColorTranslator for named colors, e.g. "Black", "White" etc.
                return ColorTranslator.FromHtml(htmlColor);
            }
        }
        catch
        {
            // ColorTranslator throws System.Exception, don't really care what the actual error is.
        }

        return Color.Empty;
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
        int left = htmlColor.IndexOf('(');
        int right = htmlColor.IndexOf(')');

        if (left < 0 || right < 0)
        {
            return Color.Empty;
        }

        string noBrackets = htmlColor.Substring(left + 1, right - left - 1);

        string[] parts = noBrackets.Split(',');

        int r = int.Parse(parts[0], CultureInfo.InvariantCulture);
        int g = int.Parse(parts[1], CultureInfo.InvariantCulture);
        int b = int.Parse(parts[2], CultureInfo.InvariantCulture);

        if (parts.Length == 3)
        {
            return Color.FromArgb(r, g, b);
        }
        else if (parts.Length == 4)
        {
            float a = float.Parse(parts[3], CultureInfo.InvariantCulture);

            return Color.FromArgb((int)(a * 255), r, g, b);
        }

        return Color.Empty;
    }

    private static Color HslToColor(string htmlColor)
    {
        int left = htmlColor.IndexOf('(');
        int right = htmlColor.IndexOf(')');

        if (left < 0 || right < 0)
        {
            return Color.Empty;
        }

        string noBrackets = htmlColor.Substring(left + 1, right - left - 1);

        string[] parts = noBrackets.Split(',');

        double h = GetDouble(parts[0], false);
        double s = GetDouble(parts[1]);
        double l = GetDouble(parts[2]);
        double alpha = 1.0;

        System.Windows.Media.Color hsl = new HslColor(h, s, l).ToColor();

        if (parts.Length == 4)
        {
            alpha = GetDouble(parts[3]);
        }

        return Color.FromArgb((int)(alpha * 255), hsl.R, hsl.G, hsl.B);
    }

    private static double GetDouble(string part, bool divide = true)
    {
        string raw = part.TrimEnd('%');

        if (double.TryParse(raw, out double d))
        {
            if (divide && d > 1)
            {
                return d / 100;
            }
        }

        return d;
    }
}