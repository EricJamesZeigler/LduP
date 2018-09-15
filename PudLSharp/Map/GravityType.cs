using System;
namespace PudLSharp.Desktop.Map
{
	//Types of boundaries and their interactions with physics
	// WALL: Can't be passed through (e.g. walls)
	// PASSTHROUGH: Can be passed through (e.g. decorations)
	// KEYPASSTHROUGH: Boundaries which become passthrough when the player holds down
    public enum GravityType
    {
		WALL, PASSTHROUGH, KEYPASSTHROUGH
    }
}
