using System.Collections.Generic;
using Client.Scripts.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class GameGrid : MonoBehaviour
	{
		[SerializeField] private GameSettings gameSettings;
		[SerializeField] private float radius = 0.5f;
		[SerializeField] private bool useAsInnerCircleRadius = true;

		private EcsWorld world;
		private float offsetX, offsetY;
		private readonly float innerRadius = Mathf.Sqrt(3) / 2;

		public Dictionary<Vector2Int, GameTile> Tiles { get; } = new Dictionary<Vector2Int, GameTile>();

		public void Generate(EcsWorld ecsWorld)
		{
			world = ecsWorld;
			GenerateGrid();
		}

		private void GenerateGrid()
		{
			var unitLength = useAsInnerCircleRadius
				? radius / innerRadius
				: radius;

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
					var hexPosition = HexOffset(x, y);
					var tile = (x == 0 && y == 0)
						? gameSettings.TilesFactory.Get(TileContentType.PlayerBase)
						: gameSettings.TilesFactory.GetRandom();

					tile.transform.position = hexPosition;
					tile.transform.SetParent(transform);
					tile.Position = new Vector2Int(x, y);
					tile.World = world;

					Tiles[tile.Position] = tile;
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