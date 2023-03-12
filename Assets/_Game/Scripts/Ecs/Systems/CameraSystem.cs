using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class CameraSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    int _playerEntity;
    Camera _camera;
    EcsPool<Body> _actors;
    float _x;

    public CameraSystem(int playerEntity, Camera camera, float mapWidth) {
      _playerEntity = playerEntity;
      _camera = camera;
      _x = mapWidth / 2;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _actors = _world.GetPool<Body>();
    }

    public void Run(IEcsSystems systems) {
      ref var actor = ref _actors.Get(_playerEntity);
      var minY = _camera.orthographicSize;
      var t = _camera.transform;
      t.position = new(
        _x,
        Mathf.Max(actor.Position.y, minY),
        t.position.z
      );
    }
  }
}
