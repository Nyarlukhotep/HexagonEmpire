using System.Collections;
using Client.Scripts.Data;
using UnityEngine;

namespace Client.Scripts.UnityComponents
{
	public class Grid : MonoBehaviour
	{
		[SerializeField] private GameSettings gameSettings;
		[SerializeField] private GameObject emptyTilePrefab;

		public float radius = 0.5f;
		public bool useAsInnerCircleRadius = true;

		private float offsetX, offsetY;


		private void Start()
		{
			StartCoroutine(nameof(GenerateMap));
		}

		private IEnumerator GenerateMap()
		{
			var unitLength = (useAsInnerCircleRadius) ? (radius / (Mathf.Sqrt(3) / 2)) : radius;

			offsetX = unitLength * Mathf.Sqrt(3);
			offsetY = unitLength * 1.5f;

			for (var i = 0; i < gameSettings.MapSize.x; i++)
			{
				for (var j = 0; j < gameSettings.MapSize.y; j++)
				{
					Vector2 hexpos = HexOffset(i, j);
					Vector3 pos = new Vector3(hexpos.x, hexpos.y, 0);
					Instantiate(emptyTilePrefab, pos, Quaternion.identity, transform);

					yield return null;
				}
			}
			
			transform.position -= new Vector3(gameSettings.MapSize.x * radius, gameSettings.MapSize.y * radius, 0.0f);
		}

		private Vector2 HexOffset(int x, int y)
		{
			Vector2 position = Vector2.zero;

			if (y % 2 == 0)
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
	}
}