using Microsoft.VisualStudio.Text.Tagging;
using System.Windows.Media;

namespace EditorColorPreview
{
    internal class ColorTag : ITag
    {
        internal Color Color { get; }

        internal ColorTag(Color color)
        {
            Color = color;
        }
    }
}
