using UnityEngine;

namespace Game {
    /// <summary>
    /// A utility class for GUI styles that caches already created styles (prevents garbage).
    /// </summary>
    public static class CommonGuiStyles {
        private static GUIStyle? FLEXIBLE_BUTTON;

        /// <summary>
        /// Returns a flexible button style with wordwrap
        /// </summary>
        public static GUIStyle FlexibleButton {
            get {
                return FLEXIBLE_BUTTON ??= new GUIStyle(GUI.skin.button) {
                    wordWrap = true
                };
            }
        }

        private static GUIStyle? FLEXIBLE_LABEL;

        /// <summary>
        /// Returns a flexible label style with wordwrap
        /// </summary>
        public static GUIStyle FlexibleLabel {
            get {
                return FLEXIBLE_LABEL ??= new GUIStyle(GUI.skin.label) {
                    wordWrap = true
                };
            }
        }
    }
}