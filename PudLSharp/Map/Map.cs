using System;
using System.Collections.Generic;
using PudLSharp.Desktop.Entity;
using System.Linq;
using Microsoft.Xna.Framework;

namespace PudLSharp.Desktop.Map
{
    public class Map
    {
		public List<Room> rooms;
		public int width;
		public int height;
		public string name;
		public int tilewidth, tileheight;

        public Map(string name, int width, int height, int tilewidth = 32, int tileheight = 32)
        {
			rooms = new List<Room>();
			this.name = name;
			this.width = width;
			this.height = height;
			this.tilewidth = tilewidth; this.tileheight = tileheight;
        }

		public void addRoom(Room r) { rooms.Add(r); }

		public List<Room> getRooms() => rooms;
		//public Room getRoomWithWorldPos(int x, int y) => rooms.Where(r => (x >= r.x) && (x <= r.x+r.width) && (y >= r.y) && (y <= r.y+r.height) );
		//public Room getRoomWithTilePos(int x int y, int tilewidth, int tileheight) => getRoomWithWorldPos(Tile.tileCoordsToWorld(x, tilewidth), Tile.tileCoordsToWorld(y, tileheight));

		public void addSpawnRoom(Room s) { rooms.Insert(0, s); }

		public Room getSpawnRoom() => rooms[0];

		public Tile tileAt(int x, int y)
		{
            Room room = rooms.Find(r => r.getTileAt(x, y) != null);

			return (room == null) ? null : room.getTileAt(x, y);
		}

        // takes world coordinates
		public List<Tile> getTilesInBound(AABB bound)
		{
			//determine upper-left bound, round down
			//int minX = Tile.worldCoordsToTile(bound.x1, this.tilewidth, false);
			//int minY = Tile.worldCoordsToTile(bound.y1, this.tilewidth, false);

            //determine bttom-right bound, round up
			//int maxX = Tile.worldCoordsToTile(bound.x2, this.tilewidth, true);
            //int maxY = Tile.worldCoordsToTile(bound.y2, this.tilewidth, true);

            // get all tiles from all rooms
			List<Tile> tiles = rooms.SelectMany(r => r.tiles).Distinct().ToList();
            // loop over tiles within the bounds
			return tiles.FindAll(tile => tile.isInBound(bound));
		}
    }
}
