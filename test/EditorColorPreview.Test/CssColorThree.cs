using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace EditorColorPreview.Test
{
    [TestClass]
    public class CssColorThree
    {
        [DataTestMethod]
        [DataRow("hsl(120, 30%, 50%)", "rgb(89, 166, 89)")]
        [DataRow("hsl(120, 30%, 50%, 0.5)", "rgba(89, 166, 89, 0.5)")]
        [DataRow("hsl(0, 0%, 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(0, 0%, 0%, 0)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsla(0, 0%, 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsla(0, 0%, 0%, 0)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsl(120, 0%, 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120, 80%, 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120, 0%, 50%)", "rgb(128, 128, 128)")]
        [DataRow("hsl(120, 100%, 50%, 0)", "rgba(0, 255, 0, 0)")]
        [DataRow("hsl(0, 100%, 50%)", "rgb(255, 0, 0)")]
        public void HSLA_Test(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("rgb(-2, 3, 4)", "rgb(0, 3, 4)")]
        [DataRow("rgb(100, 200, 300)", "rgb(100, 200, 255)")]
        [DataRow("rgb(20, 10, 0, -10)", "rgba(20, 10, 0, 0)")]
        [DataRow("rgb(100%, 200%, 300%)", "rgb(255, 255, 255)")]
        [DataRow("rgb(2, 3, 4)", "rgb(2, 3, 4)")]
        [DataRow("rgb(100%, 0%, 0%)", "rgb(255, 0, 0)")]
        [DataRow("rgba(2, 3, 4, 0.5)", "rgba(2, 3, 4, 0.5)")]
        [DataRow("rgba(2, 3, 4, 50%)", "rgba(2, 3, 4, 0.5)")]
        [DataRow("rgb(none none none)", "rgb(0, 0, 0)")]
        [DataRow("rgb(none none none / none)", "rgba(0, 0, 0, 0)")]
        [DataRow("rgb(128 none none)", "rgb(128, 0, 0)")]
        [DataRow("rgb(128 none none / none)", "rgba(128, 0, 0, 0)")]
        [DataRow("rgb(none none none / .5)", "rgba(0, 0, 0, 0.5)")]
        [DataRow("rgb(20% none none)", "rgb(51, 0, 0)")]
        [DataRow("rgb(20% none none / none)", "rgba(51, 0, 0, 0)")]
        [DataRow("rgb(none none none / 50%)", "rgba(0, 0, 0, 0.5)")]
        [DataRow("rgba(none none none)", "rgb(0, 0, 0)")]
        [DataRow("rgba(none none none / none)", "rgba(0, 0, 0, 0)")]
        [DataRow("rgba(128 none none)", "rgb(128, 0, 0)")]
        [DataRow("rgba(128 none none / none)", "rgba(128, 0, 0, 0)")]
        [DataRow("rgba(none none none / .5)", "rgba(0, 0, 0, 0.5)")]
        [DataRow("rgba(20% none none)", "rgb(51, 0, 0)")]
        [DataRow("rgba(20% none none / none)", "rgba(51, 0, 0, 0)")]
        [DataRow("rgba(none none none / 50%)", "rgba(0, 0, 0, 0.5)")]
        public void RGA_Test(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }
    }
}
