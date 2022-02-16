using Sirenix.OdinInspector.Editor;

namespace Game {
    public abstract class GameManagerEditor : OdinEditor, IGameManagerDrawer {
        public virtual void PopulateTree(OdinMenuTree tree) {
        }

        public virtual void BeforeDrawingMenuTree() {
        }

        public virtual void Initialize() {
        }

        public virtual bool DisplayDefaultEditor {
            get {
                // Display the default editor by default which will use whatever Unity or Odin uses in the inspector
                return true;
            }
        }

        public virtual object? Target {
            get {
                return null;
            }
        }
    }
}