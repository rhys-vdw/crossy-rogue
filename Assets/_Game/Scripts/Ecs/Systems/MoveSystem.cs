using Leopotam.EcsLite;

namespace Frog {
  class MoveSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    EcsPool<Move> _moves;
    EcsPool<Transform> _transforms;

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Move>().End();
      _moves = _world.GetPool<Move>();
      _transforms = _world.GetPool<Transform>();
    }

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter) {
        ref var m = ref _moves.Get(entity);
        ref var t = ref _transforms.Get(entity);
        t.Position += m.Direction;
      }
    }
  }
}
