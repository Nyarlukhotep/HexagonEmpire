using System.Linq;
using Client.Scripts.UnityComponents;
using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Tile Factory", fileName = "GameTileFactory")]
	public class GameTileFactory : ScriptableObject
	{
		[SerializeField] private TileData[] tiles;

		/// <summary>
		/// Get random tile exclude Base Tile
		/// </summary>
		/// <returns>GameTile</returns>
		public GameTile GetRandom()
		{
			var tilesExcludeBase = tiles.Where(t => !t.IsBaseTile).ToArray();
			var randomTile = tilesExcludeBase[UnityEngine.Random.Range(0, tilesExcludeBase.Length)];
			return Get(randomTile.Type);
		}
		
		/// <summary>
		/// Get tile by Type
		/// </summary>
		/// <param name="type">Tile Type</param>
		/// <returns>GameTile</returns>
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