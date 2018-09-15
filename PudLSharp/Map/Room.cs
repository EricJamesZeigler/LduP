using System;
using TiledSharp;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PudLSharp.Desktop.Map
{
	public class Room
	{
		public int x, y;
		public string name;
		public int width;
		public int height;
		public int[] spawnpos;

		public List<Tile> tiles;
		public Dictionary<int[], Tile> tileAt = new Dictionary<int[], Tile>();

		public Room(string name, int x, int y, int width, int height)
		{
			this.x = x; this.y = y;
			this.name = name;
			this.width = width;
			this.height = height;

			spawnpos = new int[] { 0, 0 };
			tiles = new List<Tile>();
		}

		public void addTile(Tile t) { tiles.Add(t); }
		public Tile getTileAt(int x, int y) => tiles.Find(tile => (tile.x == x && tile.y == y)); //TODO: make this safe
		public List<Tile> getTiles() => this.tiles;

		public int[] getSpawn() => spawnpos;
		public int spawnX() => spawnpos[0];
		public int spawnY() => spawnpos[1];
		public void setSpawn(int x, int y) { spawnpos[0] = x; spawnpos[1] = y; }

		public void registerTiles(int tilesize)
		{
			tileAt.Clear();
			foreach (Tile t in tiles)
			{
				tileAt.Add(t.getTileCoords(tilesize), t);
			}

		}
	}
}
