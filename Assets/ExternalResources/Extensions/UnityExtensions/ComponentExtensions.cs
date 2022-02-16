using UnityEngine;

namespace Common {
    /// <summary>
    /// Extension methods related to Components
    /// </summary>
    public static class ComponentExtensions {
        /// <summary>
        /// Retrieves a required component
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="self"></param>
        /// <returns></returns>
        public static T GetRequiredComponent<T>(this Component self) where T : Component {
            T componentInstance = self.GetComponent<T>();
            Assertion.NotNull(componentInstance);
            return componentInstance;
        }

        public static Option<T> GetComponentAsOption<T>(this Component self) where T : Component {
            T component = self.GetComponent<T>();
            return component == null ? Option<T>.NONE : Option<T>.Some(component);
        }
    }
}
