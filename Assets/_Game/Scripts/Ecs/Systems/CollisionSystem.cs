
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class CollisionSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<Collision>> _filter;
    readonly EcsPoolInject<Body> _actors;
    readonly EcsPoolInject<Dead> _dead;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var c = ref _filter.Pools.Inc1.Get(entity);
        ref var a = ref _actors.Value.Get(c.EntityA);
        ref var b = ref _actors.Value.Get(c.EntityB);

        if (a.Config.CombatStrength >= b.Config.CombatStrength) {
          _dead.Value.Add(c.EntityB);
        }
        if (b.Config.CombatStrength >= a.Config.CombatStrength) {
          _dead.Value.Add(c.EntityA);
        }
      }
    }
  }
}
