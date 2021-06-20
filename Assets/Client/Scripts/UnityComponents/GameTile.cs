using Client.Scripts.Data;
using TMPro;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class GameTile : MonoBehaviour
	{
		[SerializeField] private TileContentType type;
		[SerializeField] private TextMeshPro costText;
		
		public TileContentType Type => type;

		public TileState State { get; private set; } = TileState.Closed;
		
		public GameTileFactory Factory { get; private set; }

		public TileData Data { get; private set; }
		
		public void Init(GameTileFactory factory, TileData data)
		{
			Factory = factory;
			Data = data;
			
			costText.SetText(data.Reward.Amount.ToString()); //должно зависить от удаленности от начальной точки (базы)
		}

		public void Open()
		{
			State = TileState.Open;
		}
	}
}