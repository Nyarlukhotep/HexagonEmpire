using Client.Scripts.Components;
using Client.Scripts.UnityComponents;
using Lean.Pool;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class RewardHandleSystem : IEcsRunSystem
	{
		private CommonSceneData sceneData;
		
		private EcsWorld world;
		private EcsFilter<TileDataComponent> tileDataFilter;
		
		public void Run()
		{
			foreach (var idx in tileDataFilter)
			{
				ref var tileComponent = ref tileDataFilter.Get1(idx);
				if (tileComponent.rewardTimeLeft > 0)
				{
					tileComponent.rewardTimeLeft -= Time.deltaTime;
					continue;
				}

				tileComponent.rewardTimeLeft = tileComponent.tile.Data.Reward.RewardInterval;

				var popupText = LeanPool.Spawn(sceneData.PopupTextPrefab);
				popupText.Init(tileComponent.tile.transform.position, $"+{tileComponent.tile.Data.Reward.Amount}");
			}
		}
	}
}