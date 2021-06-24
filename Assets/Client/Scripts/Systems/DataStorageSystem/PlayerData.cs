using System;
using System.Collections.Generic;
using Leopotam.Ecs.Types;

namespace Client.Scripts.Systems.DataStorageSystem
{
	public class PlayerData : IData
	{
		public int currency;
		public List<OpenTileData> openTiles;

		public PlayerData()
		{
			openTiles = new List<OpenTileData>();
		}
	}

	public struct OpenTileData
	{
		public Int2 position;
		public TileContentType type;
		public int reward;
	}
}
