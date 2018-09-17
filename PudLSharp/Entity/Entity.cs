using System;
using Microsoft.Xna.Framework;
using PudLSharp.Desktop.Entity;
using PudLSharp.Desktop.Map;
using PudLSharp.Desktop.Event.Entity;
using System.Collections.Generic;

namespace PudLSharp.Desktop.Entity
{
	public class Entity
	{
		public virtual void onMove(EntityMoveEvent e) {  }
		public virtual bool hasCollision() { return false; }
		public virtual bool applyGravity() { return false; }
		public virtual bool applyFriction() { return false; }
		public virtual bool isOnGround() => this.onGround;
		public virtual Map.Map getMap() { return this.map; }
		public virtual Vector2 getGravity() => new Vector2(0, Game1.GRAVITATIONAL_ACCELLORATION);

		public Vector2 position;
		public Vector2 velocity;
		public float width;
		public float height;

		private Map.Map map;
		private bool onGround = false;

		public Entity(Map.Map map, Vector2 position, float width, float height)
		{
			this.map = map;
			this.position = position;
			this.width = width;
			this.height = height;
		}

		public virtual void update(int dt, GameTime gameTime)
		{
			if (applyGravity())
			{
				addVel(getGravity());
			}

			if (hasCollision())
			{
				collision(dt, gameTime);
			}

			// integrate position
			addPos(Vector2.Multiply(velocity, dt), gameTime);
		}

		/* Master Collision */
		/* Using Speculative Contacts */
		public virtual void collision(int dt, GameTime gameTime)
		{
			// determine the next position
			Vector2 nextPos = Vector2.Add(position, Vector2.Multiply(velocity, dt));

			// make sure each position vector is centered about the ORIGIN of the entity
			nextPos = new Vector2(nextPos.X + this.width / 2, nextPos.Y + this.height / 2);
			Vector2 currPos = new Vector2(position.X + this.width / 2, position.Y + this.height / 2);

			// get bounds for change in position
			Vector2 topLeft = Vector2.Min(nextPos, currPos);
			Vector2 bottomRight = Vector2.Max(nextPos, currPos);
            
			// expand, so that the bounds are accurate
			topLeft = Vector2.Subtract(topLeft, half_extents());
			bottomRight = Vector2.Add(bottomRight, half_extents());

			// convert vector data to bound
			AABB bound = new AABB(topLeft.X, topLeft.Y, bottomRight.X, bottomRight.Y);

			//Pre Collision Code

			// Register tile collisions with those in-bound
			TilesCollide(map.getTilesInBound(bound), dt);


			//Post Collision Code
		}

		// called when collision needs to effect velocity
		public virtual void CollideEffect(NormVector vec, int dt, float friction = -1)
		{         
			Vector2 norm = vec.vec;
			float dist = vec.distance;

			// dot product here gives length of projection of velocity onto normal vector
			// compute the distance between velocity vector and the wall (needed velocity)
			// nv = normal vector with magnitude normalVelocity
			float normalVelocity = (dist / dt) - Math.Abs(Vector2.Dot(velocity, norm));
			Vector2 nv = Vector2.Multiply(norm, Math.Abs(normalVelocity));

			// if the velocity vector in the normal direction is longer
			// then the distance between the entity and the tile
			// (otherwise, no collision will occur)
			if (normalVelocity < 0)
			{
				// add velocity in the normal direction
				addVel(nv);

                // if the normal points directly up
				if (norm.Y < 0 && norm.X == 0)
				{
					// this is ground

                    // set the entity as being on the ground
					if (!this.isOnGround()) { this.setOnGround(true); }
                    
					if (this.applyFriction())
					{
						if (friction >= 0)
						{
							// get unit tangent vector to the normal vector
							Vector2 unitTangent = new Vector2(norm.Y, -norm.X);

							// get friction vector by scaling by friction constant
							float tv = Vector2.Dot(velocity, unitTangent) * friction;
							Vector2 f = Vector2.Multiply(unitTangent, -tv);

							// apply friction to velocity
							this.addVel(f);
						}
						else // negative friction doesn't make much sense
						{
							velocity.X = 0;
						}
					}
				}
			}
		}

		// registers collision with a set of tiles
		public virtual void TileCollide(Tile tile, int dt, bool checkEdges = true)
		{
			// if the tile is not marked as an obstacle, it shouldn't have collisions
            if (!tile.getProperties().isObstacle()) return;

			NormVector vec = Collision.dist(this.getBounds(), tile.getTileAABB());

			float distance = vec.distance;
			Vector2 norm = vec.vec;

			// don't register the tile if the tile is next
			// to an obstacle in the direction of the tile
			if (checkEdges)
			{
				Tile tileAtNearPos = tile.tileInDir(norm, this.map, map.tilewidth);
				if (tileAtNearPos != null && tileAtNearPos.getProperties().isObstacle()) { return; }
			}

			if (tile.getProperties().isPassthrough()) // if the tile is passthrough
			{
				// if the normal doesn't point up, don't register collision
				if (!(norm.Y < 0 && norm.X == 0)) { return; }
			}

			CollideEffect(vec, dt, tile.getProperties().getFriction());
		}

		public virtual void TilesCollide(List<Tile> tiles, int dt)
		{
			// speculative contact collision only requires the normal
            // vector and the distance to the object; therefore,
            // only one instance of each vector is needed
			List<Vector2> tileVecs = new List<Vector2>();
            foreach (Tile t in tiles)
            {
                NormVector v = Collision.dist(this.getBounds(), t.getTileAABB());
                
				// don't register the tile if the tile is next
                // to an obstacle in the direction of the tile
				Tile tileAtNearPos = t.tileInDir(v.vec, this.map, map.tilewidth);
                if (tileAtNearPos != null && tileAtNearPos.getProperties().isObstacle()) { continue; }

                if (!tileVecs.Contains(v.vec)) 
				{
					tileVecs.Add(v.vec);
					TileCollide(t, dt, false);
				}
            }
		}
        
        // helper functions which have to do with bounds 
		public AABB getBounds() => new AABB(position.X, position.Y, position.X + width, position.Y + height);
		public Vector2 half_extents() => new Vector2(this.width / 2, this.height / 2); 
        
		// helper functions to set position
        // passes EntityMoveEvent
		public void setPos(Vector2 pos, GameTime gameTime = null) 
		{
			onMove(new EntityMoveEvent(gameTime, this, position, pos));
			position = pos; 
		}
		public void addPos(Vector2 ds, GameTime gameTime = null) { setPos(Vector2.Add(position, ds), gameTime); }

        // helper functions to set velocity
		public void setVel(Vector2 vel) { this.velocity = vel; }
		public void addVel(Vector2 dv) { this.setVel( Vector2.Add(velocity, dv) ) ; }
		public void subVel(Vector2 dv) { this.setVel( Vector2.Subtract(velocity, dv) ); }
        public void setVelX(float x) { this.setVel( new Vector2(x, this.velocity.Y) ); }
        public void setVelY(float y) { this.setVel( new Vector2(this.velocity.X, y) ); }
        
        // for determining if the entity is on the ground
		public void setOnGround(bool ground) { this.onGround = ground; }
	}


}
