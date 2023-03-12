using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(TileConfig))]
  class TileConfig : ScriptableObject {
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Color BackgroundColor { get; private set; } = Color.black;
    [field: SerializeField] public Color ForegroundColor { get; private set; } = Color.white;
  }
}
