using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class InputSystem : IEcsRunSystem {
    static readonly KeyCode[] UpKeys = new [] { KeyCode.UpArrow, KeyCode.W, KeyCode.Z };
    static readonly KeyCode[] RightKeys = new [] { KeyCode.RightArrow, KeyCode.D };
    static readonly KeyCode[] DownKeys = new [] { KeyCode.DownArrow, KeyCode.S };
    static readonly KeyCode[] LeftKeys = new [] { KeyCode.LeftArrow, KeyCode.A, KeyCode.Q };

    EcsWorldInject _world = default;
    EcsPoolInject<Move> _moves;
    EcsCustomInject<Shared> _shared;

    public void Run(IEcsSystems systems) {
      var direction = new Vector2Int();
      if (AnyKeyDown(UpKeys)) {
        direction.y++;
      }
      if (AnyKeyDown(RightKeys)) {
        direction.x++;
      }
      if (AnyKeyDown(DownKeys)) {
        direction.y--;
      }
      if (AnyKeyDown(LeftKeys)) {
        direction.x--;
      }

      if (direction != Vector2Int.zero) {
        // Set move.
        ref var move = ref _moves.Value.Get(_shared.Value.PlayerEntity);
        move.Direction = direction;

        // Enable turn.
        _world.Value.SetGroup(Group.Turn, true);
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