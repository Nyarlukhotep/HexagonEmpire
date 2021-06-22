using System.Linq;
using Client.Scripts.UnityComponents;
using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Tile Factory", fileName = "GameTileFactory")]
	public class GameTileFactory : ScriptableObject
	{
		[SerializeField] private TileData[] tiles;

		public GameTile GetRandom()
		{
			var randomTile = tiles[UnityEngine.Random.Range(1, tiles.Length)];
			return Get(randomTile.Type);
		}
		
		public GameTile Get(TileContentType type)
		{
			var data = tiles.FirstOrDefault(t => t.Type.Equals(type));
			if (data == null)
				return null;
			
			var instance = Instantiate(data.Prefab);
			instance.Init(this, data);
			
			return instance;
		}
	}
}