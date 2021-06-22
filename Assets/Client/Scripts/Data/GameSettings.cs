using Client.Scripts.UnityComponents;
using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Settings", fileName = "New GameSettings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] private GameTileFactory tilesFactory;
		[SerializeField] private Vector2Int mapSize;
		[SerializeField] private float tileRadius = 0.5f;
		[SerializeField] private bool useAsInnerCircleTileRadius = true;


		public GameTileFactory TilesFactory => tilesFactory;
		
		public Vector2Int MapSize => mapSize;

		public float TileRadius => tileRadius;

		public bool UseAsInnerCircleTileRadius => useAsInnerCircleTileRadius;
	}
}