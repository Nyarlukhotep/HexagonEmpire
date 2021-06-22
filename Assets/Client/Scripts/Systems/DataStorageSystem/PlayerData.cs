using System;
using System.Collections.Generic;
using Leopotam.Ecs.Types;

namespace Client.Scripts.Systems.DataStorageSystem
{
	[Serializable]
	public class PlayerData : IData
	{
		public int currency;
		public List<OpenTilesData> openTiles;

		public PlayerData()
		{
			openTiles = new List<OpenTilesData>();
		}
	}

	[Serializable]
	public struct OpenTilesData
	{
		public Int2 position;
		public TileContentType type;
		public int reward;
	}
}