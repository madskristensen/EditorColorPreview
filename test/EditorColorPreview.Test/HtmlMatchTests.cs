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
        [DataRow("color: rgba(0,0,4,0.82);")]
        [DataRow("color: rgba(0 0 4 0.82);")]
        [DataRow("color: hsl(120 30% 50%)")]
        [DataRow("color: hsl(120 30% 50% / 0.5)")]
        [DataRow("color: hsl(none none none)")]
        [DataRow("color: hsl(0 0% 0%)")]
        [DataRow("$sass-color: hsl(0 0% 0%)")]
        [DataRow("color: lab(29.69% 44.888% -29.04%)")]
        [DataRow("color: lab(67.5345% -8.6911 -41.6019)")]
        [DataRow("color: color(rec2020 0.42053 0.979780 0.00579);")]
        [DataRow("color: color(display-p3 -0.6112 1.0079 -0.2192);")]
        [DataRow("color: color(srgb 0.691 0.139 0.259)")]
        [DataRow("color: color(srgb-linear 0.435 0.017 0.055)")]
        [DataRow("background-color: color(xyz-d50 0.2005 0.14089 0.4472)")]
        [DataRow("background-color: color(xyz-d65 0.21661 0.14602 0.59452)")]
        [DataRow("background-color: color(xyz 0.26567 0.69174 0.04511)")]
        [DataRow("color: oklch(80% 0.15 160)")]
        [DataRow("color: oklch(55% 0.15 345)")]
        [DataRow("color: oklch(84.883% 0.36853 145.645)")]
        [DataRow("color: oklab(70% 0 0.125)")]
        [DataRow("color: oklab(55% 0 -0.2)")]
        [DataRow("color: oklab(84.883% -0.3042 0.20797)")]
        [DataRow("color: oklch(53.85% .1725 320.67 / .7);")]
        public void HtmlMatches_Should_Match(string htmlString)
        {
            MatchCollection matches = ColorUtils.MatchesColor(htmlString);

            Assert.AreEqual(1, matches.Count);
        }

        [DataTestMethod]
        [DataRow("color: rgb(0, 0)")]
        [DataRow("color: hsx(120 30% 50%)")]
        [DataRow("color: hsl(120 30% 50% / 50%)")]
        //[DataRow("color: hsl(120 30% / 0.5)")]
        [DataRow("background-color: color(profoto-rgb 0.4835 0.9167 0.2188)")]
        public void HtmlMatches_Should_Not_Match(string htmlString)
        {
            MatchCollection matches = ColorUtils.MatchesColor(htmlString);

            Assert.AreEqual(0, matches.Count);
        }

        [TestMethod]
        public void Multiple_Single_Line_Should_Be_Two()
        {
            MatchCollection matches = ColorUtils.MatchesColor("$sass-color: hsl(0 0% 0%) color: hsl(0 0% 0%)");

            Assert.AreEqual(2, matches.Count);
        }
    }
}