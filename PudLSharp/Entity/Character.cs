using System;
using Microsoft.Xna.Framework;
using PudLSharp.Desktop.Entity;
using PudLSharp.Desktop.Map;

namespace PudLSharp.Desktop.Entity
{
	public class Character : Entity
    {
		public Character(Map.Map map, Vector2 position, float width, float height) : base(map, position, width, height)
        {
			
        }

		public override bool hasCollision() => true;
		public override bool applyGravity() => true;
		public override bool applyFriction() => true;

	}
}
