#pragma warning disable IDE0044

using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Frog {
  class GameManager : MonoBehaviour {
    [Header("Config")]
    [SerializeField] SettingsConfig _settings;

    [Header("Sprites")]
    [SerializeField] ActorView _actorPrefab;

    EcsSystems _systems;
    EcsWorld _world;

    ActorView CreateView(ActorConfig config) {
      var actor = Instantiate(_actorPrefab, Vector3.zero, Quaternion.identity, transform);
      actor.Initialize(config);
      return actor;
    }

#pragma warning disable IDE0051
    void Start() {
      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      var playerEntity = _world.NewEntity();
      _world.AddComponent(playerEntity, new Player());
      _world.AddComponent(playerEntity, new Body(Vector2Int.zero));
      _world.AddComponent(playerEntity, new Move(Vector2Int.zero));
      _world.AddComponent(playerEntity, new View(CreateView(_settings.FrogConfig)));

      _systems
        .Add(new InputSystem(playerEntity))
        .AddGroup(Group.Turn, false, null,
          new MoveSystem(),
          new DisableGroupSystem(Group.Turn),
          new ViewSystem()
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