using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(SpawnerConfig))]
  class SpawnerConfig : ScriptableObject {
    [field: SerializeField] public ActorConfig Actor { get; private set; }
    [field: SerializeField] public int MinInterval { get; private set; }
    [field: SerializeField] public int MaxInterval { get; private set; }

    public int RandomInterval() =>
      Random.Range(MinInterval, MaxInterval + 1);
  }
}
