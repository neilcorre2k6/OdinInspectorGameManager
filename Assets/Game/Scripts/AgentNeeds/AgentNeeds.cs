using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "AgentNeeds", menuName = "Game/AgentNeeds", order = 0)]
    public class AgentNeeds : ScriptableObject {
        [ShowInInspector]
        [SerializeField]
        private List<AgentNeedsMapData> agentNeedsMap = new List<AgentNeedsMapData>();

        public List<AgentNeedsMapData> AgentNeedsMap => this.agentNeedsMap;

        public bool ContainsGoapId(string goapDomainId) {
            for (int i = 0; i < this.agentNeedsMap.Count; ++i) {
                string needId = this.agentNeedsMap[i].GoapDomainId;

                if (needId == goapDomainId) {
                    return true;
                }
            }

            return false;
        }
    }
}