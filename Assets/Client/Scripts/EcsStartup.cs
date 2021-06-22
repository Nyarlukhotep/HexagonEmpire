using System;
using Client.Scripts.Components;
using Client.Scripts.Data;
using Client.Scripts.Systems;
using Client.Scripts.UnityComponents;
using Leopotam.Ecs;
using Leopotam.Ecs.Ui.Systems;
using UnityEngine;

namespace Client
{
	sealed class EcsStartup : MonoBehaviour
	{
		[SerializeField] 
		private EcsUiEmitter uiEmitter;
		[SerializeField] 
		private CommonSceneData sceneData;
		[SerializeField] 
		private GameSettings gameSettings;

		private EcsWorld world;
		private EcsSystems systems;

		private void Start()
		{
			// void can be switched to IEnumerator for support coroutines.

			world = new EcsWorld();
			systems = new EcsSystems(world);
#if UNITY_EDITOR
			Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
			Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(systems);
#endif
			var guiSystems = new EcsSystems(world)
				.InjectUi(uiEmitter);

			var inputSystems = new EcsSystems(world)
#if UNITY_EDITOR || UNITY_STANDALONE
				.Add(new StandaloneMouseInputSystem());
#else
				.Add(new MobileInputSystem());
#endif
			
			systems
				.Add(new AutoSaveSystem())
				.Add(new GameInitSystem())
				.Add(new CameraDragSystem())
				.Add(inputSystems)
				.Add(guiSystems)
				.Add(new GameTileClickHandleSystem())
				.Add(new GameTileCreateSystem())
				.Add(new RewardHandleSystem())
				.Add(new UpdatePlayerDataSystem())
				.Add(new SaveLoadDataSystem())

				// register one-frame components (order is important), for example:
				.OneFrame<TileClickEventComponent>()
				.OneFrame<TileCreateComponent>()
				.OneFrame<UpdatePlayerDataComponent>()
				.OneFrame<SaveDataComponent>()

				// inject service instances here (order doesn't important), for example:
				.Inject(sceneData)
				.Inject(gameSettings)
				
				.Init();
		}

		private void Update()
		{
			systems?.Run();
		}

		private void OnDestroy()
		{
			systems?.Destroy();
			systems = null;
			world?.Destroy();
			world = null;
		}
	}
}