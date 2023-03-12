using UnityEngine;
using System;
using Random = UnityEngine.Random;

namespace Frog {
  abstract class ProbabilityConfig<T> : ScriptableObject {
#pragma warning disable IDE0044
    [SerializeField] Entry[] _entries;
#pragma warning restore IDE0044

    [Serializable]
    public struct Entry {
      public int Weight;
      public T Value;
    }

    public T Sample() {
      var index = SampleIndex();
      return index == -1
        ? default
        : _entries[index].Value;
    }

    int SampleIndex() {
      var totalWeight = 0;
      for (var i = 0; i < _entries.Length; i++) {
        var weight = _entries[i].Weight;
        if (weight < 0) {
          Debug.LogWarning("weight should be >= 0");
          weight = 0;
        }
        totalWeight += weight;
      }
      if (totalWeight == 0) {
        return -1;
      }
      var remaining = Random.Range(0, totalWeight);
      for (var i = 0; i < _entries.Length; i++) {
        var weight = _entries[i].Weight;
        remaining -= weight;
        if (weight > 0 && remaining < 0) {
          return i;
        }
      }
      throw new InvalidOperationException("Unreachable");
    }
  }
}
