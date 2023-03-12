using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class DeleteMarkedSystem : IEcsRunSystem {
    readonly EcsWorldInject _world = default;
    readonly EcsFilterInject<Inc<MarkedForDeletion>> _filter;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        _world.Value.DelEntity(entity);
      }
    }
  }
}
