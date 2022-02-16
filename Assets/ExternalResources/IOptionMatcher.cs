namespace Common {
    public interface IOptionMatcher<in T> {
        /// <summary>
        /// Handling when there's a value
        /// </summary>
        /// <param name="value"></param>
        void OnSome(T value);

        /// <summary>
        /// Handling when there's no value
        /// </summary>
        void OnNone();
    }
}