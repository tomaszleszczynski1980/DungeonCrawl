using System;
using System.Runtime.CompilerServices;
using Veldrid;

namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// Sample enemy
    /// </summary>
    public class Skeleton : Actor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Skeleton"/> class.
        /// </summary>
        /// <param name="cell">The starting cell</param>
        public Skeleton(Cell cell, Perlin.Geom.Rectangle tile)
            : base(cell, tile)
        {
            this.Tile = Tiles.SkeletonTile;
            Health = 30;
            _attackStrenght = 10;
        }

        private int _attackStrenght;

        /// <summary>
        /// Gets action taken on key pressed (Player).
        /// Other sprites move on key pressed by player.
        /// </summary>
        /// <param name="k">key that is pressed</param>
        public void OnKey(Key k)
        {
            Random rnd = new Random();
            Move(rnd.Next(-1, 2), rnd.Next(-1, 2));
        }

        /// <inheritdoc/>
        public override string Tilename => "skeleton";

        /// <inheritdoc/>
        public override bool OnCollision(Actor other)
        {
            if (other.Tilename == "skeleton")
            {
                return false;
            }
            else if (other.Tilename == "player")
            {
                other.Health -= _attackStrenght;
                return this.Health <= 0;
            }

            return true;
        }
    }
}