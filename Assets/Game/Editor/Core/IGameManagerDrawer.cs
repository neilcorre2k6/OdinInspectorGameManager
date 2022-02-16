using Sirenix.OdinInspector.Editor;

namespace Game {
    /// <summary>
    /// This is the interface for all custom editors that will be added in the <see cref="GameManager"/>.
    /// </summary>
    public interface IGameManagerDrawer {
        /// <summary>
        /// This is called when building the menu tree via <see cref="GameManager.BuildMenuTree"/>
        /// </summary>
        /// <param name="tree"></param>
        public void PopulateTree(OdinMenuTree tree);

        /// <summary>
        /// This is called before the default <see cref="GameManager.DrawMenu"/>
        /// </summary>
        public void BeforeDrawingMenuTree();

        /// <summary>
        /// This is called in <see cref="GameManager.Initialize"/> when the game manager is first initialized/created.
        /// </summary>
        public void Initialize();

        /// <summary>
        /// This determines whether the game manager will use a custom editor window to render the target scriptable object
        /// or use Unity's or Odin's default window.
        /// </summary>
        public bool DisplayDefaultEditor { get; }

        /// <summary>
        /// This is the target scriptable object of this drawer.
        /// </summary>
        public object? Target { get; }
    }
}