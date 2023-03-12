using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;
using UnityEngine;

namespace Frog {
  class InputSystem : IEcsPreInitSystem, IEcsRunSystem {
    static readonly KeyCode[] UpKeys = new [] { KeyCode.UpArrow, KeyCode.W, KeyCode.Z };
    static readonly KeyCode[] RightKeys = new [] { KeyCode.RightArrow, KeyCode.D };
    static readonly KeyCode[] DownKeys = new [] { KeyCode.DownArrow, KeyCode.S };
    static readonly KeyCode[] LeftKeys = new [] { KeyCode.LeftArrow, KeyCode.A, KeyCode.Q };

    EcsWorld _world;
    EcsPool<Move> _moves;
    readonly int _playerEntity;

    public InputSystem(int playerEntity) {
      _playerEntity = playerEntity;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _moves = _world.GetPool<Move>();
    }

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
        ref var move = ref _moves.Get(_playerEntity);
        move.Direction = direction;

        // Enable turn.
        _world.SetGroup(Group.Turn, true);
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