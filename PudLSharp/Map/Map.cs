using System;
using System.Collections.Generic;

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
			this.tilewidth = tilewidth; this.tileheight = tileheight
        }

		public void addRoom(Room r) { rooms.Add(r); }

		public List<Room> getRooms() => rooms;

		public void addSpawnRoom(Room s) { rooms.Insert(0, s); }

		public Room getSpawnRoom() => rooms[0];
    }
}
