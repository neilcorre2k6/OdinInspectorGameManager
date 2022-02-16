using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(fileName = "AgentNeeds", menuName = "Game/AgentNeeds", order = 0)]
    public class AgentNeeds : ScriptableObject {
        [ShowInInspector]
        // [SerializeField] can also be used here
        private List<AgentNeedsMapData> agentNeedsMap = new List<AgentNeedsMapData>();

        public List<AgentNeedsMapData> AgentNeedsMap => this.agentNeedsMap;

        // =====> Getters and other resolvers needed for this mechanic is added here
    }
}