using System;
using Client.Scripts.Components;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using UnityEngine;

namespace Client.Scripts.Systems
{
	public class CameraDragSystem : IEcsInitSystem, IEcsRunSystem
	{
		private CommonSceneData sceneData;

		private EcsFilter<InputComponent> inputFilter;

		private Transform cameraTransform;
		private Vector3 lastPressPosition;
		
		public void Init()
		{
			cameraTransform = sceneData.Camera.transform;
		}
		
		public void Run()
		{
			foreach (var idx in inputFilter)
			{
				ref var input = ref inputFilter.Get1(idx);

				switch (input.phase)
				{
					case InputPhase.DOWN:
						lastPressPosition = input.point;
						break;
					case InputPhase.PRESS:
						cameraTransform.position = lastPressPosition - ((Vector3)input.point - cameraTransform.position);
						break;
				}
			}
		}
	}
}