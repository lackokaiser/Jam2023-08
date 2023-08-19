using UnityEngine;

namespace Script.Extension
{
    public static class ColorExtension
    {
        public static Color InvertColor(this Color color)
        {
            var maxColor = color.maxColorComponent;
            return new Color(maxColor-color.r, maxColor-color.g, maxColor-color.b);
        }

        public static Color CombineColor(this Color color, Color other)
        {
            Color c = new Color(color.r + other.r, other.g + other.g, other.b + other.b);
            return c;
        }
    }
}