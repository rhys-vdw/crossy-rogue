using Leopotam.EcsLite;

namespace Frog {
  class MoveSystem : IEcsPreInitSystem, IEcsRunSystem {
    EcsWorld _world;
    EcsFilter _filter;
    EcsPool<Move> _moves;
    EcsPool<Body> _actors;

    public void PreInit(IEcsSystems systems) {
      _world = systems.GetWorld();
      _filter = _world.Filter<Move>().End();
      _moves = _world.GetPool<Move>();
      _actors = _world.GetPool<Body>();
    }

    public void Run(IEcsSystems systems) {
      foreach (var entity in _filter) {
        ref var m = ref _moves.Get(entity);
        ref var a = ref _actors.Get(entity);
        a.Position += m.Direction * a.Config.Speed;
      }
    }
  }
}
