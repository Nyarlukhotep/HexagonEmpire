using Client.Scripts.Components;
using Client.Scripts.Systems.DataStorageSystem;
using Leopotam.Ecs;

namespace Client.Scripts.Systems
{
	public class SaveLoadDataSystem : IEcsPreInitSystem, IEcsRunSystem, IEcsPostDestroySystem
	{
		private EcsWorld world;
		private EcsFilter<SaveDataComponent> saveDataFilter;

		private PlayerDataStorage playerDataStorage;
		
		public void PreInit()
		{
			playerDataStorage = new PlayerDataStorage();

			ref var entity = ref world.NewEntity().Get<DataComponent<PlayerDataComponent>>();
			entity.Data = playerDataStorage.Data;

			world.NewEntity().Get<UpdatePlayerDataComponent>();
		}

		public void Run()
		{
			if (!saveDataFilter.IsEmpty())
				Save();
		}

		public void PostDestroy()
		{
			Save();
		}

		private void Save()
		{
			playerDataStorage.Save(new FileSaveLoadProvider());
		}
	}
}