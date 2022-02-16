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

        public List<NeedsEditorData> NeedsMap => this.needsMap;
    }
}