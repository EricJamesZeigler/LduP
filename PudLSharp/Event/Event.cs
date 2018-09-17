using System;
using Microsoft.Xna.Framework;

namespace PudLSharp.Desktop.Event
{
    public class Event
    {
		public GameTime gameTime;

        public Event(GameTime gameTime)
        {
			this.gameTime = gameTime;
        }
    }
}
