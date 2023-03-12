using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class MoveViewSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<View, Move>> _filter;
    readonly EcsPoolInject<Body> _bodies;
    readonly EcsPoolInject<View> _views;
    readonly EcsPoolInject<Move> _moves;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var v = ref _views.Value.Get(entity);
        ref var b = ref _bodies.Value.Get(entity);
        v.ActorView.transform.position = new Vector3(
          b.Position.x,
          b.Position.y,
          0
        );

        ref var move = ref _moves.Value.Get(entity);
        v.ActorView.SetDirection(move.Direction);
      }
    }
  }
}
