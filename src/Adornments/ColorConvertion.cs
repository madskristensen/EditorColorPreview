using System.Linq;

namespace EditorColorPreview
{
    public class ColorConvertion
    {
        private static double[] _d50 = { 0.3457 / 0.3585, 1.00000, (1.0 - 0.3457 - 0.3585) / 0.3585 };
        private static double[] _d65 = { 0.3127 / 0.3290, 1.00000, (1.0 - 0.3127 - 0.3290) / 0.3290 };

        // Lab & LCH

        public static double[] LabToRgb(double l, double a, double b)
        {
            double[] result = LabToXYZ(new double[] { l, a, b });
            result = D50ToD65(result);
            result = XyzToLinearRGB(result);
            return LinearRgbToRgb(result);
        }

        public static double[] LchToRgb(double l, double c, double h)
        {
            double[] results = LchToLab(new double[] { l, c, h });
            results = LabToXYZ(results);
            results = D50ToD65(results);
            return XyzToLinearRGB(results);

        }

        private static double[] LchToLab(double[] lch) =>
            new double[] { lch[0], lch[1] * Math.Cos(lch[2]), lch[1] * Math.Sin(lch[2]) };

        private static double[] LabToXYZ(double[] lab)
        {
            // Convert Lab to D50-adapted XYZ
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            double κ = 24389 / 27;   // 29^3/3^3
            double ε = 216 / 24389;  // 6^3/29^3
            double[] f = new double[3];

            // compute f, starting with the luminance-related term
            f[1] = (lab[0] + 16) / 116;
            f[0] = lab[1] / 500 + f[1];
            f[2] = f[1] - lab[2] / 200;

            // compute xyz
            double[] xyz = {
                Math.Pow(f[0], 3) > ε ? Math.Pow(f[0], 3) : (116 * f[0] - 16) / κ,
                lab[0] > κ * ε ? Math.Pow((lab[0] + 16) / 116, 3) : lab[0] / κ,
                Math.Pow(f[2], 3) > ε ? Math.Pow(f[2], 3) : (116 * f[2] - 16) / κ
            };

            // Compute XYZ by scaling xyz by reference white
            return xyz.Select((value, i) => value * _d50[i]).ToArray();
        }

        // OKLab & OKLCH

        public static double[] OkLabToRgb(double l, double a, double b)
        {
            return OkLabToRgb(new double[] { l , a, b });
        }

        private static double[] OkLabToRgb(double[] okLab)
        {
            double[] results = OkLabToXyz(okLab);
            results = XyzToLinearRGB(results);
            return LinearRgbToRgb(results);
        }

        public static double[] OkLchToRgb(double l, double a, double b)
        {
            double[] results = OkLchToOkLab(new double[] { l, a, b });
            return OkLabToRgb(results);
        }

        private static double[] OkLabToXyz(double[] oKLab)
        {
            // Given OKLab, convert to XYZ relative to D65
            double[,] LMStoXYZ = {
                { 1.2268798733741557, -0.5578149965554813, 0.28139105017721583 },
                { -0.04057576262431372, 1.1122868293970594, -0.07171106666151701 },
                { -0.07637294974672142, -0.4214933239627914, 1.5869240244272418 }
            };
            double[,] OKLabtoLMS = {
                { 0.99999999845051981432, 0.39633779217376785678, 0.21580375806075880339 },
                { 1.0000000088817607767, -0.1055613423236563494, -0.063854174771705903402 },
                { 1.0000000546724109177, -0.089484182094965759684, -1.291485537864091739 },
            };

            double[] LMSnl = MultiplyMatrices(OKLabtoLMS, oKLab);
            LMSnl = LMSnl.Select(c => Math.Pow(c, 3)).ToArray();
            return MultiplyMatrices(LMStoXYZ, LMSnl);
        }

        private static double[] OkLchToOkLab(double[] okLch)
        {
            return new double[] {
                okLch[0], // L is still L
                okLch[1] * Math.Cos(okLch[2] * Math.PI / 180), // a
                okLch[1] * Math.Sin(okLch[2] * Math.PI / 180)  // b
            };
        }

        // sRGB function

        public static double[] RgbToLinearRgb(double[] rgb)
        {
            // convert an array of sRGB values
            // where in-gamut values are in the range [0 - 1]
            // to linear light (un-companded) form.
            // https://en.wikipedia.org/wiki/SRGB
            // Extended transfer function:
            // for negative values,  linear portion is extended on reflection of axis,
            // then reflected power function is used.
            return rgb.Select(val =>
            {
                double sign = val < 0 ? -1 : 1;
                double abs = Math.Abs(val);

                if (abs < 0.04045)
                {
                    return val / 12.92;
                }

                return sign * (Math.Pow((abs + 0.055) / 1.055, 2.4));
            }).ToArray();
        }

