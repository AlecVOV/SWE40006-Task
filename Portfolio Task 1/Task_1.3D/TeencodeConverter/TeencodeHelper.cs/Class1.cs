using System;
using System.Text;

namespace TeencodeConverter
{
    public static class TeencodeHelper
    {
        // Style 1: "day la vj du" (Replace i -> j, h -> k, etc.)
        public static string ToVjDuStyle(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("i", "j").Replace("I", "J")
                        .Replace("h", "k").Replace("H", "K")
                        .Replace("o", "0")
                        .Replace("a", "4");
        }

        // Style 2: "d4y l4 vj du" (Leet speak / Numbers)
        public static string ToNumberStyle(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("a", "4").Replace("e", "3")
                        .Replace("i", "1").Replace("o", "0")
                        .Replace("s", "5").Replace("g", "9");
        }

        // Style 3: "ĐâY Là Ví Dụ" (Alternating Caps)
        public static string ToAlternatingCaps(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
            {
                // Even index uppercase, odd index lowercase (or vice versa)
                sb.Append(i % 2 == 0 ? char.ToUpper(input[i]) : char.ToLower(input[i]));
            }
            return sb.ToString();
        }

        // Style 4: "Teencode" (Special chars)
        public static string ToSpecialChars(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("a", "α").Replace("b", "ß")
                        .Replace("c", "©").Replace("d", "∂");
        }
    }
}