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
    public ActorConfig Config;

    public Body(
      Vector2Int position,
      ActorConfig config
    ) {
      Position = position;
      Config = config;
    }
  }

  struct View {
    public ActorView ActorView;

    public View(ActorView view) {
      ActorView = view;
    }
  }
}