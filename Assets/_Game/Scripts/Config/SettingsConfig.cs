using UnityEngine;

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
    public TileConfig GrassTile { get; private set; }
  }
}
