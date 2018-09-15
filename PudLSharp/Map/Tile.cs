using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using PudLSharp.Desktop.Entity;

namespace PudLSharp.Desktop.Map
{

	public class Tile
	{
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
		public AABB getTileAABB(int tilesize) => new AABB(getTileCoords(tilesize)[0], getTileCoords(tilesize)[1], getTileCoords(tilesize)[0] + 1, getTileCoords(tilesize)[1] + 1);

		public static int tileCoordsToWorld(int tilepos, int tilesize) => tilepos * tilesize;
		public static int worldCoordsToTile(int worldpos, int tilesize, bool roundup = false) => worldpos + (roundup ? 1 : -1) * (tilesize - (worldpos % tilesize));
    }
}
