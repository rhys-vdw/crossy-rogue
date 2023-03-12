using UnityEngine;

namespace Frog {
  public static class ArrayUtility {
     public static void Fill<T>(this T[,] array, T value) {
      var length0 = array.GetLength(0);
      var length1 = array.GetLength(1);
      for (int i = 0; i < length0; i++) {
        for (int j = 0; j < length1; j++) {
          array[i, j] = value;
        }
      }
    }
  }
}