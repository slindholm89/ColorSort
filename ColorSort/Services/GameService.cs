using ColorSort.Models;

namespace ColorSort.Services {
    public class GameService {

        public GameState State { get; private set; } = new();

        public event Action? Changed;

        public void NewLevel(GameState generated) {
            State = generated;
            Notify();
        }

        public bool SelectFlask(int index) {
            if (State.Paused)
                return false;

            if (State.SelectedFlask == -1) {
                State.SelectedFlask = index;
                Notify();
                return true;
            }

            var source = State.SelectedFlask;
            State.SelectedFlask = -1;

            var moved = TryMove(source, index);
            Notify();
            return moved;
        }

        public bool TryMove(int from, int to) {
            if (from == to)
                return false;

            var source = State.Flasks[from];
            var destination = State.Flasks[to];

            if (source.IsEmpty)
                return false;

            if (destination.IsFull)
                return false;

            var color = source.TopColor!;

            if (!destination.IsEmpty && destination.TopColor != color)
                return false;

            var amount = source.Contents
                .AsEnumerable()
                .Reverse()
                .TakeWhile(x => x.ColorId == color)
                .Count();

            var freeSpace = destination.Capacity - destination.Contents.Count;

            amount = Math.Min(amount, freeSpace);

            for (int i = 0; i < amount; i++) {
                destination.Contents.Add(source.Contents.Last());
                source.Contents.RemoveAt(source.Contents.Count - 1);
            }

            State.History.Push(new MoveRecord(from, to, amount, color));
            
            return true;
        }

        public void Undo() {
            if (State.RemainingUndos <= 0)
                return;

            if (!State.History.Any())
                return;

            var move = State.History.Pop();

            var source = State.Flasks[move.Source];
            var destination = State.Flasks[move.Destination];

            for (int i = 0; i < move.Amount; i++) {
                source.Contents.Add(destination.Contents.Last());
                destination.Contents.RemoveAt(destination.Contents.Count - 1);
            }

            State.RemainingUndos--;

            Notify();
        }

        public bool IsCompleted() {
            return State.Flasks.All(x => x.IsEmpty || x.IsSolved);
        }

        public void Notify() => Changed?.Invoke();

    }
}
