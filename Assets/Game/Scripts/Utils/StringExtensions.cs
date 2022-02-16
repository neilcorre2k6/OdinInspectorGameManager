using System;

namespace Game {
    /// <summary>
    /// Contains string extension methods
    /// </summary>
    public static class StringExtensions {
        /// <summary>
        /// Custom equals that require a string parameter to avoid mistake of passing any object
        /// This is also faster
        /// </summary>
        /// <param name="source"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool EqualsFast(this string source, string other) {
            return source.Equals(other, StringComparison.Ordinal);
        }
    }
}