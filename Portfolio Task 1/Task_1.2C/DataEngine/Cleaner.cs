using System;

namespace DataEngine
{
    public class Cleaner
    {
        public string ProcessData(string input)
        {
            if (string.IsNullOrEmpty(input)) return "No data entered!";
            // Logic: Uppercase and Trim
            return $"[CLEANED]: {input.Trim().ToUpper()}";
        }
    }
}