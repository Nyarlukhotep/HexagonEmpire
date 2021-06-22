using Client.Scripts.Components;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class StandaloneMouseInputSystem : IEcsRunSystem
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

			CheckMouseInput(Input.GetMouseButton(0), InputPhase.PRESS);
			CheckMouseInput(Input.GetMouseButtonDown(0), InputPhase.DOWN);
			CheckMouseInput(Input.GetMouseButtonUp(0), InputPhase.UP);
		}

		private void CheckMouseInput(bool condition, InputPhase phase)
		{
			if (!condition)
				return;
			
			if (inputFilter.IsEmpty())
			{
				CreateNewComponent(phase);
				return;
			}
			
			foreach (var idx in inputFilter)
			{
				ref var component = ref inputFilter.Get1(idx);
				SetComponentData(ref component, phase);
			}
		}

		private void CreateNewComponent(InputPhase phase)
		{
			ref var component = ref world.NewEntity().Get<InputComponent>();
			SetComponentData(ref component, phase);
		}

		private void SetComponentData(ref InputComponent component, InputPhase phase)
		{
			component.phase = phase;
			component.point = sceneData.Camera.ScreenToWorldPoint(Input.mousePosition);
			component.basePoint = Input.mousePosition;
		}
	}
}