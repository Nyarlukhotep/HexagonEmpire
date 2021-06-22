using System;
using System.Collections.Generic;
using Leopotam.Ecs.Types;

namespace Client.Scripts.Systems.DataStorageSystem
{
	[Serializable]
	public class PlayerData : IData
	{
		public int currency;
		public List<OpenTileData> openTiles;

		public PlayerData()
		{
			openTiles = new List<OpenTileData>();
		}
	}

	[Serializable]
	public struct OpenTileData
	{
		public Int2 position;
		public TileContentType type;
		public int reward;
	}
}