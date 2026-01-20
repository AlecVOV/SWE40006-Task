using System;
using System.Text;

namespace Teencode.Core
{
    public static class BasicConverters
    {
        // Style 1: "day la vj du"
        public static string ToVjDu(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("i", "j").Replace("I", "J")
                        .Replace("h", "k").Replace("H", "K")
                        .Replace("o", "0").Replace("a", "4");
        }

        // Style 8: "d4y l4 vj du"
        public static string ToLeet(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            return input.Replace("a", "4").Replace("A", "4")
                        .Replace("e", "3").Replace("E", "3")
                        .Replace("i", "1").Replace("I", "1")
                        .Replace("o", "0").Replace("O", "0");
        }

        // Style 12: "ĐâY Là Ví Dụ"
        public static string ToAlternatingCaps(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < input.Length; i++)
                sb.Append(i % 2 == 0 ? char.ToUpper(input[i]) : char.ToLower(input[i]));
            return sb.ToString();
        }

        // Style 3: "Đây nà ví dụ" (N/L Swap)
        public static string ToNaViDu(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            // Simple swap logic for demo
            StringBuilder sb = new StringBuilder();
            foreach (char c in input)
            {
                if (c == 'n') sb.Append('l');
                else if (c == 'l') sb.Append('n');
                else if (c == 'N') sb.Append('L');
                else if (c == 'L') sb.Append('N');
                else sb.Append(c);
            }
            return sb.ToString();
        }
    }
}