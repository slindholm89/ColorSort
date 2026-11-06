namespace ColorSort.Models {
    public sealed class GameState {

        public int Level { get; set; } = 1;

        public int RemainingUndos { get; set; } = 3;

        public bool Paused { get; set; }

        public int SelectedFlask { get; set; } = -1;

        public DateTime StartedAt { get; set; } = DateTime.UtcNow;

        public List<Flask> Flasks { get; set; } = new();

        public Stack<MoveRecord> History { get; set; } = new();

        public TimeSpan AccumulatedTime { get; set; }

        public TimeSpan Elapsed => Paused ? AccumulatedTime : AccumulatedTime + (DateTime.UtcNow - StartedAt);
    }
}
