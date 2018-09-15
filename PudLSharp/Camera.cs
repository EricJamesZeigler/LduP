using System;
namespace PudLSharp.Desktop
{
    public class Camera
    {
		public int width, height;
		public int x, y;

		public Camera(int width, int height, int x = 0, int y = 0)
        {
			this.width = width; this.height = height;
			this.x = x; this.y = y;
        }

		public void setLoc(int x, int y) { this.x = x; this.y = y; }
		public void setX(int x) { this.x = x; }
		public void setY(int y) { this.y = y; }

        // Get location relative to camera
		public int camX(int x) => (x - this.x);
		public int camY(int y) => (y - this.y);
    }
}
