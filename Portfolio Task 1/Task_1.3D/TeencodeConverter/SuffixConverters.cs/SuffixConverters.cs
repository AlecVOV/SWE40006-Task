using System;
using System.Linq;

namespace Teencode.Extensions
{
    public static class SuffixConverters
    {
        // Helper to split words and add suffix
        private static string AddSuffix(string input, string suffix)
        {
            if (string.IsNullOrEmpty(input)) return "";
            var words = input.Split(' ');
            // Add suffix to every word
            for (int i = 0; i < words.Length; i++)
            {
                if (!string.IsNullOrWhiteSpace(words[i]))
                    words[i] = words[i] + suffix;
            }
            return string.Join(" ", words);
        }

        // Style 5: "Đây's là's..."
        public static string AddS(string input) => AddSuffix(input, "'s");

        // Style 6: "Đây'sss là'sss..."
        public static string AddSSS(string input) => AddSuffix(input, "'sss");

        // Style 11: "Đây'ss là'ss..."
        public static string AddSS(string input) => AddSuffix(input, "'ss");

        // Style 9: "nhây nhà nhí nhụ" (Consonant shift)
        public static string ToNhayNha(string input)
        {
            if (string.IsNullOrEmpty(input)) return "";
            // Replace starting consonants with "nh" for fun effect
            var words = input.Split(' ');
            for (int i = 0; i < words.Length; i++)
            {
                if (words[i].Length > 0)
                {
                    // Basic heuristic: replace first char with 'nh' if it's a consonant
                    char first = words[i][0];
                    if ("aeiouăâêôơưyAEIOU".IndexOf(first) == -1)
                    {
                        words[i] = "nh" + words[i].Substring(1);
                    }
                }
            }
            return string.Join(" ", words);
        }
    }
}