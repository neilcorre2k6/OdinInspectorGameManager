using System;
using System.Collections.Generic;

namespace Common {
    /// <summary>
    /// Contains extensions to the Dictionary class
    /// </summary>
    public static class DictionaryExtensions {
        /// <summary>
        /// A more safe version of Find()
        /// </summary>
        /// <param name="dictionary"></param>
        /// <param name="key"></param>
        /// <typeparam name="K"></typeparam>
        /// <typeparam name="V"></typeparam>
        /// <returns></returns>
        public static Option<V> Find<K, V>(this Dictionary<K, V> dictionary, K key) {
            return dictionary.TryGetValue(key, out V value) ? Option<V>.Some(value) : Option<V>.NONE;
        }

        public static V FindMustExist<K, V>(this Dictionary<K, V> dictionary, K key) {
            if (dictionary.TryGetValue(key, out V value)) {
                return value;
            }

            throw new Exception($"Can't find entry for key \"{key}\".");
        }
    }
}
