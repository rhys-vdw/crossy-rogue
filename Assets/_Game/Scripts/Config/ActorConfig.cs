using UnityEngine;

namespace Frog {
  [CreateAssetMenu(menuName = nameof(Frog) + "/" + nameof(ActorConfig))]
  class ActorConfig : ScriptableObject {
    public const int MaxSpeed = 3;
    [field: SerializeField] public bool IsPlayer { get; private set; } = false;
    [field: SerializeField] public Sprite Sprite { get; private set; }
    [field: SerializeField] public Color Color { get; private set; } = Color.white;
    [field: SerializeField] public int Length { get; private set; } = 1;
    [field: SerializeField, Range(0, MaxSpeed)] public int Speed { get; private set; } = 1;
    [field: SerializeField] public int CombatStrength { get; private set; } = 1;
    [field: SerializeField] public int SortingOrder { get; private set; } = 0;
  }
}
