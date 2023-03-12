#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class ActorView : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer;

    public void Initialize(ActorConfig config) {
      _renderer.sprite = config.Sprite;
      _renderer.color = config.Color;
    }
  }
}
