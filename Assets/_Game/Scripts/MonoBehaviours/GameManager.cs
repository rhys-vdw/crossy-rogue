using UnityEngine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Frog {
  public class GameManager : MonoBehaviour {
    EcsSystems _systems;
    EcsWorld _world;

#pragma warning disable IDE0051
    void Start() {
      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      var playerEntity = _world.NewEntity();
      _world.AddComponent(playerEntity, new Player());
      _world.AddComponent(playerEntity, new Transform(Vector2Int.zero));
      _world.AddComponent(playerEntity, new Move(Vector2Int.zero));

      _systems
        .Add(new InputSystem(playerEntity))
        .AddGroup(Group.Turn, false, null,
          new MoveSystem(),
          new DisableGroupSystem(Group.Turn)
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