using UnityEngine;
using System.Collections.Generic;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(SettingsConfig))]
  class SettingsConfig : ScriptableObject {
    [field: Header("Map")]
    [field: SerializeField]
    public Vector2Int MapSize { get; private set; } = new (32, 32 * 20);

    [field: Header("Actors")]
    [field: SerializeField]
    public ActorConfig FrogActor { get; private set; }

    [field: Header("Tiles")]
    [field: SerializeField]
    public TileProbabilityConfig GrassTile { get; private set; }
    [field: SerializeField]
    public TileConfig RoadTop { get; private set; }
    [field: SerializeField]
    public TileConfig RoadMiddle { get; private set; }
    [field: SerializeField]
    public TileConfig RoadBottom { get; private set; }
    [field: SerializeField]
    public TileConfig Water { get; private set; }

    [field: Header("Spawners")]
#pragma warning disable IDE0044
    [SerializeField] SpawnerProbabilityConfig[] _roadSpawners;
    [SerializeField] SpawnerProbabilityConfig[] _riverSpawners;
#pragma warning restore IDE0044
    public IReadOnlyList<SpawnerProbabilityConfig> RoadSpawners => _roadSpawners;
    public IReadOnlyList<SpawnerProbabilityConfig> RiverSpawners => _riverSpawners;
  }
}