        public static double[] LinearRgbToRgb(double[] RGB)
        {
            // convert an array of linear-light sRGB values in the range 0.0-1.0
            // to gamma corrected form
            // https://en.wikipedia.org/wiki/SRGB
            // Extended transfer function:
            // For negative values, linear portion extends on reflection
            // of axis, then uses reflected pow below that
            return RGB.Select(v =>
            {
                double sign = v < 0 ? -1 : 1;
                double abs = Math.Abs(v);

                if (abs > 0.0031308)
                {
                    return sign * (1.055 * Math.Pow(abs, 1 / 2.4) - 0.055);
                }

                return 12.92 * v;
            }).ToArray();
        }

        private static double[] XyzToLinearRGB(double[] xyz)
        {
            // convert XYZ to linear-light sRGB

            double[,] m = {
                { 3.2409699419045226, -1.537383177570094, -0.4986107602930034 },
                { -0.9692436362808796, 1.8759675015077202, 0.04155505740717559 },
                { 0.05563007969699366, -0.20397695888897652, 1.056971514242878 },
            };

            return MultiplyMatrices(m, xyz);
        }

        // display-p3-related functions

        public static double[] DisplayP3ToRgb(double n1, double n2, double n3)
        {
            double[] results = RgbToLinearRgb(new double[] { n1, n2, n3 });
            results = LinearP3ToXyz(results);
            results = XyzToLinearRGB(results);
            return LinearRgbToRgb(results);
        }

        private static double[] LinearP3ToXyz(double[] rgb)
        {
            // convert an array of linear-light display-p3 values to CIE XYZ
            // using  D65 (no chromatic adaptation)
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            double[,] m = {
                { 0.4865709486482162, 0.26566769316909306, 0.1982172852343625 },
                { 0.2289745640697488, 0.6917385218365064, 0.079286914093745 },
                { 0.0000000000000000, 0.04511338185890264, 1.043944368900976 }
            };
            // 0 was computed as -3.972075516933488e-17

            return MultiplyMatrices(m, rgb);
        }

        // a98-rgb functions

        public static double[] A98RgbToRgb(double n1, double n2, double n3)
        {
            double[] results = A98rgbToLinear(new double[] { n1, n2, n3 });
            results = LinearA98RgbToXyz(results);
            results = XyzToLinearRGB(results);
            return LinearRgbToRgb(results);
        }

        private static double[] A98rgbToLinear(double[] rgb)
        {
            // convert an array of a98-rgb values in the range 0.0 - 1.0
            // to linear light (un-companded) form.
            // negative values are also now accepted
            return rgb.Select(val =>
            {
                double sign = val < 0 ? -1 : 1;
                double abs = Math.Abs(val);

                return sign * Math.Pow(abs, 563 / 256);
            }).ToArray();
        }

        private static double[] LinearA98RgbToXyz(double[] rgb)
        {
            // convert an array of linear-light a98-rgb values to CIE XYZ
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            // has greater numerical precision than section 4.3.5.3 of
            // https://www.adobe.com/digitalimag/pdfs/AdobeRGB1998.pdf
            // but the values below were calculated from first principles
            // from the chromaticity coordinates of R G B W
            // see matrixmaker.html
            double[,] m = {
                { 0.5766690429101305, 0.1855582379065463, 0.1882286462349947 },
                { 0.29734497525053605, 0.6273635662554661, 0.07529145849399788 },
                { 0.02703136138641234, 0.07068885253582723, 0.9913375368376388 }
            };

            return MultiplyMatrices(m, rgb);
        }

        // ProPhoto

        public static double[] ProPhotoToRgb(double n1, double n2, double n3)
        {
            double[] results = ProPhotoToLinear(new double[] { n1, n2, n3 });
            results = LinerProPhotoToXyz(results);
            results = XyzToLinearRGB(results);
            results = D50ToD65(results);
            return LinearRgbToRgb(results);
        }

        private static double[] ProPhotoToLinear(double[] rgb)
        {
            // convert an array of prophoto-rgb values
            // where in-gamut colors are in the range [0.0 - 1.0]
            // to linear light (un-companded) form.
            // Transfer curve is gamma 1.8 with a small linear portion
            // Extended transfer function
            double Et2 = 16 / 512;
            return rgb.Select(v => {
                double sign = v < 0 ? -1 : 1;
                double abs = Math.Abs(v);

                if (abs <= Et2)
                {
                    return v / 16;
                }

                return sign * Math.Pow(v, 1.8);
            }).ToArray();
        }

