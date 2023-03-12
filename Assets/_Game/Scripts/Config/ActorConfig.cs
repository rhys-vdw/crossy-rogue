using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(ActorConfig))]
  class ActorConfig : ScriptableObject {
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField] public int Length { get; private set; } = 1;
  }
}