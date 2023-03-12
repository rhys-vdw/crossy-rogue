using UnityEngine;

namespace Frog {
  struct Player { }

  struct Move {
    public Vector2Int Direction;

    public Move(Vector2Int move) {
      Direction = move;
    }
  }

  struct Body {
    public Vector2Int Position;

    public Body(Vector2Int position) {
      Position = position;
    }
  }
}