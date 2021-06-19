using Client.Scripts.UnityComponents;
using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/New Tile", fileName = "New Tile")]
	public class TileData : ScriptableObject
	{
		[SerializeField] private TileContentType type;
		[SerializeField] private GameTile prefab;
		[SerializeField] private int cost;
		[SerializeField] private Reward reward;

		public TileContentType Type => type;

		public GameTile Prefab => prefab;

		public int Cost => cost;

		public Reward Reward => reward;
	}
}