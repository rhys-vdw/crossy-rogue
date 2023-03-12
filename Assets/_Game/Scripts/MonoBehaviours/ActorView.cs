#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class ActorView : MonoBehaviour {
    [SerializeField] SpriteRenderer _renderer;

    public void Initialize(ActorConfig config) {
      _renderer.sprite = config.Sprite;
      _renderer.color = config.Color;
      _renderer.sortingOrder = config.SortingOrder;
    }

    public void SetDirection(Vector2Int direction) {
      if (direction == Vector2Int.zero) {
        return;
      }
      var angle = Mathf.Repeat(VectorUtility.ToAngle(direction), 360);
      transform.localRotation = Quaternion.Euler(0, 0, angle % 180);
      transform.localScale = new(
        angle >= 180 ? -1 : 1,
        1,
        1
      );
    }
  }
}
