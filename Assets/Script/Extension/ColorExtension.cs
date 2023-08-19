using System;
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
            Color c = new Color((color.r + other.r) / 2f, (color.g + other.g) / 2f, (color.b + other.b) / 2f);
            c.a = 1f;
            //Color c = new Color(Math.Min(color.r / 2 + other.r, 1f), Math.Min(other.g / 2 + other.g, 1f),
              //  Math.Min(other.b / 2 + other.b, 1f));
            return c;
        }

        public static bool SimilarColor(this Color color, Color other, float threshold)
        {
            return !(Math.Abs(color.r - other.r) > threshold) && !(Math.Abs(color.g - other.g) > threshold) &&
                   !(Math.Abs(color.b - other.b) > threshold);
        }
    }
}