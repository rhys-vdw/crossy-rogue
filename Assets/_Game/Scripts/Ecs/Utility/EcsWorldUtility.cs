using Leopotam.EcsLite;
using Leopotam.EcsLite.ExtendedSystems;

namespace Frog {
  public static class EcsWorldUtility {
    public static ref T AddComponent<T>(this EcsWorld world, int entity, T component) where T : struct {
      var pool = world.GetPool<T>();
      ref var c = ref pool.Add(entity);
      c = component;
      return ref c;
    }

    public static int? GetSingleton<T>(this EcsWorld world) where T : struct {
      var pool = world.Filter<T>().End();
      foreach (var entity in pool) {
        return entity;
      }
      return null;
    }

    public static void SetGroup(this EcsWorld world, string groupName, bool isEnabled) {
      var entity = world.NewEntity();
      world.AddComponent(entity, new EcsGroupSystemState {
        Name = groupName,
        State = isEnabled
      });
    }
  }
}