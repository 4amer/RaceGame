using System.Collections.Generic;
using UnityEngine;

namespace Exceptions
{
    public static class ColorExtension
    {
        private static Dictionary<ColorName, string> keyColors = new Dictionary<ColorName, string>
        {
            { ColorName.Oragne, "#DECC26" }, // ����������
            { ColorName.Red, "#D91552" },    // �������
            { ColorName.Green, "#35C739" },  // �������
            { ColorName.Blue, "#3271C0" },   // �����
            { ColorName.Violet, "#A53AC3" }, // ����������
            { ColorName.Mint, "#40D0AF" },   // ������
            { ColorName.White, "#FFFFFF" },   // �����
            { ColorName.Disabled, "#6f7e87" },   // ��������
            { ColorName.Box, "#f9f7f4" }   // �������
        };

        public static Color GetColorByName(ColorName colorName)
        {
            if (keyColors.TryGetValue(colorName, out string hex))
            {
                Color color;
                if (ColorUtility.TryParseHtmlString(hex, out color))
                {
                    color.a = 0.95f;
                    return color;
                }
            }
            Debug.LogError($"���� � ������ {colorName} �� ������.");
            return Color.black;
        }

        public static ColorName GetColorNameByColor(Color color)
        {
            string hex = $"#{ColorUtility.ToHtmlStringRGB(color)}";

            foreach (var pair in keyColors)
            {
                if (pair.Value.Equals(hex, System.StringComparison.OrdinalIgnoreCase))
                {
                    return pair.Key;
                }
            }

            Debug.LogError($"���� {color} �� ������ � �������.");
            return ColorName.None;
        }
    }

    public enum ColorName
    {
        None = 0,
        Oragne = 1,
        Red = 2,
        Green = 3,
        Blue = 4,
        Violet = 5,
        Mint = 6,
        White = 7,
        Disabled = 99,
        Box = 100,
    }
}
