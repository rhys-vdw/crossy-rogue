using Leopotam.EcsLite;

namespace Frog {
  public static class EcsWorldUtility {
    public static ref T AddComponent<T>(this EcsWorld world, int entity, T component) where T : struct {
      var pool = world.GetPool<T>();
      ref var c = ref pool.Add(entity);
      c = component;
      return ref c;
    }
  }
}