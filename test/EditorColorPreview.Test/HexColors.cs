using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text;
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
        public void Test_RGB_Hex(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }
    }
}
