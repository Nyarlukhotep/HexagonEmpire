using Client.Scripts.Data;

namespace Client.Scripts.UnityComponents
{
	public class PlayerBaseTile : GameTile
	{
		public static GameTile Tile;
		
		public override void Init(GameTileFactory factory, TileData data)
		{
			base.Init(factory, data);
			Tile = this;
		}
		
		public override void Open()
		{
			State = TileState.Open;
			Neighbors.ForEach(x => x.ShowAndUpdateCost());
		}

		public override void Hide()
		{
		}

		public override void UpdateReward()
		{
		}
	}
}