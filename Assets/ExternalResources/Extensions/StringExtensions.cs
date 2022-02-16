using System;
using System.Text;

using UnityEngine;

namespace Common {
    /// <summary>
    /// Contains string extension methods
    /// </summary>
    public static class StringExtensions {

        /// <summary>
        /// Returns whether or not the string contains the specified search criteria without case sensitivity
        /// </summary>
        /// <param name="source"></param>
        /// <param name="search"></param>
        /// <returns></returns>
        public static bool CaseInsensitiveContains(this string source, string search) {
            return source.ToLower().Contains(search.ToLower());
        }

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

        /// <summary>
        /// Formats the string with the specified parameters
        /// </summary>
        /// <param name="source"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public static string FormatWith(this string source, params object[] parameters) {
            try {
                return string.Format(source, parameters);
            } catch(Exception e) {
                Debug.LogError(source);
                Debug.LogError(e.Message);

                // We return source here even if it's wrong to prevent the error from propagating
                // to callers
                return source;
            }
        }

        /// <summary>
        /// A custom StartsWith() implementation that is faster as specified in
        /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool StartsWithFast(this string a, string b) {
            int aLen = a.Length;
            int bLen = b.Length;
            int aIndex = 0;
            int bIndex = 0;

            while (aIndex < aLen && bIndex < bLen && a[aIndex] == b[bIndex]) {
                ++aIndex;
                ++bIndex;
            }

            return bIndex == bLen && aLen >= bLen || aIndex == aLen && bLen >= aLen;
        }

        /// <summary>
        /// A custom EndsWith() implementation that is faster as specified in
        /// https://docs.unity3d.com/Manual/BestPracticeUnderstandingPerformanceInUnity5.html
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool EndsWithFast(this string a, string b) {
            int aIndex = a.Length - 1;
            int bIndex = b.Length - 1;

            while (aIndex >= 0 && bIndex >= 0 && a[aIndex] == b[bIndex]) {
                --aIndex;
                --bIndex;
            }

            return bIndex < 0 && a.Length >= b.Length || aIndex < 0 && b.Length >= a.Length;
        }

        /// <summary>
        /// Clears a StringBuilder
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public static StringBuilder Clear(this StringBuilder builder) {
            builder.Remove(0, builder.Length);

            return builder;
        }

    }
}