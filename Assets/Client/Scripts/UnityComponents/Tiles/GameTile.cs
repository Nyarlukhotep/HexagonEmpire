using System;
using System.Collections.Generic;
using System.Linq;
using Client.Scripts.Components;
using Client.Scripts.Data;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Client.Scripts.UnityComponents
{
	public abstract class GameTile : MonoBehaviour, IPointerClickHandler, IEquatable<GameTile>
	{
		[SerializeField] protected GameObject innerTileObject;
		[SerializeField] protected TextMeshPro costText;
		[SerializeField] protected TextMeshPro rewardText;
		
		[SerializeField, Range (1f, 2048f)] protected float dragThreshold = 15f;

		public TileState State { get; protected set; } = TileState.Closed;
		
		public GameTileFactory Factory { get; protected set; }

		public TileData Data { get; private set; }
		
		public int Cost { get; set; }
		
		public Reward Reward { get; set; }

		public EcsWorld World { get; set; }
		
		public Vector2Int Position { get; set; }

		public bool IsVisible { get; protected set; }

		public List<GameTile> Neighbors { get; } = new List<GameTile>();

		public virtual void Init(GameTileFactory factory, TileData data)
		{
			Factory = factory;
			Data = data;
			
			Close();
		}

		public virtual void Close()
		{
			UpdateCost();
			costText.enabled = false;
			innerTileObject.SetActive(false);
		}

		public virtual void Open()
		{
			if (State == TileState.Open)
				return;
			
			State = TileState.Open;
			innerTileObject.SetActive(true);

			costText.enabled = false;
			UpdateReward();
		}

		public virtual void OnPointerClick(PointerEventData eventData)
		{
			if ((eventData.pressPosition - eventData.position).sqrMagnitude > dragThreshold * dragThreshold)
				return;

			TileClick();
		}

		public virtual void TileClick()
		{
			if (Neighbors.All(x => x.State != TileState.Open))
				return;
			
			ref var tileComponent = ref World.NewEntity().Get<TileClickEventComponent>();
			tileComponent.tile = this;
			tileComponent.data = Data;
		}

		public virtual void UpdateCost()
		{
			if (State == TileState.Open)
				return;
			// var cost = (Data.Cost * Vector3.Distance(transform.position, Vector3.zero));
			costText.enabled = true;
			costText.SetText(Data.Cost.ToString()); //должно зависить от удаленности от начальной точки (базы)
		}

		public virtual void UpdateReward()
		{
			var reward = Reward.Amount;//(int)(Data.Reward.Amount * Vector3.Distance(transform.position, Vector3.zero) / 10);
			rewardText.SetText(reward.ToString()); //должно зависить от удаленности от начальной точки (базы)
		}

		public bool Equals(GameTile other)
		{
			return other != null && other.Position.x == Position.x && other.Position.y == Position.y;
		}

		private void OnBecameVisible()
		{
			IsVisible = true;
			innerTileObject.SetActive(true);
		}

		private void OnBecameInvisible()
		{
			IsVisible = false;
			innerTileObject.SetActive(false);
		}
	}
}