        private static double[] LinerProPhotoToXyz(double[] rgb)
        {
            // convert an array of linear-light prophoto-rgb values to CIE XYZ
            // using  D50 (so no chromatic adaptation needed afterwards)
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            double[,] m = {
                { 0.7977604896723027, 0.13518583717574031, 0.0313493495815248 },
                { 0.2880711282292934, 0.7118432178101014, 0.00008565396060525902 },
                { 0.0, 0.0, 0.8251046025104601 }
	        };

            return MultiplyMatrices(m, rgb);
        }

        // Rec. 2020

        public static double[] Rec2020ToRgb(double n1, double n2, double n3)
        {
            double[] results = Rec2020ToLinear(new double[] { n1, n2, n3 });
            results = LinearRec2020ToXyz(results);
            results = XyzToLinearRGB(results);
            return LinearRgbToRgb(results);
        }

        private static double[] Rec2020ToLinear(double[] rgb)
        {
            // convert an array of rec2020 RGB values in the range 0.0 - 1.0
            // to linear light (un-companded) form.
            // ITU-R BT.2020-2 p.4

            double α = 1.09929682680944;
            double β = 0.018053968510807;

            return rgb.Select(val =>
            {
                double sign = val < 0 ? -1 : 1;
                double abs = Math.Abs(val);

                if (abs < β * 4.5)
                {
                    return val / 4.5;
                }

                return sign * (Math.Pow((abs + α - 1) / α, 1 / 0.45));
            }).ToArray();
        }

        private static double[] LinearRec2020ToXyz(double[] rgb)
        {
            // convert an array of linear-light rec2020 values to CIE XYZ
            // using  D65 (no chromatic adaptation)
            // http://www.brucelindbloom.com/index.html?Eqn_RGB_XYZ_Matrix.html
            double[,] m = {
                { 0.6369580483012914, 0.14461690358620832, 0.1688809751641721 },
                { 0.2627002120112671, 0.6779980715188708, 0.05930171646986196 },
                { 0.000000000000000, 0.028072693049087428, 1.060985057710791 }
	        };
            // 0 is actually calculated as  4.994106574466076e-17

            return MultiplyMatrices(m, rgb);
        }

        // XYZ

        public static double[] XyzToRgb(double n1, double n2, double n3, bool covertWhitePoint = false)
        {
            double[] results;
            if (covertWhitePoint)
            {
                results = D50ToD65(new double[] { n1, n2, n3 });
                results = XyzToLinearRGB(results);
            }
            else
            {
                results = XyzToLinearRGB(new double[] { n1, n2, n3 });
            }

            return LinearRgbToRgb(results);
        }


        public double[] D65ToD50(double[] xyz)
        {
            // Bradford chromatic adaptation from D65 to D50
            // The matrix below is the result of three operations:
            // - convert from XYZ to retinal cone domain
            // - scale components from one reference white to another
            // - convert back to XYZ
            // http://www.brucelindbloom.com/index.html?Eqn_ChromAdapt.html
            double[,] m = {
                { 1.0479298208405488, 0.022946793341019088, -0.05019222954313557 },
                { 0.029627815688159344, 0.990434484573249, -0.01707382502938514 },
                { -0.009243058152591178, 0.015055144896577895, 0.7518742899580008 }
            };

            return MultiplyMatrices(m, xyz);
        }

        private static double[] D50ToD65(double[] xyz)
        {
            // Bradford chromatic adaptation from D50 to D65
            double[,] M = {
                { 0.9554734527042182, -0.023098536874261423, 0.0632593086610217 },
                { -0.028369706963208136, 1.0099954580058226, 0.021041398966943008 },
                {  0.012314001688319899, -0.020507696433477912, 1.3303659366080753 }
            };

            return MultiplyMatrices(M, xyz);
        }

        // Matrix Math

        private static double[] MultiplyMatrices(double[,] m, double[] vector)
        {
            double c1 = m[0, 0] * vector[0]
                + m[0, 1] * vector[1]
                + m[0, 2] * vector[2];
            double c2 = m[1, 0] * vector[0]
                + m[1, 1] * vector[1]
                + m[1, 2] * vector[2];
            double c3 = m[2, 0] * vector[0]
                + m[2, 1] * vector[1]
                + m[2, 2] * vector[2];

            return new double[] { c1, c2, c3 };
        }
    }
}
