using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using PudLSharp.Desktop.Entity;

namespace PudLSharp.Desktop.Map
{

	public class Tile
	{
		// values are in GLOBAL COORDS
		public int x, y;
		public int id;
		private TileProps properties;

		public Tile(int id, int x, int y, TileProps props)
		{
			this.x = x; this.y = y;
			this.properties = props;
		}

		public TileProps getProperties() => this.properties;
		public int[] getTileCoords(int tilesize) => new int[] { Tile.worldCoordsToTile(x, tilesize), Tile.worldCoordsToTile(y, tilesize) };
		public Vector2 getTileCoordVec(int tilesize) => new Vector2(getTileCoords(tilesize)[0], getTileCoords(tilesize)[1]);
		public AABB getTileAABB() => new AABB(x, y, x + properties.getWidth(), y + properties.getHeight());
		public bool isInBound(AABB bound) => bound.overlaps(getTileAABB());

		public static int tileCoordsToWorld(int tilepos, int tilesize) => tilepos * tilesize;
		public static int worldCoordsToTile(int worldpos, int tilesize, bool roundup = false) => worldpos/tilesize + (roundup ? 1 : 0) * ((worldpos % tilesize));

		public Tile tileInDir(Vector2 dir, Map map, int tilesize)
		{
			// find next tile in the direction of the normal vector and discard it if it is an obstacle
			Vector2 nearPos = Vector2.Add(new Vector2(this.x, this.y), Vector2.Multiply(new Vector2(Math.Sign(dir.X), Math.Sign(dir.Y)), tilesize));
            return map.tileAt(Convert.ToInt32(nearPos.X), Convert.ToInt32(nearPos.Y));
		}
	}
}
