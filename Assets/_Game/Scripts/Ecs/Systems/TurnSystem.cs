using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class TurnSystem : IEcsRunSystem {
    EcsWorldInject _world;
    EcsPoolInject<TimeState> _timeStates;
    EcsCustomInject<Shared> _shared;

    public void Run(IEcsSystems systems) {
      ref var time = ref _timeStates.Value.Get(_shared.Value.TimeEntity);
      if (time.MovesToResolve == 0) {
        time.MovesToResolve = ActorConfig.MaxSpeed;
        time.TurnCount++;
        _world.Value.SetGroup(Group.Turn, false);
      }
    }
  }
}
