using System.Net.Mime;
using Veldrid;
using Perlin;
using Perlin.Display;

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
        public Player(Cell cell, Perlin.Geom.Rectangle tile)
            : base(cell, tile)
        {
            Program.OnKeyPressed += OnKey;
            this.Tile = Tiles.PlayerTile;
            Health = 50;
            _attackStrength = 5;
        }

        private int _attackStrength;

        /// <inheritdoc/>
        public override void OnEnter(Cell cell)
        {
            if (cell.Tilename == "Portal")
            {
                System.Environment.Exit(0);
            }
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

        /// <inheritdoc/>
        public override bool OnCollision(Actor other)
        {
            if (other.Tilename == "skeleton")
            {
                other.Health -= _attackStrength;
                return other.Health <= 0;
            }

            return true;
        }
    }
}