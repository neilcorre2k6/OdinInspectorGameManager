using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game {
    /// <summary>
    /// Used a class here to prevent boxing when generating the menu items in the GameManager. See DrawAgentNeeds.cs.
    /// </summary>
    [Serializable]
    public class AgentNeedsMapData {
        [SerializeField]
        private string agentId;

        [SerializeField]
        private List<NeedsEditorData> needsMap;

        public AgentNeedsMapData(string goapDomainId) {
            this.agentId = goapDomainId;
            this.needsMap = new List<NeedsEditorData>();
        }

        public string GoapDomainId => this.agentId;

        [Serializable]
        public struct NeedsEditorData {
            [SerializeField]
            private NeedsEnum needId;

            [SerializeField]
            private int minHour;

            [SerializeField]
            private int maxHour;
        }

        public enum NeedsEnum {
            Bladder = 0,
            Bowel = 1,
            Food = 2
        }

        /*
         * Ideally, the NeedsEditorData struct and the NeedsEnum would be in a separate file.
         * But, for simplicity's sake, I included them here in the same file.
         */
    }
}