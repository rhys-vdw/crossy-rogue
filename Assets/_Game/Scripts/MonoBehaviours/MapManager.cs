#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  class MapManager : MonoBehaviour {
    Map _map;

    public void Initialize(Map map) {
      _map = map;
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
