using Client.Scripts.Components;
using Client.Scripts.Systems.DataStorageSystem;

namespace Client.Scripts.Systems
{
	public class PlayerDataStorage : DataStorage<PlayerDataComponent>
	{
		public PlayerDataStorage()
		{
			Data = Load<PlayerDataComponent>(new FileSaveLoadProvider());
		}
	}
}