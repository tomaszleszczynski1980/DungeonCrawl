using Veldrid;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// The game player
    /// </summary>
    public class Player : Actor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Player"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Player(Cell cell)
            : base(cell)
        {
            Program.OnKeyPressed += OnKey;
        }

        /// <summary>
        /// Action taken on key pressed
        /// </summary>
        /// <param name="k">key that is pressed</param>
        public void OnKey(Key k)
        {
            switch (k)
            {
                case Key.Up:
                    Move(0, -1);
                    break;
                case Key.Down:
                    Move(0, 1);
                    break;
                case Key.Left:
                    Move(-1, 0);
                    break;
                case Key.Right:
                    Move(1, 0);
                    break;
            }
        }

        /// <inheritdoc/>
        public override string Tilename => "player";
    }
}