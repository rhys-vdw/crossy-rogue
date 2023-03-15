using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace Frog {
  class DisableGroupSystem : IEcsRunSystem {
    EcsWorldInject _world = default;
    readonly string _groupName;

    public DisableGroupSystem(string groupName) {
      _groupName = groupName;
    }

    public void Run(IEcsSystems systems) {
      _world.Value.SetGroup(_groupName, false);
    }
  }
}
