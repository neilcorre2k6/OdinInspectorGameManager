using UnityEngine;

namespace Common {
    public static class Vector2Extensions {
        /// <summary>
        /// Vector2 equals comparison
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static bool TolerantEquals(this Vector2 a, Vector2 b) {
            return a.x.TolerantEquals(b.x) && a.y.TolerantEquals(b.y);
        }
        
        /// <summary>
        /// Returns a random vector that is between the specified extents
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public static Vector2 RandomRange(this Vector2 a, Vector2 b) {
            float x = Random.Range(a.x, b.x);
            float y = Random.Range(a.y, b.y);

            return new Vector2(x, y);
        }
    }
}