using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class InputSystem : IEcsPreInitSystem, IEcsRunSystem {
    static readonly KeyCode[] UpKeys = new [] { KeyCode.UpArrow, KeyCode.W, KeyCode.Z };
    static readonly KeyCode[] RightKeys = new [] { KeyCode.RightArrow, KeyCode.D };
    static readonly KeyCode[] DownKeys = new [] { KeyCode.DownArrow, KeyCode.S };
    static readonly KeyCode[] LeftKeys = new [] { KeyCode.LeftArrow, KeyCode.A, KeyCode.Q };

    EcsWorld _world;

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems) {
      var move = new Vector2Int();
      if (AnyKeyDown(UpKeys)) {
        move.y++;
      }
      if (AnyKeyDown(RightKeys)) {
        move.x++;
      }
      if (AnyKeyDown(DownKeys)) {
        move.y--;
      }
      if (AnyKeyDown(LeftKeys)) {
        move.x--;
      }

      if (move != Vector2Int.zero) {
        var entity = _world.NewEntity();
        _world.AddComponent(entity, new InputState(move));
      }
    }

    static bool AnyKeyDown(KeyCode[] keys) {
      foreach (var key in keys) {
        if (Input.GetKeyDown(key)) return true;
      }
      return false;
    }
  }
}