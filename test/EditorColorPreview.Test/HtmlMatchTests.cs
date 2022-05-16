using System;
using System.Text.RegularExpressions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditorColorPreview.Test
{
    [TestClass]
    public class HtmlMatchTests
    {
        [DataTestMethod]
        [DataRow("color: rgb(0, 0, 0)")]
        [DataRow("color: hsl(120 30% 50%)")]
        [DataRow("color: hsl(120 30% 50% / 0.5)")]
        [DataRow("color: hsl(none none none)")]
        [DataRow("color: hsl(0 0% 0%)")]
        [DataRow("$sass-color: hsl(0 0% 0%)")]
        public void HtmlMatches_Should_Match(string htmlString)
        {
            MatchCollection matches = ColorUtils.MatchesColor(htmlString);

            Assert.AreEqual(1, matches.Count);
        }

        [TestMethod]
        public void Multiple_Single_Line_Should_Be_Two()
        {
            MatchCollection matches = ColorUtils.MatchesColor("$sass-color: hsl(0 0% 0%) color: hsl(0 0% 0%)");

            Assert.AreEqual(2, matches.Count);
        }
    }
}