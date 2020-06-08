using System;
using Perlin.Display;
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
        public Skeleton(Cell cell, DisplayObject parent)
            : base(cell, parent, Tiles.SkeletonTile)
        {
            Health = 30;
        }

        /// <summary>
        /// Action taken on key pressed (Player).
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
                this.Health -= 5;
                return this.Health <= 0;
            }

            return true;
        }
    }
}