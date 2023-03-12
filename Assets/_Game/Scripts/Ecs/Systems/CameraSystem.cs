using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class CameraSystem : IEcsRunSystem {
    EcsCustomInject<Shared> _shared;
    EcsCustomInject<Map> _map;
    EcsPoolInject<Body> _actors;

    public void Run(IEcsSystems systems) {
      ref var actor = ref _actors.Value.Get(_shared.Value.PlayerEntity);
      var camera = _shared.Value.Camera;
      var minY = camera.orthographicSize - 0.5f;
      var t = camera.transform;
      t.position = new(
        _map.Value.Width / 2f - 0.5f,
        Mathf.Max(actor.Position.y, minY),
        t.position.z
      );
    }
  }
}
