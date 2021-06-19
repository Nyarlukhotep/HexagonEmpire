using System;
using UnityEngine;

namespace Client.Scripts
{
	[Serializable]
	public class Reward
	{
		[SerializeField] private int amount;
		[SerializeField] private RewardType type;
		[SerializeField] private float rewardInterval;

		public int Amount => amount;
		
		public RewardType Type => type;
		
		public float RewardInterval => rewardInterval;
	}
}