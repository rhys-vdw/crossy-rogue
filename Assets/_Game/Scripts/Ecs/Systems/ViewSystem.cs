using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class ViewSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<Body>, Exc<View>> _withoutViewFilter;
    readonly EcsFilterInject<Inc<View>> _viewFilter;
    readonly EcsPoolInject<Body> _bodies;
    readonly EcsPoolInject<View> _views;
    readonly EcsPoolInject<Move> _moves;
    readonly Transform _viewParent;
    readonly ActorView _actorPrefab;

    public ViewSystem(Transform viewParent, ActorView actorPrefab) {
      _viewParent = viewParent;
      _actorPrefab = actorPrefab;
    }

    public void Run(IEcsSystems systems) {
      // Create missing entities.
      // TODO: Move to another system.
      foreach (var entity in _withoutViewFilter.Value) {
        ref var b = ref _bodies.Value.Get(entity);
        var instance = CreateView(b.Config);
        ref var view = ref _views.Value.Add(entity);
        view.ActorView = instance;
      }

      // Update views.
      foreach (var entity in _viewFilter.Value) {
        ref var v = ref _views.Value.Get(entity);
        ref var b = ref _bodies.Value.Get(entity);
        v.ActorView.transform.position = new Vector3(
          b.Position.x,
          b.Position.y,
          0
        );
        if (_moves.Value.Has(entity)) {
          ref var move = ref _moves.Value.Get(entity);
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
