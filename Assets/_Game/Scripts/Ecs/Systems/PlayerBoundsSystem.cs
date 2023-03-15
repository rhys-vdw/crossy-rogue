using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class PlayerBoundsSystem : IEcsRunSystem {
    EcsPoolInject<Body> _actors;
    EcsFilterInject<Inc<Player>> _filter;
    EcsCustomInject<SettingsConfig> _settings;

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter.Value) {
        ref var actor = ref _actors.Value.Get(entity);
        actor.Position.Clamp(Vector2Int.zero, _settings.Value.MapSize);
      }
    }
  }
}