using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Drawing;

namespace EditorColorPreview.Test
{
    [TestClass]
    public class CssColorFour
    {
        [DataTestMethod]
        [DataRow("hsl(120 30% 50%)", "rgb(89, 166, 89)")]
        [DataRow("hsl(120 30% 50% / 0.5)", "rgba(89, 166, 89, 0.5)")]
        [DataRow("hsl(none none none)", "rgb(0, 0, 0)")]
        [DataRow("hsl(0 0% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(none none none / none)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsl(0 0% 0% / 0)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsla(none none none)", "rgb(0, 0, 0)")]
        [DataRow("hsla(0 0% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsla(none none none / none)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsla(0 0% 0% / 0)", "rgba(0, 0, 0, 0)")]
        [DataRow("hsl(120 none none)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120 0% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120 80% none)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120 80% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120 none 50%)", "rgb(128, 128, 128)")]
        [DataRow("hsl(120 0% 50%)", "rgb(128, 128, 128)")]
        [DataRow("hsl(120 100% 50% / none)", "rgba(0, 255, 0, 0)")]
        [DataRow("hsl(120 100% 50% / 0)", "rgba(0, 255, 0, 0)")]
        [DataRow("hsl(none 100% 50%)", "rgb(255, 0, 0)")]
        [DataRow("hsl(0 100% 50%)", "rgb(255, 0, 0)")]
        [DataRow("hsl(120deg none none)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120deg 0% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120deg 80% none)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120deg 80% 0%)", "rgb(0, 0, 0)")]
        [DataRow("hsl(120deg none 50%)", "rgb(128, 128, 128)")]
        [DataRow("hsl(120deg 0% 50%)", "rgb(128, 128, 128)")]
        [DataRow("hsl(120deg 100% 50% / none)", "rgba(0, 255, 0, 0)")]
        [DataRow("hsl(120deg 100% 50% / 0)", "rgba(0, 255, 0, 0)")]
        public void HSLA_Test(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
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

        [DataTestMethod]
        [DataRow("hwb(120 30% 50%)", "rgb(77, 128, 77)")]
        [DataRow("hwb(120 30% 50% / 0.5)", "rgba(77, 128, 77, 0.5)")]
        [DataRow("hwb(none none none)", "rgb(255, 0, 0)")]
        [DataRow("hwb(0 0% 0%)", "rgb(255, 0, 0)")]
        [DataRow("hwb(none none none / none)", "rgba(255, 0, 0, 0)")]
        [DataRow("hwb(0 0% 0% / 0)", "rgba(255, 0, 0, 0)")]
        [DataRow("hwb(120 none none)", "rgb(0, 255, 0)")]
        [DataRow("hwb(120 0% 0%)", "rgb(0, 255, 0)")]
        [DataRow("hwb(120 80% none)", "rgb(204, 255, 204)")]
        [DataRow("hwb(120 80% 0%)", "rgb(204, 255, 204)")]
        [DataRow("hwb(120 none 50%)", "rgb(0, 128, 0)")]
        [DataRow("hwb(120 0% 50%)", "rgb(0, 128, 0)")]
        [DataRow("hwb(120 30% 50% / none)", "rgba(77, 128, 77, 0)")]
        [DataRow("hwb(120 30% 50% / 0)", "rgba(77, 128, 77, 0)")]
        [DataRow("hwb(none 100% 50% / none)", "rgba(170, 170, 170, 0)")]
        [DataRow("hwb(0 100% 50% / 0)", "rgba(170, 170, 170, 0)")]
        public void HWB_Test(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("color(srgb 0 0.6 0)", "#009900")]
        [DataRow("color(srgb 0% 60% 0%)", "#009900")]
        [DataRow("color(srgb-linear 0 0.21586 0)", "#008000")]
        [DataRow("color(srgb-linear 0 0 0)", "#000")]
        [DataRow("color(srgb-linear 1 1 1)", "color(srgb 1 1 1)")]
        [DataRow("color(srgb-linear 0 1 0)", "lab(87.8185% -79.271 80.9946)")]
        [DataRow("color(srgb 0.691 0.139 0.259)", "color(srgb-linear 0.435 0.017 0.055)")]
        [DataRow("color(display-p3 0.21604 0.49418 0.13151)", "#008000")]
        [DataRow("color(display-p3 0 0 0)", "#000000")]
        [DataRow("color(display-p3 1 1 1)", "rgb(100% 100% 100%)")]
        [DataRow("color(display-p3 26.374% 59.085% 16.434%)", "#009900")]
        [DataRow("color(display-p3 0 1 0)", "lab(86.61399% -106.539 102.871)")]
        [DataRow("color(display-p3 1 1 0.330897)", "yellow")]
        [DataRow("color(display-p3 0.465377 0.532768 0.317713)", "rgb(44.8436% 53.537% 28.8112%)")]
        [DataRow("color(srgb 0.448436 0.53537 0.288113)", "rgb(44.8436% 53.537% 28.8112%)")]
        //[DataRow("color(a98-rgb 0.281363 0.498012 0.116746)", "#008000")]
        [DataRow("color(a98-rgb 0 0 0)", "#000000")]
        [DataRow("color(a98-rgb 1 1 1)", "rgb(99.993% 100% 100%)")]
        [DataRow("color(a98-rgb 0 1 0)", "lab(83.2141% -129.1072 87.1718)")]
        //[DataRow("color(prophoto-rgb 0.230479 0.395789 0.129968)", "#008000")]
        [DataRow("color(prophoto-rgb 0 0 0)", "#000000")]
        //[DataRow("color(prophoto-rgb 1 1 1)", "lab(100% 0.0131 0.0085)")]
        [DataRow("color(prophoto-rgb 0 1 0)", "lab(87.5745% -186.6921 150.9905)")]
        [DataRow("color(prophoto-rgb 0.42444 0.934918 0.190922)", "color(display-p3 0 1 0)")]
        //[DataRow("color(prophoto-rgb 28.610% 49.131% 16.133%)", "#009900")]
        [DataRow("color(rec2020 0.235202 0.431704 0.085432)", "#008000")]
        [DataRow("color(rec2020 0 0 0)", "#000000")]
        [DataRow("color(rec2020 1 1 1)", "rgb(99.993% 100% 100%)")]
        [DataRow("color(rec2020 0 1 0)", "lab(85.7729% -160.7259 109.2319)")]
        [DataRow("color(rec2020 0.431836 0.970723 0.079208)", "color(display-p3 0 1 0)")]
        [DataRow("color(rec2020 29.9218% 53.3327% 12.0785%)", "#009900")]
        [DataRow("color(rec2020 0.299218 0.533327 0.120785)", "#009900")]
        [DataRow("color(xyz 0.07719 0.15438 0.02573)", "#008000")]
        [DataRow("color(xyz 0 0 0)", "#000000")]
        [DataRow("color(xyz 1 1 1)", "lab(100.115% 9.06448 5.80177)")]
        [DataRow("color(xyz 0 1 0)", "lab(99.6289% -354.58 146.707)")]
        [DataRow("color(xyz 0.26567 0.69174 0.04511)", "color(display-p3 0 1 0)")]
        [DataRow("color(xyz-d50 0.08312 0.154746 0.020961)", "#008000")]
        [DataRow("color(xyz-d50 0 0 0)", "#000000")]
        [DataRow("color(xyz-d50 1 1 1)", "lab(100% 6.1097 -13.2268)")]
        [DataRow("color(xyz-d50 0 1 0)", "lab(100% -431.0345 172.4138)")]
        [DataRow("color(xyz-d50 0.29194 0.692236 0.041884)", "color(display-p3 0 1 0)")]
        [DataRow("color(xyz-d65 0.07719 0.15438 0.02573)", "#008000")]
        [DataRow("color(xyz-d65 0 0 0)", "#000000")]
        [DataRow("color(xyz-d65 1 1 1)", "lab(100.115% 9.06448 5.80177)")]
        [DataRow("color(xyz-d65 0 1 0)", "lab(99.6289% -354.58 146.707)")]
        [DataRow("color(xyz-d65 0.26567 0.69174 0.04511)", "color(display-p3 0 1 0)")]
        public void Color_Test(string testHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(testHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);
            if (expected.IsKnownColor)
            {
                Assert.AreEqual(expected.A, actual.A);
                Assert.AreEqual(expected.R, actual.R);
                Assert.AreEqual(expected.G, actual.G);
                Assert.AreEqual(expected.B, actual.B);
            }
            else
            {
                Assert.AreEqual(expected, actual);
            }
            
        }

        [DataTestMethod]
        [DataRow("Basic sRGB white", "color(srgb 1 1 1)", "color(srgb 1 1 1)")]
        [DataRow("White with lots of space", "color(    srgb         1      1 1       )", "color(srgb 1 1 1)")]
        [DataRow("sRGB color", "color(srgb 0.25 0.5 0.75)", "color(srgb 0.25 0.5 0.75)")]
        [DataRow("Different case for sRGB", "color(SrGb 0.25 0.5 0.75)", "color(srgb 0.25 0.5 0.75)")]
        [DataRow("sRGB color with unnecessary decimals", "color(srgb 1.00000 0.500000 0.20)", "color(srgb 1 0.5 0.2)")]
        [DataRow("sRGB white with 0.5 alpha", "color(srgb 1 1 1 / 0.5)", "color(srgb 1 1 1 / 0.5)")]
        [DataRow("sRGB white with 0 alpha", "color(srgb 1 1 1 / 0)", "color(srgb 1 1 1 / 0)")]
        [DataRow("sRGB white with 50% alpha", "color(srgb 1 1 1 / 50%)", "color(srgb 1 1 1 / 0.5)")]
        [DataRow("sRGB white with 0% alpha", "color(srgb 1 1 1 / 0%)", "color(srgb 1 1 1 / 0)")]
        [DataRow("Display P3 color", "color(display-p3 0.6 0.7 0.8)", "color(display-p3 0.6 0.7 0.8)")]
        [DataRow("Different case for Display P3", "color(dIspLaY-P3 0.6 0.7 0.8)", "color(display-p3 0.6 0.7 0.8)")]
        [DataRow("sRGB color with negative component should clamp to 0", "color(srgb -0.25 0.5 0.75)", "color(srgb 0 0.5 0.75)")]
        [DataRow("sRGB color with component > 1 should clamp", "color(srgb 0.25 1.5 0.75)", "color(srgb 0.25 1 0.75)")]
        [DataRow("Display P3 color with negative component should clamp to 0", "color(display-p3 0.5 -199 0.75)", "color(display-p3 0.5 0 0.75)")]
        [DataRow("Display P3 color with component > 1 should clamp", "color(display-p3 184 1.00001 2347329746587)", "color(display-p3 1 1 1)")]
        [DataRow("Alpha > 1 should clamp", "color(srgb 0.1 0.2 0.3 / 1.9)", "color(srgb 0.1 0.2 0.3)")]
        [DataRow("Negative alpha should clamp", "color(srgb 1 1 1 / -0.2)", "color(srgb 1 1 1 / 0)")]
        public void Parsing_Tests(string _, string inputHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(inputHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Lab_Color_test()
        {
            Color expected = ColorUtils.HtmlToColor("rgb(46.27%, 32.94%, 80.39%)");

            Color actual = ColorUtils.HtmlToColor("lab(44.36% 36.05 -58.99)");

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("oklab(51.975% -0.1403 0.10768)", "#008000")]
        [DataRow("oklab(0% 0 0)", "#000000")]
        [DataRow("oklab(100% 0 0)", "#FFFFFF")]
        [DataRow("oklab(50% 0.05 0)", "rgb(48.477% 34.29% 38.412%)")]
        [DataRow("oklab(70% -0.1 0)", "rgb(29.264% 70.096% 63.017%)")]
        [DataRow("oklab(70% 0 0.125)", "rgb(73.942% 60.484% 19.65%)")]
        [DataRow("oklab(55% 0 -0.2)", "rgb(27.888% 38.072% 89.414%)")]
        [DataRow("oklab(84.883% -0.3042 0.20797)", "color(display-p3 0 1 0)")]
        public void OkLab_Color_Test(string inputHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(inputHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }

        [DataTestMethod]
        [DataRow("oklch(51.975% 0.17686 142.495)", "#008000")]
        [DataRow("oklch(0% 0 0)", "#000000")]
        [DataRow("oklch(100% 0 0)", "#FFFFFF")]
        [DataRow("oklch(50% 0.2 0)", "rgb(70.492% 2.351% 37.073%)")]
        [DataRow("oklch(50% 0.2 270)", "rgb(23.056% 31.73% 82.628%)")]
        [DataRow("oklch(80% 0.15 160)", "rgb(32.022% 85.805% 61.147%)")]
        [DataRow("oklch(55% 0.15 345)", "rgb(67.293% 27.791% 52.28%)")]
        [DataRow("oklch(84.883% 0.36853 145.645)", "color(display-p3 0 1 0)")]
        public void OkLch_Color_Test(string inputHtml, string expectedHtml)
        {
            Color actual = ColorUtils.HtmlToColor(inputHtml);

            Color expected = ColorUtils.HtmlToColor(expectedHtml);

            Assert.AreEqual(expected, actual);
        }
    }
}

