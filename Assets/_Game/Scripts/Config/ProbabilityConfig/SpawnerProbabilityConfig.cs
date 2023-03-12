using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(SpawnerProbabilityConfig))]
  class SpawnerProbabilityConfig : ProbabilityConfig<SpawnerConfig> { }
}
