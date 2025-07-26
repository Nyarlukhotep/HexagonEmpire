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
		[SerializeField] protected GameObject tileOutlineObject;
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

		public List<GameTile> Neighbors { get; } = new List<GameTile>();

		public virtual void Init(GameTileFactory factory, TileData data)
		{
			Factory = factory;
			Data = data;
			
			Hide();
		}

		public virtual void Hide()
		{
			costText.enabled = false;
			innerTileObject.SetActive(false);
		}

		public virtual void Open()
		{
			if (State == TileState.Open)
				return;
			
			State = TileState.Open;
			
			costText.enabled = false;
			innerTileObject.SetActive(true);
			tileOutlineObject.SetActive(false);

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
		}

		public virtual void ShowAndUpdateCost()
		{
			if (State == TileState.Open)
				return;
			
			costText.enabled = true;
			costText.SetText(Cost.ToString());
		}

		public virtual void UpdateReward()
		{
			rewardText.SetText(Reward.Amount.ToString());
		}

		public bool Equals(GameTile other)
		{
			return other != null && other.Position.x == Position.x && other.Position.y == Position.y;
		}
	}
}