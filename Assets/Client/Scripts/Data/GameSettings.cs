using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Settings", fileName = "New GameSettings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] private GameTileFactory tilesFactory;
		[SerializeField] private Vector2Int mapSize;
		[SerializeField] private float tileRadiusX = 0.5f;
		[SerializeField] private float tileRadiusY = 0.5f;
		[SerializeField] private bool useAsInnerCircleTileRadius = true;
		[SerializeField] private float autoSaveInterval = 5.0f;


		public GameTileFactory TilesFactory => tilesFactory;
		
		public Vector2Int MapSize => mapSize;

		public float TileRadiusX => tileRadiusX;
		public float TileRadiusY => tileRadiusY;

		public bool UseAsInnerCircleTileRadius => useAsInnerCircleTileRadius;

		public float AutoSaveInterval => autoSaveInterval;
	}
}