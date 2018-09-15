using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using TiledSharp;
using System;
using System.Collections.Generic;
using PudLSharp.Desktop.Map;
using System.Linq;

namespace PudLSharp.Desktop
{

    public class Game1 : Game
    {
		public const int WINDOWED_WIDTH  = 1024;
		public const int WINDOWED_HEIGHT = 768;
		public const float GRAVITATIONAL_ACCELLORATION = 9.8F;

        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;
              
		static Map.Map Cave;
		public static Dictionary<int, TileProps> tileProps = new Dictionary<int, TileProps>();

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
			graphics.PreferredBackBufferWidth = WINDOWED_WIDTH;
			graphics.PreferredBackBufferHeight = WINDOWED_HEIGHT;
			graphics.ApplyChanges();

            Content.RootDirectory = "Content";
        }
        
        protected override void Initialize()
        {   
            base.Initialize();
		}

		protected override void LoadContent()
		{
			// Create a new SpriteBatch, which can be used to draw textures.
			spriteBatch = new SpriteBatch(GraphicsDevice);

			/* Register tile objects and their sprites/properties */
			tileProps.Add(0, new TileProps().setTexture(null).setSize(0, 0).setObstacle(false));
			Texture2D texWall = Content.Load<Texture2D>("sprite/wall");
			tileProps.Add(1, new TileProps().setTexture(texWall).setSizeToTex().setObstacle(true));

			/* Register each map from XML */
			TmxMap CaveTMX = new TmxMap("Content/Maps/cave.tmx");
			Cave = mapFromTiled(CaveTMX, "Cave");

        }
        
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
			

            // TODO: Add your update logic here

            base.Update(gameTime);
        }
        
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
			GraphicsDevice.Clear(Color.Gray);
            
            base.Draw(gameTime);
        }

		private Map.Map mapFromTiled(TmxMap map, string name)
		{
			Map.Map m = new Map.Map(name, map.Width * map.TileWidth, map.Height * map.TileHeight, map.TileWidth, map.TileHeight);

			foreach (TmxLayer layer in map.Layers)
			{
				// take only the tiles which are not empty
				List<TmxLayerTile> tiles = layer.Tiles.Where(t => t.Gid != 0).ToList();

				// find the top left and bottom right tiles
				int topLeftTileX = tiles.Min(tile => tile.X);
				int topLeftTileY = tiles.Min(tile => tile.Y);
				int bottomRightTileX = tiles.Max(tile => tile.X);
				int bottomRightTileY = tiles.Max(tile => tile.Y);

                // use these tiles to determine the size of the room
				int roomWidth = map.TileWidth * (bottomRightTileX - topLeftTileX);
				int roomHeight = map.TileHeight * (bottomRightTileY - topLeftTileY);
                
                // use top left tile as origin point of room
				int roomx = map.TileWidth * topLeftTileX;
				int roomy = map.TileHeight * topLeftTileY;

				Room room = new Room(layer.Name, roomx, roomy, roomWidth, roomHeight);

                // add tiles
				foreach (TmxLayerTile tile in tiles)
				{
					// global tile id's are +1 from the sheet's id
					int id = tile.Gid - 1;
					if (id == 0) { room.setSpawn(tile.X * map.TileWidth, tile.Y * map.TileHeight); }

					Tile t = new Tile(id, tile.X * map.TileWidth, tile.Y * map.TileHeight, tileProps[id]);

					room.addTile(t);
				}

				//TODO: make this compatible with non-square tiles
				room.registerTiles(map.TileWidth);

                // if the layer's name is "spawn", set it as the spawn room
				if (layer.Name.ToLowerInvariant() == "spawn") m.addSpawnRoom(room); else m.addRoom(room);
			}
            
			return m;
		}
    }
}
