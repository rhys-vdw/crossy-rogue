
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class DeadSystem : IEcsRunSystem {
    readonly EcsWorldInject _world;
    readonly EcsFilterInject<Inc<Dead>> _filter;
    readonly EcsPoolInject<MarkedForDeletion> _delete;
    readonly EcsCustomInject<Shared> _shared;

    public void Run(IEcsSystems systems) {
      var playerEntity = _shared.Value.PlayerEntity;
      foreach (var entity in _filter.Value) {
        if (entity == playerEntity) {
          UnityEngine.Debug.Log("Game over!");
          _world.Value.SetGroup(Group.Input, false);
        } else {
          _delete.Value.Add(entity);
        }
      }
    }
  }
}
