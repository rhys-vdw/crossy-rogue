using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class ViewSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    EcsPool<Body> _bodies;
    EcsPool<View> _views;

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<View>().End();
      _bodies = _world.GetPool<Body>();
      _views = _world.GetPool<View>();
    }

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter) {
        ref var v = ref _views.Get(entity);
        ref var b = ref _bodies.Get(entity);
        v.ActorView.transform.position = new Vector3(
          b.Position.x,
          b.Position.y,
          0
        );
      }
    }
  }
}
