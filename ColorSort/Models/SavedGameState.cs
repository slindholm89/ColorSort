namespace ColorSort.Models {
    public sealed class SavedGameState {
        public int Level { get; set; }

        public int RemainingUndos { get; set; }

        public DateTime StartedAt { get; set; }

        public TimeSpan AccumulatedTime { get; set; }

        public bool Paused { get; set; }

        public List<Flask> Flasks { get; set; } = new();

        public List<MoveRecord> History { get; set; } = new();
    }
}
