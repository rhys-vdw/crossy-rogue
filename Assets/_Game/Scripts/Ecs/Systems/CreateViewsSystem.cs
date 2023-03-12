using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class CreateViewsSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<Body>, Exc<View>> _filter;
    readonly EcsPoolInject<Body> _bodies;
    readonly EcsPoolInject<View> _views;
    readonly EcsCustomInject<Shared> _shared;
    readonly EcsCustomInject<SettingsConfig> _settings;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var b = ref _bodies.Value.Get(entity);
        var instance = CreateView(b.Config);
        ref var view = ref _views.Value.Add(entity);
        view.ActorView = instance;
      }
    }

    ActorView CreateView(ActorConfig config) {
      var actor = Object.Instantiate(
        _settings.Value.ActorViewPrefab,
        Vector3.zero,
        Quaternion.identity,
        _shared.Value.ViewParent
      );
      actor.Initialize(config);
      return actor;
    }
  }
}
