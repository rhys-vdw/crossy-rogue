#pragma warning disable IDE0044

using System.Collections.Generic;
using UnityEngine;

namespace Frog {
  readonly struct SpawnerInfo {
    public readonly SpawnerConfig Config;
    public readonly int Row;
    public readonly bool IsLeft;

    public SpawnerInfo(SpawnerConfig config, int row, bool isLeft) {
      Config = config;
      Row = row;
      IsLeft = isLeft;
    }
  }

  class Map {
    TileConfig[,] _tiles;
    List<SpawnerInfo> _spawners = new();

    public Map(Vector2Int size) {
      _tiles = new TileConfig[size.x, size.y];
    }

    public int Width => _tiles.GetLength(0);
    public int Height => _tiles.GetLength(1);

    public void SetTile(int x, int y, TileConfig tile) {
      _tiles[x, y] = tile;
    }

    public bool HasTile(Vector2Int p) => HasTile(p.x, p.y);

    public bool HasTile(int x, int y) =>
      x >= 0 && x < Width && y >= 0 && y < Height;

    public TileConfig GetTile(int x, int y) =>
      HasTile(x, y) ? _tiles[x, y] : null;

    public void AddSpawner(SpawnerInfo info) {
      _spawners.Add(info);
    }

    public IReadOnlyList<SpawnerInfo> Spawners => _spawners;
  }
}
