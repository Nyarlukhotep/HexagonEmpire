using System;
using System.Collections.Generic;
using Client.Scripts.Components;
using Client.Scripts.Data;
using Client.Scripts.Systems.DataStorageSystem;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class GameInitSystem : IEcsInitSystem
	{
		private GameSettings gameSettings;
		private EcsWorld world;
		private EcsFilter<DataComponent<PlayerData>> playerDataFilter;
		
		private float offsetX;
		private float offsetY;
		private readonly float innerRadius = Mathf.Sqrt(3) / 2;
		private Dictionary<Vector2Int, GameTile> Tiles { get; } = new ();
		private Transform gridParentTransform;
		
		public void Init()
		{
			gridParentTransform = new GameObject("GridParentObject").transform;
			GenerateGrid();
			SetCostAndReward();
			InitBaseTile();
		}

		private void SetCostAndReward()
		{
			foreach (var t in Tiles.Values)
			{
				//Base sample setting the dependence of cost and reward
				t.Cost = t.Data.Cost * (int)(Vector3.Distance(t.transform.position, Vector3.zero) * 10);
				t.Reward = new Reward(
					t.Data.Reward.Amount * (int)Math.Max(1, Vector3.Distance(t.transform.position, Vector3.zero) / 4),
					t.Data.Reward.Type,
					t.Data.Reward.Interval
				);
			}
		}

		private void InitBaseTile()
		{
			ref var tileComponent = ref world.NewEntity().Get<TileCreateComponent>();
			tileComponent.tile = PlayerBaseTile.Tile;
			tileComponent.isExistingTile = true;
		}
		
		private void GenerateGrid()
		{
			var unitLength = gameSettings.UseAsInnerCircleTileRadius
				? gameSettings.TileRadius / innerRadius
				: gameSettings.TileRadius;

			offsetX = unitLength * Mathf.Sqrt(3);
			offsetY = unitLength * 1.5f;

			var halfWidth = gameSettings.MapSize.x / 2;
			halfWidth = (halfWidth & 1) == 0 ? halfWidth : halfWidth - 1;
			var halfHeight = gameSettings.MapSize.y / 2;
			halfHeight = (halfHeight & 1) == 0 ? halfHeight : halfHeight - 1;

			for (var x = -halfWidth; x < halfWidth + 1; x++)
			{
				for (var y = -halfHeight; y < halfHeight + 1; y++)
				{
					GameTile tile = null;
					var hexPosition = HexOffset(x, y);

					foreach (var idx in playerDataFilter)
					{
						ref var dataComponent = ref playerDataFilter.Get1(idx);

						dataComponent.Data.openTiles.ForEach(t =>
						{
							if (t.position.x != x || t.position.y != y) 
								return;
							
							tile = gameSettings.TilesFactory.Get(t.type);
							
							ref var tileComponent = ref world.NewEntity().Get<TileCreateComponent>();
							tileComponent.tile = tile;
							tileComponent.isExistingTile = true;
						});
						
						if (tile == null)
						{
							tile = x == 0 && y == 0
								? gameSettings.TilesFactory.Get(TileContentType.PlayerBase)
								: gameSettings.TilesFactory.GetRandom();
						}
						
						tile.transform.position = hexPosition;
						tile.transform.SetParent(gridParentTransform);
						tile.Position = new Vector2Int(x, y);
						tile.World = world;

						Tiles[tile.Position] = tile;
					}
				}
			}

			foreach (var tile in Tiles.Values)
			{
				SetNeighbors(tile);
			}
		}

		private Vector2 HexOffset(int x, int y)
		{
			var position = Vector2.zero;

			if ((y & 1) == 0)
			{
				position.x = x * offsetX;
				position.y = y * offsetY;
			}
			else
			{
				position.x = (x + 0.5f) * offsetX;
				position.y = y * offsetY;
			}

			return position;
		}

		private void SetNeighbors(GameTile tile)
		{
			var positionX = tile.Position.x;
			var positionY = tile.Position.y;
			var tileNeighbors = tile.Neighbors;

			//Left
			var neighborPosition = new Vector2Int(positionX - 1, positionY);
			TryAddNeighbor(tileNeighbors, neighborPosition);
			
			//Right
			neighborPosition.x = positionX + 1;
			neighborPosition.y = positionY;
			TryAddNeighbor(tileNeighbors, neighborPosition);

			if ((positionY & 1) == 0)
			{
				//Top Left
				neighborPosition.x = positionX - 1;
				neighborPosition.y = positionY + 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Top Right
				neighborPosition.x = positionX;
				neighborPosition.y = positionY + 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Bottom Left
				neighborPosition.x = positionX - 1;
				neighborPosition.y = positionY - 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Bottom Right
				neighborPosition.x = positionX;
				neighborPosition.y = positionY - 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);
			}
			else
			{
				//Top Left
				neighborPosition.x = positionX;
				neighborPosition.y = positionY + 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Top Right
				neighborPosition.x = positionX + 1;
				neighborPosition.y = positionY + 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Bottom Left
				neighborPosition.x = positionX;
				neighborPosition.y = positionY - 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);

				//Bottom Right
				neighborPosition.x = positionX + 1;
				neighborPosition.y = positionY - 1;
				TryAddNeighbor(tileNeighbors, neighborPosition);
			}
		}

		private void TryAddNeighbor(List<GameTile> tileNeighbors, Vector2Int neighborPosition)
		{
			if (Tiles.ContainsKey(neighborPosition))
				tileNeighbors.Add(Tiles[neighborPosition]);
		}
	}
}