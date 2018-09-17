using System;

using Microsoft.Xna.Framework;
namespace PudLSharp.Desktop
{
    public class Camera
    {
		public float width, height;
		public float x, y;

		public Camera(float width, float height, float x = 0, float y = 0)
        {
			this.width = width; this.height = height;
			this.x = x; this.y = y;
        }

		public void setLoc(float x, float y) { this.x = x; this.y = y; }
		public void setX(float x) { this.x = x; }
		public void setY(float y) { this.y = y; }

        // Get location relative to camera
		public float camx(float x) => (x - this.x);
		public float camy(float y) => (y - this.y);
		public Vector2 camvec(Vector2 v) => new Vector2(camx(v.X), camy(v.Y));
    }
}
