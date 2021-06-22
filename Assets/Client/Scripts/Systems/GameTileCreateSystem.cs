using Client.Scripts.Components;
using Client.Scripts.Data;
using Client.Scripts.Systems.DataStorageSystem;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class GameTileCreateSystem : IEcsRunSystem
	{
		private GameSettings gameSettings;
		
		private EcsWorld world;
		private EcsFilter<TileCreateComponent> tileCreateComponentFilter;
		private EcsFilter<DataComponent<PlayerData>> playerDataFilter;
		
		public void Run()
		{
			foreach (var idx in tileCreateComponentFilter)
			{
				ref var tileCreateComponent = ref tileCreateComponentFilter.Get1(idx);
				
				foreach (var idx2 in playerDataFilter)
				{
					ref var dataComponent = ref playerDataFilter.Get1(idx2);
					
					if (!tileCreateComponent.isExistingTile)
					{
						dataComponent.Data.openTiles.Add(new OpenTileData
						{
							position = tileCreateComponent.tile.Position,
							type = tileCreateComponent.tile.Data.Type,
							reward = tileCreateComponent.tile.Reward.Amount
						});
						
						world.NewEntity().Get<UpdatePlayerDataComponent>();
					}
					
					ref var tileComponent = ref world.NewEntity().Get<TileDataComponent>();
					tileComponent.tile = tileCreateComponent.tile;
					tileComponent.createTimestamp = Time.time;
					tileComponent.rewardTimeLeft = tileCreateComponent.tile.Data.Reward.Interval + UnityEngine.Random.Range(0.0f, 4.0f);
					
					tileComponent.tile.Open();
					
					tileComponent.tile.Neighbors.ForEach(x => x.ShowAndUpdateCost());
				}
			}
		}
	}
}