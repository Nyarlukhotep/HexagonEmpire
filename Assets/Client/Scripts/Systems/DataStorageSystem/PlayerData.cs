using System;
using System.Collections.Generic;

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
}
