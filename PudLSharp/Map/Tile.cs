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
		public int[] getTileCoords() => new int[] { Tile.worldCoordsToTile(x, properties.getWidth()), Tile.worldCoordsToTile(y, properties.getHeight()) };
		public AABB getTileAABB() => new AABB(getTileCoords()[0], getTileCoords()[1], getTileCoords()[0] + 1, getTileCoords()[1] + 1);

		public static int tileCoordsToWorld(int tilepos, int tilesize) => tilepos * tilesize;
		public static int worldCoordsToTile(int worldpos, int tilewidth) => (tilex % tilewidth == 0) ? worldpos / tilewidth : 0;
    }
}
