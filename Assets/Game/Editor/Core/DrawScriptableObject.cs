using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game {
    /// <summary>
    /// A common concrete implementation of the <see cref="IGameManagerDrawer"/> interface.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DrawScriptableObject<T> : IGameManagerDrawer where T : ScriptableObject {
        /// <summary>
        /// The current target scriptable object of this drawer.
        /// </summary>
        protected T? target;

        /// <summary>
        /// Cached array for where to search for the existence of a new scriptable object, when creating a new object.
        /// </summary>
        protected readonly string[] searchInFolders = new string[1];

        private const string DEFAULT_ASSETS_PATH = "Assets/";
        protected string path = DEFAULT_ASSETS_PATH;

        /// <summary>
        /// This is the path where new instances of <see cref="T"/> will be saved
        /// </summary>
        protected string Path {
            get {
                return this.path;
            }
            set {
                this.path = value;
            }
        }

        /// <summary>
        /// The current target scriptable object of this drawer.
        /// </summary>
        public virtual object? Target {
            get {
                return this.target;
            }
        }

        /// <summary>
        /// Display the default editor by default which will use whatever Unity or Odin uses in the inspector.
        /// Defaults to true so that clients can just add a new scriptable object to the game manager without worrying
        /// if a drawer/renderer is available.
        /// </summary>
        public virtual bool DisplayDefaultEditor {
            get {
                return true;
            }
        }

        public virtual void PopulateTree(OdinMenuTree tree) {
        }

        public virtual void BeforeDrawingMenuTree() {
        }

        public virtual void Initialize() {
            // Get the scriptable object from the default location
            string typeAsString = typeof(T).ToString();

            this.searchInFolders[0] = "Assets/Game/ScriptableObjects/";
            string[] foundAssetGuids = AssetDatabase.FindAssets($"t:{typeAsString}", this.searchInFolders);

            if (foundAssetGuids == null || foundAssetGuids.Length <= 0) {
                EditorUtility.DisplayDialog($"{typeAsString} Not Found!", $"There is no {typeAsString} defined anywhere under Assets/Game/ScriptableObjects/. Did you forget to create one?", "OK");
                return;
            }

            // Set the first found scriptable object of this type as the target for this drawer
            string firstMatchPath = AssetDatabase.GUIDToAssetPath(foundAssetGuids[0]);
            this.target = AssetDatabase.LoadAssetAtPath<T>(firstMatchPath);

            if (this.target == null) {
                EditorUtility.DisplayDialog($"{typeAsString} Not Found!", $"There is no {typeAsString} defined. Did you forget to create one?", "OK");
                return;
            }

            if (string.IsNullOrEmpty(firstMatchPath)) {
                EditorUtility.DisplayDialog($"{typeof(T)} Found!", $"There is no {typeof(T)} defined. Did you forget to create one?", "OK");
                return;
            }

            // Null check is performed above
            this.path = firstMatchPath;
        }
    }
}