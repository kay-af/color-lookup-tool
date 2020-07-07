using ColorLookupTool.Util;
using System.Collections.Generic;

namespace ColorLookupTool
{
    /// <summary>
    /// A static helper class to lookup a color name using hex-color string or int representation of colors
    /// </summary>
    public static class ColorLookup
    {
        private static KdTreeUtil lookup;
        private static bool initialized = false;

        /// <summary>
        /// Initialize the color lookup module
        /// </summary>
        public static void Initialize()
        {
            using (CSVReader reader = new CSVReader(Properties.Resources.colors))
            {
                List<KdColorNode> nodes = new List<KdColorNode>();

                string line;
                while (reader.ReadRow(out line))
                {
                    string[] split = line.Split(',');

                    string name = split[0].Trim(new char[] { ' ', '"' });
                    double red = double.Parse(split[2].Trim());
                    double green = double.Parse(split[3].Trim());
                    double blue = double.Parse(split[4].Trim());

                    KdColorNode node = new KdColorNode(name, new double[] { red, green, blue });
                    nodes.Add(node);
                }

                lookup = new KdTreeUtil(nodes.ToArray());
            }

            initialized = true;
        }

        /// <summary>
        /// Lookup a color name using the RGB(0-255) values
        /// </summary>
        /// <exception cref="LookupException"></exception>
        /// <param name="red">Red value (0-255)</param>
        /// <param name="green">Green value (0-255)</param>
        /// <param name="blue">Blue value (0-255)</param>
        /// <returns>The nearest matched color name</returns>
        public static string Match(double red, double green, double blue)
        {
            if (!initialized) throw new LookupException("Match called before initialization");
            KdColorNode match = lookup.NearestMatch(new double[] { red, green, blue });
            return match.colorName;
        }

        /// <summary>
        /// Lookup a color name using int representation of color
        /// 0xff0000 -> Red
        /// 0x00ff00 -> Green
        /// 0x0000ff -> Blue
        /// Remove alpha channel if present before using this method\n
        /// </summary>
        /// <exception cref="LookupException"></exception>
        /// <param name="color">int representation of a color</param>
        /// <returns>The nearest matched color name</returns>
        public static string Match(int color)
        {
            double red = (color >> 16) & 0xff;
            double green = (color >> 8) & 0xff;
            double blue = color & 0xff;
            return Match(red, green, blue);
        }

        /// <summary>
        /// Lookup a color name using hex-string eg: "#ffffff"
        /// Alpha channel will be removed if length exceeds 6
        /// </summary>
        /// <exception cref="LookupException"></exception>
        /// <param name="hexCode"># followed by six digit hex-string</param>
        /// <returns>The nearest matched color name</returns>
        public static string Match(string hexCode)
        {
            if (!hexCode.StartsWith("#")) throw new LookupException("Invalid color-hex string");
            hexCode = hexCode.TrimStart('#');
            if(hexCode.Length > 6)
            {
                hexCode = hexCode.Substring(0, 6);
            } else if(hexCode.Length < 6)
            {
                throw new LookupException("Invalid color-hex string");
            }
            return Match(int.Parse(hexCode, System.Globalization.NumberStyles.HexNumber));
        }
    }
}
