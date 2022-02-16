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

        protected string nameForNew = string.Empty;

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

        protected virtual void CreateNew() {
            if (string.IsNullOrEmpty(this.nameForNew)) {
                return;
            }

            T newItem = ScriptableObject.CreateInstance<T>();
            newItem.name = this.nameForNew;

            this.searchInFolders[0] = $"{this.Path}";
            string[] foundAssetsGuids = AssetDatabase.FindAssets($"{this.nameForNew}", this.searchInFolders);

            // Prevent duplicates
            if (foundAssetsGuids != null && foundAssetsGuids.Length > 0) {
                EditorUtility.DisplayDialog($"{this.nameForNew} already exists!",
                    $"{this.nameForNew} already exists in {this.Path}.", "OK");
                return;
            }

            AssetDatabase.CreateAsset(newItem, $"{this.Path}/{this.nameForNew}.asset");
            AssetDatabase.SaveAssets();

            this.nameForNew = string.Empty;

            EditorUtility.SetDirty(this.target);
        }

        public virtual void PopulateTree(OdinMenuTree tree) {
        }

        public virtual void BeforeDrawingMenuTree() {
            GUILayout.BeginHorizontal();
            GUILayout.Label("New: ", GUILayout.Width(40));
            this.nameForNew = EditorGUILayout.TextField(this.nameForNew).Trim();

            if (GUILayout.Button("Add", GUILayout.Width(40), GUILayout.Height(15))) {
                if (!string.IsNullOrEmpty(this.nameForNew)) {
                    // Add the new item
                    CreateNew();
                    this.nameForNew = "";
                }
            }

            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Path: ", GUILayout.Width(40));
            this.Path = EditorGUILayout.TextField(this.Path).Trim();
            GUILayout.EndHorizontal();

            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save", CommonGuiStyles.FlexibleButton)) {
                // Save the current opened Game Manager tab
                EditorUtility.SetDirty(this.target);
                AssetDatabase.SaveAssets();
                GameManager.SetGameManagerDirty();
                EditorUtility.DisplayDialog("Save", "Save Successful", "OK");
            }

            GUILayout.EndHorizontal();
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