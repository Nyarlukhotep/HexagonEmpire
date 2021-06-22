using Client.Scripts.Systems.DataStorageSystem;

namespace Client.Scripts.Systems
{
	public class PlayerDataStorage : DataStorage<PlayerData>
	{
		public PlayerDataStorage()
		{
			Data = Load<PlayerData>(new FileSaveLoadProvider()) ?? new PlayerData();
		}
	}
}