using UnityEngine;

namespace Frog {
  public class ActorView : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer;

    public void Initialize(Sprite sprite, Color color) {
      _renderer.sprite = sprite;
      _renderer.color = color;
    }
  }
}
