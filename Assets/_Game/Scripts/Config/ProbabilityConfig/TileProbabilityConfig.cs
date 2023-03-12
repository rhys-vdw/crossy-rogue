using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(TileProbabilityConfig))]
  class TileProbabilityConfig : ProbabilityConfig<TileConfig> { }
}
