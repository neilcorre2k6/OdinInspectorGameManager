using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game {
    [CreateAssetMenu(menuName = "Game/GameColors")]
    public class GameColors : ScriptableObject {
        [ShowInInspector]
        private List<Entry>? entries;

        [Serializable]
        public struct Entry {
            public string id;
            public Color color;
        }

        // =====> Getters and other resolvers needed for this mechanic is added here
    }
}