using Client.Scripts.Systems.DataStorageSystem;

namespace Client.Scripts.Systems
{
	public sealed class PlayerDataStorage : DataStorage<PlayerData>
	{
		public PlayerDataStorage(ISaveLoadDataProvider saveLoadDataProvider) : base(saveLoadDataProvider)
		{
			Data = saveLoadDataProvider.Load<PlayerData>() ?? new PlayerData();
		}
	}
}