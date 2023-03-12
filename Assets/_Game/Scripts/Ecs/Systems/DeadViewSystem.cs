using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class DeadViewSystem : IEcsRunSystem {
    readonly EcsFilterInject<Inc<View, Dead>> _filter;
    readonly EcsPoolInject<Body> _bodies;
    readonly EcsCustomInject<SettingsConfig> _settings;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var body = ref _bodies.Value.Get(entity);
        Object.Instantiate(
          _settings.Value.SplatPrefab,
          (Vector2) body.Position,
          Quaternion.Euler(0, 0, Random.Range(0, 360f))
        );
      }
    }
  }
}
