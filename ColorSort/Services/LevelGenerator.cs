using ColorSort.Models;

namespace ColorSort.Services {
    public class LevelGenerator {


        public GameState Generate(int level) {
            int colors = Math.Min(3 + (level / 5), 10);
            int emptyFlasks = 2;

            var state = new GameState {
                Level = level,
                RemainingUndos = 3
            };

            string[] palette =
            {
            "#FF5A5F",
            "#FBBF24",
            "#10B981",
            "#60A5FA",
            "#A78BFA",
            "#EC4899",
            "#14B8A6",
            "#F97316",
            "#84CC16",
            "#22C55E"
        };

            for (int i = 0; i < colors; i++) {
                var flask = new Flask();

                for (int j = 0; j < 4; j++)
                    flask.Contents.Add(new() { ColorId = palette[i] });

                state.Flasks.Add(flask);
            }

            for (int i = 0; i < emptyFlasks; i++)
                state.Flasks.Add(new Flask());

            var random = new Random();

            for (int shuffle = 0; shuffle < level * 20 + 100; shuffle++) {
                var a = random.Next(state.Flasks.Count);
                var b = random.Next(state.Flasks.Count);

                if (a == b)
                    continue;

                ReverseMove(state.Flasks[a], state.Flasks[b]);
            }

            return state;
        }

        private static void ReverseMove(Flask source, Flask destination) {
            if (source.IsEmpty || destination.IsFull)
                return;

            destination.Contents.Add(source.Contents.Last());
            source.Contents.RemoveAt(source.Contents.Count - 1);
        }

    }
}
