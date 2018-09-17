using System;
using Microsoft.Xna.Framework;
using PudLSharp.Desktop.Entity;

namespace PudLSharp.Desktop.Event.Entity
{
	public class EntityMoveEvent : EntityEvent
    {
		public Vector2 oldPos;
		public Vector2 newPos;

		public EntityMoveEvent(GameTime gameTime, PudLSharp.Desktop.Entity.Entity entity, Vector2 oldPos, Vector2 newPos) : base(gameTime, entity)
        {
			this.oldPos = oldPos;
			this.newPos = newPos;
        }
    }
}
