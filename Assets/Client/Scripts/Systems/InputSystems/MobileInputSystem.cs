using System;
using Client.Scripts.Components;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class MobileInputSystem : IEcsRunSystem
	{
		private CommonSceneData sceneData;
		
		private EcsWorld world;
		private EcsFilter<InputComponent> inputFilter;
		
		public void Run()
		{
			if (Input.GetKey(KeyCode.Escape))
			{
				Application.Quit();
			}
			
			foreach (var idx in inputFilter)
			{
				ref var input = ref inputFilter.Get1(idx);
				
				if (input.phase != InputPhase.UP) 
					continue;
				
				ref var entity = ref inputFilter.GetEntity(idx);
				entity.Destroy();
			}
			
			for (var i = 0; i < Input.touchCount; i++)
			{
				var inputTouch = Input.touches[i];
				if (inputFilter.IsEmpty())
				{
					ref var touch = ref world.NewEntity().Get<InputComponent>();
					UpdateTouchData(ref touch, inputTouch);
					return;
				}
				
				foreach (var idx in inputFilter)
				{
					ref var touch = ref inputFilter.Get1(idx);
					UpdateTouchData(ref touch, inputTouch);
				}
			}
		}
		
		private void UpdateTouchData(ref InputComponent component, UnityEngine.Touch inputTouch)
		{
			switch (inputTouch.phase)
			{
				case TouchPhase.Ended:
				case TouchPhase.Canceled:
					SetComponentData(ref component, InputPhase.UP);
					break;
				case TouchPhase.Began:
					SetComponentData(ref component, InputPhase.DOWN);
					break;
				case TouchPhase.Moved:
				case TouchPhase.Stationary:
					SetComponentData(ref component, InputPhase.PRESS);
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}
		}

		private void SetComponentData(ref InputComponent component, InputPhase phase)
		{
			component.phase = phase;
			component.point = sceneData.Camera.ScreenToWorldPoint(Input.mousePosition);
			component.basePoint = Input.mousePosition;
		}
	}
}