using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEditor;
using UnityEngine;

namespace Game {
    public class DrawAgentNeeds : DrawScriptableObject<AgentNeeds> {
        private string nameForNew = string.Empty;

        public override bool DisplayDefaultEditor {
            get {
                return false;
            }
        }

        public override void BeforeDrawingMenuTree() {
            if (this.target == null) {
                return;
            }

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

        private void CreateNew() {
            if (string.IsNullOrEmpty(this.nameForNew)) {
                return;
            }

            if (this.target == null) {
                return;
            }


            IReadOnlyList<AgentNeedsMapData> existingAgentNeeds = this.target.AgentNeedsMap;

            for (int i = 0; i < existingAgentNeeds.Count; ++i) {
                AgentNeedsMapData existingData = existingAgentNeeds[i];

                // Prevent duplicates
                if (existingData.GoapDomainId.EqualsFast(this.nameForNew)) {
                    EditorUtility.DisplayDialog($"{this.nameForNew} already exists!",
                        $"{this.nameForNew} already exists in {this.Path}.", "OK");
                    return;
                }
            }

            AgentNeedsMapData newItem = new AgentNeedsMapData(this.nameForNew);
            this.target.AgentNeedsMap.Add(newItem);

            this.nameForNew = string.Empty;
            EditorUtility.SetDirty(this.target);
            GameManager.SetGameManagerDirty();
        }

        public override void PopulateTree(OdinMenuTree tree) {
            base.PopulateTree(tree);

            if (this.target == null) {
                return;
            }

            List<AgentNeedsMapData> agentNeedsMap = this.target.AgentNeedsMap;

            // Add a menu button for each goal selectors
            for (int i = 0; i < agentNeedsMap.Count; ++i) {
                AgentNeedsMapData needsMapData = agentNeedsMap[i];
                string goapDomainId = needsMapData.GoapDomainId;

                OdinMenuItem newMenuItem = new OdinMenuItem(tree, goapDomainId, needsMapData);
                newMenuItem.OnDrawItem = delegate(OdinMenuItem item) {
                    GUI.backgroundColor = Color.red;

                    Rect rect = new Rect(item.LabelRect);
                    // This is width plus padding
                    rect.x = rect.xMax - 25;
                    rect.width = 20;

                    // This adds a red "X" button at the right side of the menu item
                    if (GUI.Button(rect, "X")) {
                        if (EditorUtility.DisplayDialog($"Delete {goapDomainId}",
                            $"Are you sure you want to delete {goapDomainId}? "
                            + "This can't be undone.", "Delete", "Cancel")) {
                            this.target.AgentNeedsMap.Remove(needsMapData);
                            EditorUtility.SetDirty(this.target);
                            AssetDatabase.SaveAssets();
                            GameManager.SetGameManagerDirty();
                        }
                    }

                    GUI.backgroundColor = Color.white;
                };

                tree.MenuItems.Insert(i, newMenuItem);
            }
        }
    }
}