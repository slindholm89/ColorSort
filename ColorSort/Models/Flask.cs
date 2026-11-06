namespace ColorSort.Models {
    public sealed class Flask {

        public int Capacity { get; set; } = 4;

        public List<LiquidUnit> Contents { get; set; } = new();

        public bool IsEmpty => Contents.Count == 0;

        public bool IsFull => Contents.Count >= Capacity;

        public string? TopColor => IsEmpty ? null : Contents.Last().ColorId;

        public bool IsSolved =>
            IsEmpty ||
            (Contents.Count == Capacity &&
             Contents.All(x => x.ColorId == Contents[0].ColorId));

    }
}
