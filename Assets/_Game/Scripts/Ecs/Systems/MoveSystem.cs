using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.ExtendedFilters;
using UnityEngine;

namespace Frog {
  class MoveSystem : IEcsPreInitSystem, IEcsRunSystem {
    readonly EcsWorldInject _world = default;
    readonly EcsFilterInject<Inc<Move, Body>, Exc<Dead>> _filter;
    readonly EcsPoolInject<Move> _moves;
    readonly EcsPoolInject<Body> _actors;
    readonly EcsPoolInject<TimeState> _time;
    readonly EcsPoolInject<Collision> _collisions;
    readonly EcsCustomInject<Map> _map;
    EcsCustomInject<Shared> _shared;

    int?[,] _groundMap;
    EcsFilterReorderHandler _getSpeed;

    public void PreInit(IEcsSystems systems) {
      var map = _map.Value;
      _groundMap = new int?[map.Width, map.Height];
      _getSpeed = GetSpeed;
    }

    int GetSpeed(int entity) {
      ref var actor = ref _actors.Value.Get(entity);
      return actor.Config.Speed;
    }

    public void Run(IEcsSystems systems) {
      ref var time = ref _time.Value.Get(_shared.Value.TimeEntity);
      if (time.MovesToResolve == 0) {
        throw new System.InvalidOperationException($"No moves to resolve");
      }
      foreach (var entity in _filter.Value) {
        ref var move = ref _moves.Value.Get(entity);
        ref var a = ref _actors.Value.Get(entity);
        SetGround(a.Position, null);
        if (a.Config.Speed >= time.MovesToResolve) {
          a.Position += move.Direction;
        }
        var blocker = GetGround(a.Position);
        if (blocker is int blockingEntity) {
          ref var c = ref _collisions.Value.Add(_world.Value.NewEntity());
          c.EntityA = entity;
          c.EntityB = blockingEntity;
        } else {
          SetGround(a.Position, entity);
        }
      }
      time.MovesToResolve--;
    }

    void SetGround(Vector2Int position, int? entity) {
      ref var time = ref _time.Value.Get(_shared.Value.TimeEntity);
      if (_map.Value.HasTile(position)) {
        _groundMap[position.x, position.y] = entity;
      }
    }

    int? GetGround(Vector2Int position) =>
      _map.Value.HasTile(position)
        ? _groundMap[position.x, position.y]
        : null;
  }
}
