using System;
using Microsoft.Xna.Framework.Graphics;

namespace PudLSharp.Desktop.Map
{
    public class TileProps
    {
		private int width, height = 0;
		//private GravityType gtype = GravityType.WALL;
		private bool obstacle = true;
		private bool keyPassthrough = false;
        private int slideiness = 0;
		private Texture2D tex;

        public TileProps(){}
        
        // return instance of object to chain setting of properties
		// TODO: figure out how to do this in a way that isn't autistic
		public TileProps setWidth(int w) { this.width = w; return this; }
		public TileProps setHeight(int h) { this.height = h; return this; }
		public TileProps setSize(int w, int h) { this.width = w; this.height = h; return this; }
		public TileProps setSizeToTex() { this.width = tex.Width; this.height = tex.Height; return this; }
		public TileProps setObstacle(bool obstacle) { this.obstacle = obstacle; return this;  }
		public TileProps setKeyPassthrough(bool passthrough) { this.keyPassthrough = passthrough; return this; }
		public TileProps setSlideiness(int slideiness) { this.slideiness = slideiness; return this; }
		public TileProps setTexture(Texture2D texture) { this.tex = texture; return this; }

		public int getWidth() => this.width;
		public int getHeight() => this.height;
		public bool isObstacle() => this.obstacle;
		public bool isPassthrough() => this.keyPassthrough;
		public int getSlideiness() => this.slideiness;
		public Texture2D getTexture() => this.tex;
	}
}
