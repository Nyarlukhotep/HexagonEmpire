using System.Linq;
using Client.Scripts.Components;
using Client.Scripts.Data;
using Client.Scripts.Systems.DataStorageSystem;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class GameTileHandleSystem : IEcsRunSystem
	{
		private GameSettings gameSettings;
		
		private EcsWorld world;
		private EcsFilter<TileClickEventComponent> tileClickEventFilter;
		private EcsFilter<DataComponent<PlayerData>> playerDataFilter;
		
		public void Run()
		{
			foreach (var idx in tileClickEventFilter)
			{
				ref var tileClickComponent = ref tileClickEventFilter.Get1(idx);
				if (tileClickComponent.tile.State == TileState.Open)
					continue;
				
				foreach (var idx2 in playerDataFilter)
				{
					ref var dataComponent = ref playerDataFilter.Get1(idx2);
					if (dataComponent.Data.currency < tileClickComponent.data.Cost)
						continue;

					dataComponent.Data.currency -= tileClickComponent.data.Cost;
				
					ref var tileComponent = ref world.NewEntity().Get<TileDataComponent>();
					tileComponent.tile = tileClickComponent.tile;
					tileComponent.createTimestamp = Time.time;
					tileComponent.rewardTimeLeft = tileClickComponent.data.Reward.Interval;
				
					tileComponent.tile.Open();
					
					dataComponent.Data.openTiles.Add(new OpenTilesData
					{
						position = tileComponent.tile.Position,
						type = tileComponent.tile.Data.Type,
						reward = tileComponent.tile.Reward.Amount
					});
					
					world.NewEntity().Get<UpdatePlayerDataComponent>();
					
					tileComponent.tile.Neighbors.ForEach(x => x.UpdateCost());
				}
			}
		}
	}
}