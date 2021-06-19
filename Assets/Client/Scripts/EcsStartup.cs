using Client.Scripts.Data;
using Client.Scripts.Systems;
using Client.Scripts.UnityComponents;
using DG.Tweening;
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
				.Add(new StandaloneInputSystem());
#else
				.Add(new MobileInputSystem());
#endif
			
			systems
				.Add(inputSystems)
				.Add(guiSystems)
				.Add(new GameTileCreateHandleSystem())
				.Add(new RewardHandleSystem())
				// register your systems here, for example:
				// .Add (new TestSystem1 ())
				// .Add (new TestSystem2 ())

				// register one-frame components (order is important), for example:
				// .OneFrame<TestComponent1> ()
				// .OneFrame<TestComponent2> ()

				// inject service instances here (order doesn't important), for example:
				// .Inject (new CameraService ())
				// .Inject (new NavMeshSupport ())
				.Inject(sceneData)
				.Inject(gameSettings)
				
				.Init();

			DOTween.Init();
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