using UnityEngine;

namespace Frog {
  public static class VectorUtility {
    public static Vector2 Round(Vector2 v) =>
      new (Mathf.Round(v.x), Mathf.Round(v.y));

    public static float AbsMax(this Vector2 v) =>
      Mathf.Max(Mathf.Abs(v.x), Mathf.Abs(v.y));

    public static float Max(this Vector2 v) =>
      Mathf.Max(v.x, v.y);

    public static Vector2 RandomOnUnitCircle() =>
      FromRadians(Random.value * Mathf.PI * 2f);

    public static Vector3 XYZ(float v) =>
      new (v, v, v);

    public static Vector2 XY(float v) =>
      new (v, v);

    public static Vector3 ToXZ(ref this Vector2 v) =>
      new (v.x, 0, v.y);

    public static Vector3 WithXZ(this Vector3 v, float xz) =>
      WithXZ(v, xz, xz);

    public static Vector3 WithXZ(this Vector3 v, float x, float z) =>
      new (x, v.y, z);

    public static Vector3 WithXY(this Vector3 v, float xy) =>
      WithXY(v, xy, xy);

    public static Vector3 WithXY(this Vector3 v, Vector2 xy) =>
      WithXY(v, xy.x, xy.y);

    public static Vector3 WithXY(this Vector3 v, float x, float y) =>
      new (x, y, v.z);

    public static Vector2 WithX(this Vector2 v, float x) =>
      new (x, v.y);

    public static Vector3 WithX(this Vector3 v, float x) =>
      new (x, v.y, v.z);

    public static Vector2 WithY(this Vector2 v, float y) =>
      new (v.x, y);

    public static Vector3 WithY(this Vector3 v, float y) =>
      new (v.x, y, v.z);

    public static Vector3 WithZ(this Vector2 v, float z) =>
      new (v.x, v.y, z);

    public static Vector3 WithZ(this Vector3 v, float z) =>
      new (v.x, v.y, z);

    public static Vector2 FromRadians(float radians) =>
      new (Mathf.Cos(radians), Mathf.Sin(radians));

    public static Vector2 RadiansLength(float radians, float length) =>
      new (Mathf.Cos(radians) * length, Mathf.Sin(radians) * length);

    public static Vector2 FromAngle(float angle) =>
      FromRadians(angle * Mathf.Deg2Rad);

    public static Vector2 AngleLength(float angle, float length) =>
      RadiansLength(angle * Mathf.Deg2Rad, length);

    public static float ToRadians(this Vector2 v) =>
      Mathf.Atan2(v.y, v.x);

    public static float ToAngle(this Vector2 v) =>
      ToRadians(v) * Mathf.Rad2Deg;

    public static string Format(this Vector2 v, string format = null) =>
      $"({v.x.ToString(format)}, {v.y.ToString(format)})";

    public static string Format(this Vector3 v, string format = null) =>
      $"({v.x.ToString(format)}, {v.y.ToString(format)}, {v.z.ToString(format)})";

    public static bool Approximately(Vector2 a, Vector2 b) =>
      Mathf.Approximately(a.x, b.x) &&
      Mathf.Approximately(a.y, b.y);

    public static bool Approximately(Vector3 a, Vector3 b) =>
      Mathf.Approximately(a.x, b.x) &&
      Mathf.Approximately(a.y, b.y) &&
      Mathf.Approximately(a.z, b.z);

    // From: http://answers.unity.com/comments/834881/view.html
    public static Vector2 RotateRadians(this Vector2 v, float radians) {
      var sin = Mathf.Sin(radians);
      var cos = Mathf.Cos(radians);

      var tx = v.x;
      var ty = v.y;

      return new (cos * tx - sin * ty, sin * tx + cos * ty);
    }

    public static Vector2 Rotate(this Vector2 v, float degrees) =>
      RotateRadians(v, degrees * Mathf.Deg2Rad);

    // UNTESTED
    public static Vector2 RotateCounterClockwise90(this Vector2 v) =>
      new(-v.y, v.x);

    // UNTESTED
    public static Vector2 RotateClockwise90(this Vector2 v) =>
      new(v.y, -v.x);

    public static Vector2 ClampMagnitude(Vector2 v, float min, float max) =>
      v.normalized * Mathf.Clamp(v.magnitude, min, max);

    public static Vector2 Clamp(Vector2 v, Rect rect) => new (
      Mathf.Clamp(v.x, rect.xMin, rect.xMax),
      Mathf.Clamp(v.y, rect.yMin, rect.yMax)
    );

    public static Vector2 Project(Vector2 a, Vector2 b) {
      var bUnit = b.normalized;
      return Vector2.Dot(a, bUnit) * bUnit;
    }

    public static Vector2 Reject(Vector2 a, Vector2 b) =>
      a - Project(a, b);

    public static float ArcLengthToRadians(
      float arcLength,
      float radius
    ) => arcLength / radius;

    public static float ArcLengthToAngle(
      float arcLength,
      float radius
    ) => ArcLengthToRadians(arcLength, radius) * Mathf.Rad2Deg;
  }
}
