#pragma warning disable IDE0044

using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Frog {
  class GameManager : MonoBehaviour {
    [Header("Scene")]
    [SerializeField] MapManager _mapManager;
    [SerializeField] Camera _camera;

    [Header("Config")]
    [SerializeField] SettingsConfig _settings;

    [Header("Sprites")]
    [SerializeField] ActorView _actorPrefab;

    EcsSystems _systems;
    EcsWorld _world;

#pragma warning disable IDE0051
    void Start() {
      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      var map = new Map(_settings.MapSize);
      for (var y = 0; y < map.Height; y++) {
        for (var x = 0; x < map.Width; x++) {
          map.SetTile(x, y, _settings.GrassTile.Sample());
        }
      }
      _mapManager.Initialize(map);

      var playerEntity = _world.NewEntity();
      _world.AddComponent(playerEntity, new Player());
      _world.AddComponent(playerEntity, new Body(
        new Vector2Int(map.Width / 2, 0),
        _settings.FrogActor
      ));
      _world.AddComponent(playerEntity, new Move(Vector2Int.zero));

      _systems
        .Add(new InputSystem(playerEntity))
        .AddGroup(Group.Turn, false, null,
          new MoveSystem(),
          new DisableGroupSystem(Group.Turn),
          new ViewSystem(transform, _actorPrefab),
          new CameraSystem(playerEntity, _camera, map.Width)
        );

#if UNITY_EDITOR
      _systems.Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem(
        entityNameFormat: "D"
      ));
#endif

      _systems.Init();
    }

    void Update() {
      _systems.Run();
    }
#pragma warning restore IDE0051
  }
}