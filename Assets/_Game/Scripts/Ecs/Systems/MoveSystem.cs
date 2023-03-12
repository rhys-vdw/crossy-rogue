using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class MoveSystem : IEcsPreInitSystem, IEcsRunSystem {
    readonly EcsWorldInject _world = default;
    readonly EcsFilterInject<Inc<Move>> _filter;
    readonly EcsPoolInject<Move> _moves;
    readonly EcsPoolInject<Body> _actors;
    readonly EcsPoolInject<Collision> _collisions;
    readonly EcsCustomInject<Map> _map;

    int?[,] _groundMap;

    public void PreInit(IEcsSystems systems) {
      var map = _map.Value;
      _groundMap = new int?[map.Width, map.Height];
    }

    public void Run(IEcsSystems systems) {
      _groundMap.Fill(null);
      foreach (var entity in _filter.Value) {
        ref var m = ref _moves.Value.Get(entity);
        ref var a = ref _actors.Value.Get(entity);
        a.Position += m.Direction * a.Config.Speed;
        var blocker = GetGround(a.Position);
        if (blocker is int blockingEntity) {
          ref var c = ref _collisions.Value.Add(_world.Value.NewEntity());
          c.EntityA = entity;
          c.EntityB = blockingEntity;
        } else {
          SetGround(a.Position, entity);
        }
      }
    }

    void SetGround(Vector2Int position, int value) {
      if (_map.Value.HasTile(position)) {
        _groundMap[position.x, position.y] = value;
      }
    }

    int? GetGround(Vector2Int position) =>
      _map.Value.HasTile(position)
        ? _groundMap[position.x, position.y]
        : null;
  }
}
