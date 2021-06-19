using UnityEngine;

namespace Client.Scripts.Data
{
	[CreateAssetMenu(menuName = "Game/Game Settings", fileName = "New GameSettings")]
	public class GameSettings : ScriptableObject
	{
		[SerializeField] private GameTileFactory tilesFactory;


		public GameTileFactory TilesFactory => tilesFactory;
	}
}