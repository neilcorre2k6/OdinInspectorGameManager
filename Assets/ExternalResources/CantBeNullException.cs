using System;

namespace Common {
    /// <summary>
    /// A common exception so that we don't have to type "{item} can't be null"
    /// </summary>
    public sealed class CantBeNullException : Exception {
        public CantBeNullException(string item) : base($"{item} can't be null") {
        }
    }
}