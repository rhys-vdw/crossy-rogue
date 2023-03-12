#pragma warning disable IDE0044

using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using Leopotam.EcsLite.Di;

namespace Frog {
  class Shared {
    public readonly int PlayerEntity;
    public readonly Transform ViewParent;
    public readonly Camera Camera;

    public Shared(
      int playerEntity,
      Transform viewParent,
      Camera camera
    ) {
      PlayerEntity = playerEntity;
      ViewParent = viewParent;
      Camera = camera;
    }
  }

  class GameManager : MonoBehaviour {
    [Header("Scene")]
    [SerializeField] MapManager _mapManager;
    [SerializeField] Camera _camera;

    [Header("Config")]
    [SerializeField] SettingsConfig _settings;

    EcsSystems _systems;
    EcsWorld _world;

#pragma warning disable IDE0051
    void Start() {
      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      var map = new Map(_settings.MapSize);
      MapGenerator.Grass(map, 0, map.Height, _settings);
      for (var i = 0; i < _settings.RoadSpawners.Count; i++) {
        var start = 8 * i;
        MapGenerator.Road(map, start, 3, _settings);
        MapGenerator.SetSpawners(map, start, 3, _settings.RoadSpawners[i]);
      }
      MapGenerator.River(map, 4, 3, _settings);
      _mapManager.Initialize(map);

      var playerEntity = _world.NewEntity();
      _world.AddComponent(playerEntity, new Player());
      _world.AddComponent(playerEntity, new Body(
        new Vector2Int(map.Width / 2, 0),
        _settings.FrogActor
      ));
      _world.AddComponent(playerEntity, new Move(Vector2Int.zero));

      var shared = new Shared(
        playerEntity,
        transform,
        _camera
      );

      var timeEntity = _world.NewEntity();
      _world.AddComponent(timeEntity, new TimeState());

      _systems
        .AddGroup(Group.Input, true, null,
          new InputSystem(playerEntity)
        )
        .AddGroup(Group.Turn, false, null,
          new SpawnerSystem(map, timeEntity),
          new MoveSystem(),
          new CollisionSystem(),
          new DeadSystem(),
          new PlayerBoundsSystem(_settings.MapSize),
          new CreateViewsSystem(),
          new MoveViewSystem(),
          new DeadViewSystem(),
          new CameraSystem(),
          new DisableGroupSystem(Group.Turn),
          new DeleteMarkedSystem(),
          new TurnSystem(timeEntity)
        );

#if UNITY_EDITOR
      _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(
        entityNameFormat: "D"
      ));
#endif

      _systems
        .Inject(
          map,
          _settings,
          shared
        )
        .Inject()
        .Init();
    }

    void Update() {
      _systems.Run();
    }
#pragma warning restore IDE0051
  }
}