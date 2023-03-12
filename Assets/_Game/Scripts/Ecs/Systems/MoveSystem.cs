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

    int[,] _groundMap;
    const int Empty = -1;

    public void PreInit(IEcsSystems systems) {
      var map = _map.Value;
      _groundMap = new int[map.Width, map.Height];
    }

    public void Run(IEcsSystems systems) {
      _groundMap.Fill(Empty);
      foreach (var entity in _filter.Value) {
        ref var m = ref _moves.Value.Get(entity);
        ref var a = ref _actors.Value.Get(entity);
        var prev = a.Position;
        var blocker = GetGround(prev);
        if (blocker is int blockingEntity) {
          ref var c = ref _collisions.Value.Add(_world.Value.NewEntity());
          c.EntityA = entity;
          c.EntityB = blockingEntity;
        }
        a.Position += m.Direction * a.Config.Speed;
        SetGround(a.Position, entity);
      }
    }

    void SetGround(Vector2Int position, int value) {
      if (_map.Value.HasTile(position)) {
        _groundMap[position.x, position.y] = value;
      }
    }

    int? GetGround(Vector2Int position) {
      if (_map.Value.HasTile(position)) {
        var value = _groundMap[position.x, position.y];
        return value == Empty ? null : value;
      }
      return null;
    }
  }
}
