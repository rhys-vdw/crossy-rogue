#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class Map {
    TileConfig[,] _tiles;

    public Map(Vector2Int size) {
      _tiles = new TileConfig[size.x, size.y];
    }

    public int Width => _tiles.GetLength(0);
    public int Height => _tiles.GetLength(1);

    public void SetTile(int x, int y, TileConfig tile) {
      _tiles[x, y] = tile;
    }

    public TileConfig GetTile(int x, int y) =>
      x >= 0 && x < Width && y >= 0 && y < Height
        ? _tiles[x, y]
        : null;
  }
}
