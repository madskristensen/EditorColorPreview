//***************************************************************************
//
//    Copyright (c) Microsoft Corporation. All rights reserved.
//    This code is licensed under the Visual Studio SDK license terms.
//    THIS CODE IS PROVIDED *AS IS* WITHOUT WARRANTY OF
//    ANY KIND, EITHER EXPRESS OR IMPLIED, INCLUDING ANY
//    IMPLIED WARRANTIES OF FITNESS FOR A PARTICULAR
//    PURPOSE, MERCHANTABILITY, OR NON-INFRINGEMENT.
//
//***************************************************************************

using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Editor;
using Microsoft.VisualStudio.Text.Tagging;
using Microsoft.VisualStudio.Utilities;
using System;
using System.ComponentModel.Composition;

namespace EditorColorPreview
{
    [Export(typeof(IViewTaggerProvider))]
    [ContentType("css")]
    [TagType(typeof(IntraTextAdornmentTag))]
    [TextViewRole(PredefinedTextViewRoles.Document)]
    internal sealed class ColorAdornmentTaggerProvider : IViewTaggerProvider
    {
        [Import]
        internal IBufferTagAggregatorFactoryService BufferTagAggregatorFactoryService = null;

        public ITagger<T> CreateTagger<T>(ITextView textView, ITextBuffer buffer) where T : ITag
        {
            if (textView == null)
                throw new ArgumentNullException("textView");

            if (buffer == null)
                throw new ArgumentNullException("buffer");

            if (textView.Roles.Contains("DIFF"))
                return null;

            return ColorAdornmentTagger.GetTagger(
                textView, buffer,
                new Lazy<ITagAggregator<ColorTag>>(
                    () => BufferTagAggregatorFactoryService.CreateTagAggregator<ColorTag>(buffer)))
                as ITagger<T>;
        }
    }
}
