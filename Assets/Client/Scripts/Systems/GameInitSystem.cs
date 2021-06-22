using Client.Scripts.Components;
using Client.Scripts.Data;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class GameInitSystem : IEcsInitSystem
	{
		private GameSettings gameSettings;
		private EcsWorld world;
		
		public void Init()
		{
			var grid = GameObject.Instantiate(gameSettings.GameGridPrefab);
			grid.Generate(world);

			foreach (var t in grid.Tiles.Values)
			{
				t.Cost = t.Data.Cost * (int) (Vector3.Distance(t.transform.position, Vector3.zero) * 2);
				t.Reward = new Reward(
					t.Data.Reward.Amount * (int)(Vector3.Distance(t.transform.position, Vector3.zero) * 2),
					t.Data.Reward.Type,
					t.Data.Reward.Interval
				);
			}

			InitBaseTile();
		}

		private void InitBaseTile()
		{
			ref var tileComponent = ref world.NewEntity().Get<TileDataComponent>();
			tileComponent.tile = PlayerBaseTile.Tile;
			tileComponent.createTimestamp = Time.time;
			tileComponent.rewardTimeLeft = PlayerBaseTile.Tile.Data.Reward.Interval;
				
			tileComponent.tile.Open();
		}
	}
}