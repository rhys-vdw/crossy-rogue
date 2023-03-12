using UnityEngine;

namespace Frog {
  struct Player { }

  struct Move {
    public Vector2Int Direction;

    public Move(Vector2Int move) {
      Direction = move;
    }
  }

  struct Transform {
    public Vector2Int Position;

    public Transform(Vector2Int position) {
      Position = position;
    }
  }
}