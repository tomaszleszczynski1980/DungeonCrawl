namespace Codecool.DungeonCrawl.Logic.Actors
{
    /// <summary>
    /// Actor is a base class for every entity in the dungeon.
    /// </summary>
    public abstract class Actor : IDrawable
    {
        /// <summary>
        /// Gets the cell where this actor is located
        /// </summary>
        public Cell Cell { get; private set; }

        /// <summary>
        /// Gets or sets and sets this actors health
        /// </summary>
        public int Health { get; set; }

        /// <summary>
        /// Gets or sets and sets this actors attack strength
        /// </summary>
        public int AttackStrength { get; set; }

        /// <summary>
        /// Gets or sets and sets this actors defence
        /// </summary>
        public int Defence { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Actor"/> class.
        /// </summary>
        /// <param name="cell">The cell of this actor</param>
        public Actor(Cell cell)
        {
            Cell = cell;
            Cell.Actor = this;

            // Program.Collision += Collision();
        }

        /// <summary>
        /// Moves this actor by the given amount
        /// </summary>
        /// <param name="dx">X amoount</param>
        /// <param name="dy">Y amount</param>
        public void Move(int dx, int dy)
        {
            Cell nextCell = Cell.GetNeighbor(dx, dy);
            if (nextCell.Passable)
            {
                // if (nextCell.Actor != null && nextCell.Actor.OnCollision(this))
                if (nextCell.Actor?.OnCollision(this) ?? true)
                {
                    Cell.Actor = null;
                    nextCell.Actor = this;
                    Cell = nextCell;
                    OnEnter(Cell);
                }
            }
        }

        /// <summary>
        /// Actors Collision.
        /// </summary>
        public virtual bool OnCollision(Actor other)
        {
            return true;
        }

        /// <summary>
        /// On enter finish.
        /// </summary>
        public virtual void OnEnter(Cell cell)
        {
            return;
        }

        /// <summary>
        /// Gets the X position
        /// </summary>
        public int X => Cell.X;

        /// <summary>
        /// Gets the Y position
        /// </summary>
        public int Y => Cell.Y;

        /// <summary>
        /// Gets the name of this tile.
        /// </summary>
        public abstract string Tilename { get; }
    }
}