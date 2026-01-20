using System;
using System.Text;

namespace Teencode.Special
{
    public static class SymbolConverters
    {
        // Style 2: "up l^ ..." (Upside down simulation)
        public static string ToUpsideDown(string input)
        {
            // Simplified mapping for demo
            string normal = "abcdefghijklmnopqrstuvwxyz";
            string upside = "ɐqɔpǝɟ6ɥıɾʞlɯuodbɹsʇnʌʍxʎz";
            StringBuilder sb = new StringBuilder();
            foreach (char c in input.ToLower())
            {
                int index = normal.IndexOf(c);
                sb.Append(index >= 0 ? upside[index] : c);
            }
            return sb.ToString();
        }

        // Style 4: "+)4ij |_4` vj' ])u." (ASCII Art)
        public static string ToAsciiArt1(string input)
        {
            return input.Replace("d", "+)").Replace("D", "+)")
                        .Replace("a", "4").Replace("l", "|_")
                        .Replace("v", "vj'").Replace("u", "])u");
        }

        // Style 10: "+)Cl¥ lCl` V]' ])µ" (Hardcore Symbols)
        public static string ToHardcoreSymbols(string input)
        {
            return input.Replace("d", "+)C").Replace("D", "+)C")
                        .Replace("a", "¥").Replace("e", "3")
                        .Replace("i", "j").Replace("o", "()nt")
                        .Replace("u", "µ");
        }

        // Style 7: Number Code (Simple Cipher)
        public static string ToNumbers(string input)
        {
            StringBuilder sb = new StringBuilder();
            foreach (char c in input.ToUpper())
            {
                if (char.IsLetter(c))
                    sb.Append(((int)c - 64).ToString()); // A=1, B=2
                else
                    sb.Append(c);
            }
            return sb.ToString();
        }
    }
}