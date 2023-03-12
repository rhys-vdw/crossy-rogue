using Leopotam.EcsLite;

namespace Frog {
  class DisableGroupSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    readonly string _groupName;

    public DisableGroupSystem(string groupName) {
      _groupName = groupName;
    }

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
    }

    public void Run(IEcsSystems systems) {
      _world.SetGroup(_groupName, false);
    }
  }
}
