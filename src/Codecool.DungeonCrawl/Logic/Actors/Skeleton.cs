using System;
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
        public Skeleton(Cell cell)
            : base(cell)
        {
            AttackStrength = 5;
            Defence = 3;
            Health = 40;
        }

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
                this.Health -= other.AttackStrength / this.Defence;
                return this.Health <= 0;
            }

            return true;
        }
    }
}