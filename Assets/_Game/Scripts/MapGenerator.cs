#pragma warning disable IDE0044

using UnityEngine;

namespace Frog {
  static class MapGenerator {
    public static void Grass(Map map, int startY, int height, SettingsConfig settings) {
      var max = startY + height;
      for (var y = startY; y < max; y++) {
        for (var x = 0; x < map.Width; x++) {
          map.SetTile(x, y, settings.GrassTile.Sample());
        }
      }
    }

    public static void Road(Map map, int startY, int height, SettingsConfig settings) {
      for (var x = 0; x < map.Width; x++) {
        map.SetTile(x, startY, settings.RoadBottom);
      }
      var topY = startY + height - 1;
      for (var y = startY + 1; y < topY; y++) {
        for (var x = 0; x < map.Width; x++) {
          map.SetTile(x, y, settings.RoadMiddle);
        }
      }
      for (var x = 0; x < map.Width; x++) {
        map.SetTile(x, topY, settings.RoadTop);
      }
    }
  }
}
