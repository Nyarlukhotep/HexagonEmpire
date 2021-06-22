using System;
using UnityEngine;

namespace Client.Scripts
{
	[Serializable]
	public class Reward
	{
		[SerializeField] private int amount;
		[SerializeField] private RewardType type;
		[SerializeField] private float interval;

		public int Amount => amount;
		
		public RewardType Type => type;
		
		public float Interval => interval;

		public Reward(int rewardAmount, RewardType rewardType, float rewardInterval)
		{
			amount = rewardAmount;
			type = rewardType;
			interval = rewardInterval;
		}
	}
}