using System;
using Microsoft.Xna.Framework;
using PudLSharp.Desktop.Entity;

namespace PudLSharp.Desktop.Event.Entity
{
	public class EntityEvent : Event
    {
		public PudLSharp.Desktop.Entity.Entity entity;

		public EntityEvent(GameTime gameTime, PudLSharp.Desktop.Entity.Entity entity) : base(gameTime)
        {
			this.entity = entity;
        }
    }
}
