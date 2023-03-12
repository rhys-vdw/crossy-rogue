using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class ViewSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _vithoutViewFilter;
    EcsFilter _viewFilter;
    EcsPool<Body> _bodies;
    EcsPool<View> _views;
    EcsPool<Move> _moves;
    readonly Transform _viewParent;
    readonly ActorView _actorPrefab;

    public ViewSystem(Transform viewParent, ActorView actorPrefab) {
      _viewParent = viewParent;
      _actorPrefab = actorPrefab;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _vithoutViewFilter = _world.Filter<Body>().Exc<View>().End();
      _viewFilter = _world.Filter<View>().End();
      _bodies = _world.GetPool<Body>();
      _views = _world.GetPool<View>();
      _moves = _world.GetPool<Move>();
    }

    public void Run(IEcsSystems systems) {
      // Create missing entities.
      // TODO: Move to another system.
      foreach (var entity in _vithoutViewFilter) {
        ref var b = ref _bodies.Get(entity);
        var instance = CreateView(b.Config);
        ref var view = ref _views.Add(entity);
        view.ActorView = instance;
      }

      // Update views.
      foreach (var entity in _viewFilter) {
        ref var v = ref _views.Get(entity);
        ref var b = ref _bodies.Get(entity);
        v.ActorView.transform.position = new Vector3(
          b.Position.x,
          b.Position.y,
          0
        );
        if (_moves.Has(entity)) {
          ref var move = ref _moves.Get(entity);
          v.ActorView.SetDirection(move.Direction);
        }
      }
    }

    ActorView CreateView(ActorConfig config) {
      var actor = Object.Instantiate(
        _actorPrefab,
        Vector3.zero,
        Quaternion.identity,
        _viewParent
      );
      actor.Initialize(config);
      return actor;
    }
  }
}
