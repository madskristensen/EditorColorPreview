using System.Drawing;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace EditorColorPreview.Test
{
    [TestClass]
    public class HexColors
    {
        [DataTestMethod]
        [DataRow("#000000", "rgb(0,0,0)")]
        [DataRow("#FFFFFF", "rgb(255,255,255)")]
        [DataRow("#FF0000", "rgb(255,0,0)")]
        [DataRow("#00FF00", "rgb(0,255,0)")]
        [DataRow("#0000FF", "rgb(0,0,255)")]
        [DataRow("#FFFF00", "rgb(255,255,0)")]
        [DataRow("#00FFFF", "rgb(0,255,255)")]
        [DataRow("#FF00FF", "rgb(255,0,255)")]
        [DataRow("#C0C0C0", "rgb(192,192,192)")]
        [DataRow("#808080", "rgb(128,128,128)")]
        [DataRow("#800000", "rgb(128,0,0)")]
        [DataRow("#808000", "rgb(128,128,0)")]
        [DataRow("#008000", "rgb(0,128,0)")]
        [DataRow("#800080", "rgb(128,0,128)")]
        [DataRow("#008080", "rgb(0,128,128)")]
        [DataRow("#000080", "rgb(0,0,128)")]
        [DataRow("#000", "rgb(0,0,0)")]
        [DataRow("#FFF", "rgb(255,255,255)")]
        [DataRow("#F00", "rgb(255,0,0)")]
        [DataRow("#0F0", "rgb(0,255,0)")]
        [DataRow("#00F", "rgb(0,0,255)")]
        [DataRow("#FF0000FF", "rgba(255, 0, 0, 1.00)")]
        [DataRow("#FF0000F2", "rgba(255, 0, 0, .95)")]
        [DataRow("#FF0000E6", "rgba(255, 0, 0, .90)")]
        [DataRow("#FF0000D9", "rgba(255, 0, 0, .85)")]
        [DataRow("#FF0000CC", "rgba(255, 0, 0, .80)")]
        [DataRow("#FF0000BF", "rgba(255, 0, 0, .75)")]
        [DataRow("#FF0000B3", "rgba(255, 0, 0, .70)")]
        [DataRow("#FF000099", "rgba(255, 0, 0, .60)")]
        [DataRow("#FF00008C", "rgba(255, 0, 0, .55)")]
        [DataRow("#FF000080", "rgba(255, 0, 0, .50)")]
        [DataRow("#FF000073", "rgba(255, 0, 0, .45)")]
        [DataRow("#FF000066", "rgba(255, 0, 0, .40)")]
        [DataRow("#FF000059", "rgba(255, 0, 0, .35)")]
        [DataRow("#FF00004D", "rgba(255, 0, 0, .30)")]
        [DataRow("#FF000040", "rgba(255, 0, 0, .25)")]
        [DataRow("#FF000033", "rgba(255, 0, 0, .20)")]
        [DataRow("#FF000026", "rgba(255, 0, 0, .15)")]
        [DataRow("#FF00001A", "rgba(255, 0, 0, .10)")]
        [DataRow("#FF00000D", "rgba(255, 0, 0, .05)")]
        [DataRow("#FF000000", "rgba(255, 0, 0, .0)")]
        public void Test_RGB_Hex(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }
    }
}
