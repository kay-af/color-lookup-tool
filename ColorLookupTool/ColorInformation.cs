using System;

namespace ColorLookupTool
{
    public class ColorInformation
    {
        public string colorName { get; internal set; }
        public int red { get; internal set; }
        public int green { get; internal set; }
        public int blue { get; internal set; }

        internal ColorInformation(string colorName, int red, int green, int blue)
        {
            this.colorName = colorName;
            this.red = red;
            this.green = green;
            this.blue = blue;
        }

        public int IntColor
        {
            get
            {
               return (red << 16) | (green << 8) | blue;
            }
        }

        public string HexString
        {
            get {
                return "#" + Convert.ToString(IntColor, 16);
            }
        }

        public override string ToString()
        {
            return colorName + " [" + HexString + "]";
        }
    }
}
