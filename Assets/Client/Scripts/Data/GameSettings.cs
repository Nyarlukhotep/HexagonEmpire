using Client.Scripts.UnityComponents;
using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Settings", fileName = "New GameSettings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] private GameTileFactory tilesFactory;
		[SerializeField] private GameGrid gameGridPrefab;
		[SerializeField] private Vector2Int mapSize;


		public GameTileFactory TilesFactory => tilesFactory;
		
		public GameGrid GameGridPrefab => gameGridPrefab;

		public Vector2Int MapSize => mapSize;
	}
}