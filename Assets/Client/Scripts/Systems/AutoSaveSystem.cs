using Client.Scripts.Components;
using Client.Scripts.Data;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class AutoSaveSystem : IEcsInitSystem, IEcsRunSystem
	{
		private GameSettings gameSettings;
		
		private EcsWorld world;
		
		private float timeLeft;

		public void Init()
		{
			timeLeft = gameSettings.AutoSaveInterval;
		}
		
		public void Run()
		{
			if (timeLeft > 0)
			{
				timeLeft -= Time.deltaTime;
				return;
			}

			timeLeft = gameSettings.AutoSaveInterval;
			world.NewEntity().Get<SaveDataComponent>();
		}
	}
}