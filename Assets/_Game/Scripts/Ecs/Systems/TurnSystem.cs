using Leopotam.EcsLite;
using UnityEngine;

namespace Frog {
  class TurnSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsPool<TimeState> _timeStates;
    readonly int _timeEntity;

    public TurnSystem(int timeEntity) {
      _timeEntity = timeEntity;
    }

    public void PreInit(IEcsSystems systems) {
      _timeStates = systems.GetWorld().GetPool<TimeState>();
    }

    public void Run(IEcsSystems systems) {
      ref var time = ref _timeStates.Get(_timeEntity);
      time.TurnCount++;
    }
  }
}
