using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.Drawing;
using System.Linq;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;

namespace EditorColorPreview
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("css")]
    [ContentType("typescript")]
    [TagType(typeof(IntraTextAdornmentTag))]
    internal sealed class ColorAdornmentTaggerProvider : IViewTaggerProvider
    {
        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag =>
            buffer.Properties.GetOrCreateSingletonProperty(() => new ColorAdornmentTagger(buffer, textView)) as ITagger<T>;
    }

    internal class ColorAdornmentTagger : ITagger<IntraTextAdornmentTag>, IDisposable
    {
        private readonly ITextView _view;
        private readonly ITextBuffer _buffer;
        private bool _isDisposed;
        private bool _isProcessing;

        public ColorAdornmentTagger(ITextBuffer buffer, ITextView view)
        {
            _view = view;
            _buffer = buffer;
            _buffer.Changed += OnBufferChange;
        }

        private void OnBufferChange(object sender, TextContentChangedEventArgs e)
        {
            if (_isProcessing)
                return;

            try
            {
                _isProcessing = true;
                int start = e.Changes.First().NewSpan.Start;
                int end = e.Changes.Last().NewSpan.End;

                ITextSnapshotLine startLine = e.After.GetLineFromPosition(start);
                ITextSnapshotLine endLine = e.After.GetLineFromPosition(end);

                SnapshotSpan span = new(e.After, Span.FromBounds(startLine.Start, endLine.End));
                TagsChanged?.Invoke(this, new SnapshotSpanEventArgs(span));
            }
            finally
            {
                _isProcessing = false;
            }
        }

        public IEnumerable<ITagSpan<IntraTextAdornmentTag>> GetTags(NormalizedSnapshotSpanCollection spans)
        {
            if (_isProcessing)
                yield break;

            foreach (SnapshotSpan span in spans.Where(s => !s.IsEmpty && s.Length >= 3))
            {
                SnapshotSpan currentSpan = span;
                string text = currentSpan.GetText();

                foreach (Match match in ColorUtils.MatchesColor(text))
                {
                    Color color = ColorUtils.HtmlToColor(match.Value);

                    if (color != Color.Empty)
                    {
                        System.Windows.Media.Color winColor = System.Windows.Media.Color.FromArgb(color.A, color.R, color.G, color.B);
                        IntraTextAdornmentTag tag = new(new ColorAdornment(winColor, _view), null, PositionAffinity.Successor);
                        SnapshotSpan colorSpan = new(currentSpan.Snapshot, currentSpan.Start + match.Index, 0);

                        yield return new TagSpan<IntraTextAdornmentTag>(colorSpan, tag);
                    }
                }
            }
        }

        public void Dispose()
        {
            if (!_isDisposed)
            {
                _buffer.Changed -= OnBufferChange;
            }

            _isDisposed = true;
        }

        public event EventHandler<SnapshotSpanEventArgs> TagsChanged;
    }
}
