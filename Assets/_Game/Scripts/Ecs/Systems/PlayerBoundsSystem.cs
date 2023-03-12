using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class PlayerBoundsSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsPool<Body> _actors;
    EcsFilter _filter;
    Vector2Int _bounds;

    public PlayerBoundsSystem(Vector2Int bounds) {
      _bounds = bounds;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _actors = _world.GetPool<Body>();
      _filter = _world.Filter<Player>().End();
    }

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter) {
        ref var actor = ref _actors.Get(entity);
        actor.Position.Clamp(Vector2Int.zero, _bounds);
      }
    }
  }
}