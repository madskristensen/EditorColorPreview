using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows.Media;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;

namespace EditorColorPreview
{
    internal sealed class ColorTagger : ITagger<ColorTag>
    {
        public static readonly Regex _regex = new(@"(#(?:[0-9a-fA-F]{2}){2,4}|(#[0-9a-fA-F]{3})|(rgb|hsl)a?\((-?\d+%?[,\s]+){2,3}\s*[\d\.]+%?\)|black|silver|gray|whitesmoke|maroon|red|purple|fuchsia|green|lime|olivedrab|yellow|navy|blue|teal|aquamarine|orange|aliceblue|antiquewhite|aqua|azure|beige|bisque|blanchedalmond|blueviolet|brown|burlywood|cadetblue|chartreuse|chocolate|coral|cornflowerblue|cornsilk|crimson|darkblue|darkcyan|darkgoldenrod|darkgray|darkgreen|darkgrey|darkkhaki|darkmagenta|darkolivegreen|darkorange|darkorchid|darkred|darksalmon|darkseagreen|darkslateblue|darkslategray|darkslategrey|darkturquoise|darkviolet|deeppink|deepskyblue|dimgray|dimgrey|dodgerblue|firebrick|floralwhite|forestgreen|gainsboro|ghostwhite|goldenrod|gold|greenyellow|grey|honeydew|hotpink|indianred|indigo|ivory|khaki|lavenderblush|lavender|lawngreen|lemonchiffon|lightblue|lightcoral|lightcyan|lightgoldenrodyellow|lightgray|lightgreen|lightgrey|lightpink|lightsalmon|lightseagreen|lightskyblue|lightslategray|lightslategrey|lightsteelblue|lightyellow|limegreen|linen|mediumaquamarine|mediumblue|mediumorchid|mediumpurple|mediumseagreen|mediumslateblue|mediumspringgreen|mediumturquoise|mediumvioletred|midnightblue|mintcream|mistyrose|moccasin|navajowhite|oldlace|olive|orangered|orchid|palegoldenrod|palegreen|paleturquoise|palevioletred|papayawhip|peachpuff|peru|pink|plum|powderblue|rosybrown|royalblue|saddlebrown|salmon|sandybrown|seagreen|seashell|sienna|skyblue|slateblue|slategray|slategrey|snow|springgreen|steelblue|tan|thistle|tomato|transparent|turquoise|violet|wheat|white|yellowgreen|rebeccapurple)\b", RegexOptions.Compiled);

        public IEnumerable<ITagSpan<ColorTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            foreach (SnapshotSpan span in spans.Where(s => !s.IsEmpty))
            {
                SnapshotSpan currentSpan = span;
                var text = currentSpan.GetText();

                if (!text.Trim().Contains("\n"))
                {
                    currentSpan = span.Snapshot.GetLineFromPosition(span.Start).ExtentIncludingLineBreak;
                    text = currentSpan.GetText();
                }

                foreach (Match match in _regex.Matches(text))
                {
                    var newSpan = new Span(currentSpan.Start + match.Index, match.Length);

                    if (span.Snapshot.Length >= newSpan.End)
                    {
                        var colorSpan = new SnapshotSpan(currentSpan.Snapshot, newSpan);
                        var color = (Color)ColorConverter.ConvertFromString(match.Value);

                        yield return new TagSpan<ColorTag>(colorSpan, new ColorTag(color));
                    }
                }
            }
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged
        {
            add { }
            remove { }
        }
    }
}
