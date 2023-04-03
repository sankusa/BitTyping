using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace WeedLib
{
    public static class ColorEx
    {
        public static string ToColorCodeRGB(this Color color)
        {
            return ColorUtility.ToHtmlStringRGB(color);
        }

        public static Color FromHtmlString(this Color color, string htmlString)
        {
            bool success = ColorUtility.TryParseHtmlString(htmlString, out Color newColor);
            if(!success)
            {
                Debug.LogWarning("Parse failed. htmlString = " + htmlString);
                return color;
            }

            return newColor;
        }
    }
}