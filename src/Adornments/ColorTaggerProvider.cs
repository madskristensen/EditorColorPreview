using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace EditorColorPreview
{
    [Export(typeof(ITaggerProvider))]
    [ContentType("css")]
    [TagType(typeof(ColorTag))]
    internal sealed class ColorTaggerProvider : ITaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextBuffer buffer) where T : ITag =>
            buffer.Properties.GetOrCreateSingletonProperty(() => new ColorTagger()) as ITagger<T>;
    }
}
