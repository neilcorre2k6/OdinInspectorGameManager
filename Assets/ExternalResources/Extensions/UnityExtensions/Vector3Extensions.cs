using UnityEngine;

namespace Common {
    /// <summary>
    /// Contains Vector3 extension methods
    /// </summary>
    public static class Vector3Extensions {
        /// <summary>
        /// Computes for the the 2D magnitude of the specified Vector3 (ignores z)
        /// </summary>
        /// <param name="v"></param>
        /// <returns></returns>
        public static float Magnitude2D(this Vector3 v) {
            return Mathf.Sqrt((v.x * v.x) + (v.y * v.y));
        }

        /// <summary>
        /// Returns whether or not the specified vectors a and b are equal.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool TolerantEquals(this Vector3 a, Vector3 b) {
            return a.x.TolerantEquals(b.x) && a.y.TolerantEquals(b.y) &&
                   a.z.TolerantEquals(b.z);
        }

        /// <summary>
        ///Returns whether or not the specified vector is zero or not based on the extended float tolerance.
        /// See <see cref="FloatExtensions"/> for the tolerance value.
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public static bool IsTolerantZero(this Vector3 a) {
            return a.x.IsZero() && a.y.IsZero() &&
                   a.z.IsZero();
        }

        /// <summary>
        /// Adds a Vector2 to the Vector3
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector3 Add(this Vector3 a, Vector2 b) {
            return new Vector3(a.x + b.x, a.y + b.y, a.z);
        }
    }
}