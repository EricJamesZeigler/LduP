using System;
namespace PudLSharp.Desktop.Entity
{
    public class AABB
    {
		public float x1, y1, x2, y2;
		public bool tileCoords;

        public AABB(float x1, float y1, float x2, float y2, Boolean tileCoords = false)
        {
			this.x1 = x1;
			this.y1 = y1;
			this.x2 = x2;
			this.y2 = y2;
			this.tileCoords = tileCoords;
        }

        // moves a bounding box to a position without changing its shape
		public void moveTo(float x1, float y1)
		{
			float dx1 = x1 - this.x1;
			float dy1 = y1 - this.y1;
			this.x1 = x1; this.y1 = y1;
			this.x2 += dx1; this.y2 += dy1;
		}

		public float width() => x2 - x1;
		public float height() => y2 - y1;
		public int widthi() => Convert.ToInt32(width());
		public int heighti() => Convert.ToInt32(height());
		public bool usingTileCoords() => this.tileCoords;

        // grow a distance from the origin
		public void grow(float h, float v)
		{
			//float[] origin = { (x1+x2)/2, (y1+y2)/2 };
			x1 -= h / 2; x2 += h / 2;
			y1 -= v / 2; y2 += v / 2;
		}
    }
}
