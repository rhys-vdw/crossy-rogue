using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class MoveSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<Move>> _filter;
    readonly EcsPoolInject<Move> _moves;
    readonly EcsPoolInject<Body> _actors;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var m = ref _moves.Value.Get(entity);
        ref var a = ref _actors.Value.Get(entity);
        a.Position += m.Direction * a.Config.Speed;
      }
    }
  }
}
