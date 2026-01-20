using System;
using Teencode.Core;       // From DLL 1
using Teencode.Special;    // From DLL 2
using Teencode.Extensions; // From DLL 3

namespace TeencodeConverter
{
    // This class aggregates all logic from the external DLLs
    public static class TeencodeHelper
    {
        public static string Convert(string input, int styleIndex)
        {
            switch (styleIndex)
            {
                case 1: return BasicConverters.ToVjDu(input);
                case 2: return SymbolConverters.ToUpsideDown(input);
                case 3: return BasicConverters.ToNaViDu(input);
                case 4: return SymbolConverters.ToAsciiArt1(input);
                case 5: return SuffixConverters.AddS(input);
                case 6: return SuffixConverters.AddSSS(input);
                case 7: return SymbolConverters.ToNumbers(input);
                case 8: return BasicConverters.ToLeet(input);
                case 9: return SuffixConverters.ToNhayNha(input);
                case 10: return SymbolConverters.ToHardcoreSymbols(input);
                case 11: return SuffixConverters.AddSS(input);
                case 12: return BasicConverters.ToAlternatingCaps(input);
                default: return input;
            }
        }
    }
}