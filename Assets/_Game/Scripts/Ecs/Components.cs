using UnityEngine;

namespace Frog {
  public struct InputState {
    public Vector2Int Move;

    public InputState(Vector2Int move) {
      Move = move;
    }
  }
}