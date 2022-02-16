using System.Collections.Generic;
using System.Text;

namespace Common {
    public static class IntExtensions {
        private static readonly StringBuilder STRING_BUILDER = new StringBuilder();

        private static readonly Dictionary<int, string> NUMBER_ROMAN_DICTIONARY = new Dictionary<int, string> {
            {1000, "M"},
            {900, "CM"},
            {500, "D"},
            {400, "CD"},
            {100, "C"},
            {90, "XC"},
            {50, "L"},
            {40, "XL"},
            {10, "X"},
            {9, "IX"},
            {5, "V"},
            {4, "IV"},
            {1, "I"},
        };

        /// <summary>
        /// Converts an integer to a Roman Numeral.
        /// Reference: https://stackoverflow.com/questions/7040289/converting-integers-to-roman-numerals
        /// </summary>
        /// <param name="number"></param>
        /// <returns></returns>
        public static string ToRoman(this int number) {
            STRING_BUILDER.Clear();

            foreach (KeyValuePair<int, string> item in NUMBER_ROMAN_DICTIONARY) {
                while (number >= item.Key) {
                    STRING_BUILDER.Append(item.Value);
                    number -= item.Key;
                }
            }

            return STRING_BUILDER.ToString();
        }
    }
}