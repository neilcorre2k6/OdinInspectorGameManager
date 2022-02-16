using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game {
    public class GameManager : OdinMenuEditorWindow {
        // ==========================================
        // =====> ADD NEW DRAWERS IN THIS LIST <=====
        // ==========================================
        // Scriptable object drawers to display in the game manager
        private readonly List<IGameManagerDrawer> drawers = new List<IGameManagerDrawer> {
            new DrawAgentNeeds(),
        };

        // ======================================================
        // =====> ADD NEW GAME MANAGER STATES IN THIS ENUM <=====
        // ======================================================
        // ReSharper disable UnusedMember.Local
        private enum ManagerState {
            AgentNeeds
        }
        // ReSharper restore UnusedMember.Local

#region EditorCodes

        [OnValueChanged("SetGameManagerDirty")] // Call the method "SetGameManagerDirty" when this enum changes
        [LabelText("Manager View")]
        [LabelWidth(100f)]
        [PropertyOrder(-1)] // Ensure that this enum is always drawn first
        [EnumToggleButtons] // Render this enum as toggle buttons
        [ShowInInspector]
        private ManagerState currentManagerState;

        /// <summary>
        /// This is used to store the selected value before moving to a new tab. So, that the user
        /// does not need to select the same item again when returning to the previous tab.
        /// </summary>
        private ManagerState previousManagerState;

        /// <summary>
        /// This is the current drawer that will be used to render/draw the <see cref="currentSelectedValue"/>
        /// </summary>
        private IGameManagerDrawer? currentDrawer;

        /// <summary>
        /// This is the index of the toolbar or the enum toggles. Cached here so that it's easy to pull it and render it first
        /// before the selected manager state.
        /// </summary>
        private int topToolBarIndex;

        /// <summary>
        /// This is the current selected value in the <see cref="currentManagerState"/>.
        /// This is the value inside the target ScriptableObject that will be rendered by the <see cref="currentDrawer"/>.
        /// </summary>
        private object? currentSelectedValue;

        /// <summary>
        /// This is a list of selected values in each of the manager states. This is used so that the user does not
        /// need to select the same item again when returning to a state.
        /// </summary>
        private List<object?>? drawerTargets;

        /// <summary>
        /// Cache the menuTree so that we don't build a new one every time the manager is drawn.
        /// We just update the contents of this instance.
        /// </summary>
        private OdinMenuTree? menuTree;

        /// <summary>
        /// Implemented as a singleton since the rendering/repainting of windows can happen multiple times per frame
        /// and we only need to open one GameManager window.
        /// </summary>
        private static GameManager? INSTANCE;

        /// <summary>
        /// This is a flag to determine if the manager should be forced to rebuild the whole menu tree.
        /// Set this to public so that drawers can trigger the whole manager to be rebuilt if needed.
        /// </summary>
        private static bool IS_DIRTY = true;

        [MenuItem("Game/Game Manager #&g")] // Add a button in the toolbar to open the game manager with shift+alt+G as the shortcut
        public static void OpenGameManager() {
            if (INSTANCE != null) {
                // If a window exists already, focus on it
                INSTANCE.Focus();
            } else {
                // Create a window and show it
                GetWindow<GameManager>().Show();
            }
        }

        protected override void Initialize() {
            if (INSTANCE == null) {
                INSTANCE = this;
            }

            this.drawerTargets = new List<object?>();

            for (int i = 0; i < this.drawers.Count; ++i) {
                this.drawers[i].Initialize();

                // Start the target for this drawer as a null at first.
                // These values will be set when the user selects a value from the menu tree (left side)
                this.drawerTargets.Add(null);
            }

            // Add the enum tabs as the last element so that the other targets will follow their enum indexes
            // when being rendered.
            this.drawerTargets.Add(base.GetTarget());

            // Then, we just get the index of the enum tabs,
            // so that we can select it and render it at the top of the manager
            this.topToolBarIndex = this.drawerTargets.Count - 1;
        }

        protected override void OnDestroy() {
            base.OnDestroy();
            INSTANCE = null;
        }

        /// <summary>
        /// Force the game manager to be dirty so that the window will be redrawn.
        /// </summary>
        public static void SetGameManagerDirty() {
            IS_DIRTY = true;
        }

        protected override void OnGUI() {
            // Layout event is when the content and sizes of the window is subject to change and is being computed by UnityGUI
            if (IS_DIRTY && Event.current.type == EventType.Layout) {
                ForceMenuTreeRebuild();

                if (this.currentSelectedValue == null) {
                    // The selected value for the current game manager state is null, we clear the selection 
                    // so that there will be nothing to render in the right side.
                    // i.e. Prevent the manager from rendering the wrong value for the current selected game manager state (or scriptable object)
                    this.MenuTree.Selection.Clear();
                } else {
                    // The user selected a value before in the current game manager state (or scriptable object),
                    // we try to select the same value again and render it.
                    TrySelectMenuItemWithObject(this.currentSelectedValue);
                }

                // Reset the flag so that we don't rebuild again in the next frame
                IS_DIRTY = false;
            }

            EditorGUILayout.Space();
            // Draw the top tool bar, the enum toggle buttons
            DrawEditor(this.topToolBarIndex);
            EditorGUILayout.Space();

            // Then, render/draw the rest of the manager. See BuildMenuTree(), DrawEditors(), and DrawMenu() methods.
            base.OnGUI();
        }

        protected override void DrawEditors() {
            if (this.drawerTargets == null) {
                // The targets list has not been initialized yet
                return;
            }

            if (this.previousManagerState != this.currentManagerState) {
                // The manager haven't drawn the updated menu tree yet
                return;
            }

            if (this.currentDrawer != null && this.currentDrawer.DisplayDefaultEditor) {
                // Display the target by default based on how Unity and/or Odin normally display that type in the inspector
                this.drawerTargets[(int)this.currentManagerState] = this.currentDrawer.Target;
            } else {
                // Get the target for the current drawer from the menu tree (left side)
                OdinMenuTreeSelection? treeSelection = this.MenuTree?.Selection ?? null;
                this.currentSelectedValue = treeSelection?.SelectedValue;

                if (this.currentSelectedValue == null) {
                    // Don't draw the editor if no menu item is selected. Keep the right side empty.
                    return;
                }

                // Set the current selected value as the target to be displayed in the Manager's main body panel (right side) 
                this.drawerTargets[(int)this.currentManagerState] = this.currentSelectedValue;
            }

            // Draw the editor based on the data type of the current target, based on the current manager state
            DrawEditor((int)this.currentManagerState);
        }

        protected override IEnumerable<object?> GetTargets() {
            // Return the cached targets if the list exists
            return this.drawerTargets ?? base.GetTargets();
        }

        protected override void DrawMenu() {
            if (this.currentDrawer == null) {
                return;
            }

            if (this.currentDrawer.DisplayDefaultEditor) {
                // Don't display the left menu if the current drawer will display the default inspector
                return;
            }

            EditorGUILayout.Space();
            // Draw the menu items based on how the drawer wanted them to look like
            this.currentDrawer.BeforeDrawingMenuTree();
            EditorGUILayout.Space();

            // Draw the menu items based on the list made in BuildMenuTree()
            base.DrawMenu();
        }

        /// <summary>
        /// This builds the tree menu at the left side of the editor window
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        protected override OdinMenuTree BuildMenuTree() {
            if (this.menuTree == null) {
                // Create a menu tree and cache it so that we don't create a new one every frame
                this.menuTree = new OdinMenuTree();

                // Draw the search bar for the menu tree in the left side of the manager.
                // This will search through whatever the drawer will add in its PopulateTree() method.
                this.menuTree.Config.DrawSearchToolbar = true;
            } else {
                this.menuTree.MenuItems.Clear();
            }

            if (this.drawerTargets == null) {
                return this.menuTree;
            }

            // Save the target of the previous drawer, so that the user won't need to select it again 
            this.drawerTargets[(int)this.previousManagerState] = this.currentSelectedValue;

            // Get the stored selected value for the current state
            int currentStateIndex = (int)this.currentManagerState;
            this.currentSelectedValue = this.drawerTargets[currentStateIndex];

            // Update the previous state
            this.previousManagerState = this.currentManagerState;

            // Populate the menu items (left side( based on the drawer
            this.currentDrawer = this.drawers[currentStateIndex];
            this.currentDrawer.PopulateTree(this.menuTree);

            return this.menuTree;
        }

#endregion
    }
}