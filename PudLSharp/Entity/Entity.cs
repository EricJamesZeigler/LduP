using System;
using Microsoft.Xna.Framework;

namespace PudLSharp.Desktop.Entity
{
    public class Entity
    {
		public virtual bool hasCollision();
        public virtual bool applyGravity();
        public virtual bool applyFriction();

		public virtual Vector2 position;
		public virtual Vector2 velocity;
		public virtual float width;
		public virtual float height;

		private Map.Map map;

		//public virtual float g = 9.8;

        public Entity(Map.Map map)
        {
			this.map = map;
        }

		public virtual void update(int dt)
		{
			if (hasCollision())
			{
				collision(dt);
			}

			if (applyGravity())
			{
				velocity.Y += dvel(dt).Y;
			}

			// integrate position
			position = Vector2.Add(velocity, position);
		}

        /* Master Collision Detection */
        /* Using Speculative Contacts */
		public virtual void collision(int dt)
		{
			// determine the next position
			Vector2 nextPos = new Vector2(position.X + dvel(dt).X, position.Y + dvel(dt).Y);

            // get bounds for change in position
			Vector2 topLeft = Vector2.Min(nextPos, position);
			Vector2 bottomRight = Vector2.Max(nextPos, position);

            // expand, so that the bounds are accurate
			Vector2.Subtract(topLeft, half_extents());
			Vector2.Add(bottomRight, half_extents());

            //Pre Collision Code

            

            //Post Collision Code
		}

		//protected virtual void InnerCollide
        
		public AABB getBounds() => new AABB(position.X, position.Y, position.X + width, position.Y + height);

        //memes are people too
		public virtual Vector2 dvel() => new Vector2(0, Game1.GRAVITATIONAL_ACCELLORATION);
        private Vector2 half_extents() => new Vector2(this.width/2, this.height/2);
    }
}
