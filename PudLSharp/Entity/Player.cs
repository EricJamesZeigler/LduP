using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using PudLSharp.Desktop.Entity;
using PudLSharp.Desktop.Map;

namespace PudLSharp.Desktop.Entity
{
	public class Player : Character
    {
		private bool moving = false;
		private bool spaceHeld = false;

		public Player(Map.Map map, Vector2 position, float width, float height) : base(map, position, width, height)
        {
        }

		public override void update(int dt, GameTime gameTime)
		{
			KeyboardState kstate = Keyboard.GetState();

			if (kstate.IsKeyUp(Keys.Left) && kstate.IsKeyUp(Keys.Right)
			    || kstate.IsKeyDown(Keys.Left) && kstate.IsKeyDown(Keys.Right)) { this.setMoving(false); }
			else if (kstate.IsKeyDown(Keys.Left)) { this.moveLeft(); this.setMoving(true); }
			else if (kstate.IsKeyDown(Keys.Right)) { this.moveRight(); this.setMoving(true); }

			if (kstate.IsKeyDown(Keys.Space)) { if (!spaceHeld) { spaceHeld = true; jump(); } }
			else if (kstate.IsKeyUp(Keys.Space)) { if (spaceHeld) { spaceHeld = false; } }

			//if(kstate.)

			base.update(dt, gameTime);
		}

		public override void TileCollide(Tile tile, int dt, bool checkEdges = true)
		{
			// if down is held and tile is passthrough, ignore collisions
			if (Game1.DOWN == true && tile.getProperties().isPassthrough()) { return; }
			base.TileCollide(tile, dt, checkEdges);
		}

		public void moveRight()
		{
			this.setVelX(this.getSpeed());
		}

		public void moveLeft()
		{
			this.setVelX(-this.getSpeed());
		}

		public void jump()
		{
			if (this.isOnGround())
			{
				this.addVel(new Vector2(0, -this.getJumpVel()));
				this.setOnGround(false);
			}
		}

		public override bool applyFriction()
		{
			return !this.getMoving();
		}

		public float getSpeed() => 3F;
		public float getJumpVel() => 8.5F;

		public void setMoving(bool moving) { this.moving = moving; }
		public bool getMoving() => this.moving;
	}
}
