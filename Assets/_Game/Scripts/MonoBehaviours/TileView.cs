#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class TileView : MonoBehaviour {
    [SerializeField] SpriteRenderer _foreground;
    [SerializeField] SpriteRenderer _background;

    public void Initialize(TileConfig config) {
      _foreground.sprite = config.Sprite;
      _foreground.color = config.ForegroundColor;
      _background.color = config.BackgroundColor;
    }
  }
}
