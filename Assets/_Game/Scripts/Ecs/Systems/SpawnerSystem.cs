using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace Frog {
  class SpawnerSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorldInject _world;
    EcsFilterInject<Inc<Spawner>> _filter;
    EcsPoolInject<Spawner> _spawners;
    EcsPoolInject<TimeState> _timeStates;
    EcsPoolInject<Body> _bodies;
    EcsPoolInject<Move> _moves;
    EcsCustomInject<Map> _map;
    EcsCustomInject<Shared> _shared;

    public void PreInit(IEcsSystems systems) {
      foreach (var info in _map.Value.Spawners) {
        var entity = _world.Value.NewEntity();
        ref var spawner = ref _spawners.Value.Add(entity);
        spawner.Info = info;
        spawner.NextSpawnTurn = info.Config.RandomInterval();
      }
    }

    public void Run(IEcsSystems systems) {
      ref var time = ref _timeStates.Value.Get(_shared.Value.TimeEntity);

      foreach (var entity in _filter.Value) {
        ref var s = ref _spawners.Value.Get(entity);
        if (time.TurnCount >= s.NextSpawnTurn) {
          var spawnerConfig = s.Info.Config;
          var actorEntity = _world.Value.NewEntity();

          // Create body.
          ref var body = ref _bodies.Value.Add(actorEntity);
          body = new Body(
            new Vector2Int(s.Info.IsLeft ? -1 : _map.Value.Width, s.Info.Row),
            spawnerConfig.Actor
          );

          // Create move.
          ref var move = ref _moves.Value.Add(actorEntity);
          move = new Move(new(
            x: s.Info.IsLeft ? 1 : -1,
            y: 0
          ));

          s.NextSpawnTurn = time.TurnCount + spawnerConfig.RandomInterval();
        }
      }
    }
  }
}
