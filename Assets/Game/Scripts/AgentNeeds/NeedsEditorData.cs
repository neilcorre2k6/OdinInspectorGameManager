using System;
using UnityEngine;

namespace Game {
    [Serializable]
    public struct NeedsEditorData {
        [SerializeField]
        private NeedsEnum needId;

        [SerializeField]
        private int minHour;

        [SerializeField]
        private int maxHour;

        [SerializeField]
        private int minMinutes;

        [SerializeField]
        private int maxMinutes;

        public NeedsEnum NeedId => this.needId;

        public int MinHour => this.minHour;

        public int MaxHour => this.maxHour;

        public int MinMinutes => this.minMinutes;

        public int MaxMinutes => this.maxMinutes;
    }
}