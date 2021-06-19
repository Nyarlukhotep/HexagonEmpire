using System;
using Client.Scripts.Components;
using Client.Scripts.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class GameTileCreateHandleSystem : IEcsRunSystem
	{
		private GameSettings gameSettings;
		
		private EcsWorld world;
		private EcsFilter<InputComponent> inputFilter;
		
		public void Run()
		{
			foreach (var idx in inputFilter)
			{
				ref var input = ref inputFilter.Get1(idx);
				if (input.phase != InputPhase.UP)
					continue;
				
				var tilesCount = Enum.GetValues(typeof(TileContentType)).Length;
				var tileIndex = UnityEngine.Random.Range(0, tilesCount);
				var tile = gameSettings.TilesFactory.Get((TileContentType) tileIndex);
				tile.transform.localPosition = input.point;
				
				ref var tileComponent = ref world.NewEntity().Get<TileDataComponent>();
				tileComponent.tile = tile;
				tileComponent.createTimestamp = Time.time;
				tileComponent.rewardTimeLeft = tile.Data.Reward.RewardInterval;
			}
		}
	}
}