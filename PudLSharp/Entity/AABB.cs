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

		public float[] origin() => new float[] { (x1+x2)/2, (y1+y2)/2 };
		public float width() => Math.Abs(x2 - x1);
		public float height() => Math.Abs(y2 - y1);
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

		public bool overlaps(AABB bound)
		{
			if (this.x2 <= bound.x1) return false;
			if (this.x1 >= bound.x2) return false;
			if (this.y2 <= bound.y1) return false;
			if (this.y1 >= bound.y2) return false;
			return true;
		}
    }
}
