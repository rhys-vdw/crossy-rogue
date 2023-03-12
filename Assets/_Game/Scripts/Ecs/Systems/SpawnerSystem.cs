using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class SpawnerSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    EcsPool<Spawner> _spawners;
    EcsPool<TimeState> _timeStates;
    readonly Map _map;
    readonly int _timeEntity;

    public SpawnerSystem(Map map, int timeEntity) {
      _map = map;
      _timeEntity = timeEntity;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Spawner>().End();
      _spawners = _world.GetPool<Spawner>();
      _timeStates = _world.GetPool<TimeState>();

      foreach (var info in _map.Spawners) {
        var entity = _world.NewEntity();
        ref var spawner = ref _spawners.Add(entity);
        spawner.Info = info;
        spawner.NextSpawnTurn = info.Config.RandomInterval();
      }
    }

    public void Run(IEcsSystems systems) {
      ref var time = ref _timeStates.Get(_timeEntity);
      foreach (var entity in _filter) {
        ref var s = ref _spawners.Get(entity);
        if (time.TurnCount >= s.NextSpawnTurn) {
          var spawnerConfig = s.Info.Config;
          var actorEntity = _world.NewEntity();
          _world.AddComponent(actorEntity, new Body(
            new Vector2Int(s.Info.IsLeft ? -1 : _map.Width, s.Info.Row),
            spawnerConfig.Actor
          ));
          _world.AddComponent(actorEntity, new Move(new(
            x: (s.Info.IsLeft ? 1 : -1) * spawnerConfig.Actor.Speed,
            y: 0
          )));
          s.NextSpawnTurn = time.TurnCount + spawnerConfig.RandomInterval();
        }
      }
    }
  }
}
