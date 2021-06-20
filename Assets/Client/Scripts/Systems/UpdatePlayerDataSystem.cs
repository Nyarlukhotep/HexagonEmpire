using Client.Scripts.Components;
using Client.Scripts.Systems.DataStorageSystem;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;

namespace Client.Scripts.Systems
{
	public class UpdatePlayerDataSystem : IEcsRunSystem
	{
		private CommonSceneData sceneData;
		
		private EcsWorld world;
		private EcsFilter<UpdatePlayerDataComponent> updateDataFilter;
		private EcsFilter<DataComponent<PlayerDataComponent>> playerDataFilter;
		
		public void Run()
		{
			if (updateDataFilter.IsEmpty())
				return;
			
			foreach (var idx in playerDataFilter)
			{
				ref var playerData = ref playerDataFilter.Get1(idx);
					
				sceneData.PlayerDataView.UpdateData(playerData.Data);
			}
		}
	}
}