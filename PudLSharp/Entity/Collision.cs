using System;
using Microsoft.Xna.Framework;

namespace PudLSharp.Desktop.Entity
{
    public class Collision
    {
        public Collision()
        {
        }

		// given two AABBs, it gives a vector representing distance in the normal direction of the obstacle
		public static NormVector dist(AABB a, AABB obstacle)
		{
			Vector2 dist = new Vector2(0, 0);
			float distance = 0;

			// get the origin points of each box
			Vector2 og_a = new Vector2(a.origin()[0], a.origin()[1]);
			Vector2 og_obs = new Vector2(obstacle.origin()[0], obstacle.origin()[1]);         

			// get vector between objects
			Vector2 d = Vector2.Subtract(og_a, og_obs);

			// grow obstacle bound to find distance
			AABB obstacleBound = obstacle;
			obstacleBound.grow((a.width()/2), (a.height()/2));

            // normal should be in the direction of the major axis
            //   this is because the major axis points in the 
            //   direction of whichever side a is on
			if (Math.Abs( d.X ) >= Math.Abs( d.Y ))
			{
				dist = new Vector2(Math.Sign(d.X), 0);
				distance = Math.Abs(Math.Abs(d.X) - a.width());
			}
			else
			{
				dist = new Vector2(0, Math.Sign(d.Y) );
				distance = Math.Abs((Math.Abs(d.Y) - a.height()));
			}

			return new NormVector(dist, distance);
		}
    }

	public class NormVector
    {
        public Vector2 vec;
        public float distance;

        public NormVector(Vector2 vec, float distance)
        {
            this.vec = vec;
            this.distance = distance;
        }
    }
}
