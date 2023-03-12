
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class DeadSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<Dead>> _filter;
    readonly EcsPoolInject<MarkedForDeletion> _delete;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        _delete.Value.Add(entity);
      }
    }
  }
}
