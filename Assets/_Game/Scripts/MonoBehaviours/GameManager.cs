using UnityEngine;
using Leopotam.EcsLite;

namespace Frog {
  public class GameManager : MonoBehaviour {
    EcsSystems _systems;
    EcsWorld _world;

#pragma warning disable IDE0051
    void Start() {
      _world = new EcsWorld();
      _systems = new EcsSystems(_world);

      _systems
        .Add(new InputSystem())
        .Init();
    }

    void Update() {
      _systems.Run();
    }
#pragma warning restore IDE0051
  }
}