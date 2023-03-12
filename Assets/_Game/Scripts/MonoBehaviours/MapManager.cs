#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class MapManager : MonoBehaviour {
    [SerializeField] TileView _tilePrefab;
    Map _map;

    public void Initialize(Map map) {
      _map = map;
      for (var y = 0; y < map.Height; y++) {
        for (var x = 0; x < map.Width; x++) {
          var tile = map.GetTile(x, y);
          if (tile == null) {
            Debug.LogError($"Tile is null at ({x}, {y})");
          } else {
            var view = Instantiate(
              _tilePrefab,
              new Vector3(x, y, 0),
              Quaternion.identity,
              transform
            );
            view.Initialize(tile);
          }
        }
      }
    }

#pragma warning disable IDE0051
    void OnDrawGizmos() {
      if (_map == null) return;
      Gizmos.matrix = transform.localToWorldMatrix;
      Gizmos.DrawWireCube(
        Vector3.zero,
        new Vector3(_map.Width, _map.Height, 0)
      );
    }
#pragma warning restore IDE0051
  }
}
