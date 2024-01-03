using System.Collections.Generic;
using UnityEngine;

public class ParticalSpawner : MonoBehaviour {
  public static ParticalSpawner Instance { get; private set; }

  [SerializeField] Transform sparkObjectParent;
  [SerializeField] ParticleSystem sparkPartical;
  [SerializeField] int poolSize;

  List<ParticleSystem> sparkParticals;

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    InitializePools();
  }

  void InitializePools() {
    sparkParticals = new();

    for (int i = 0; i < poolSize; i++) {
      var partical = Instantiate(sparkPartical, sparkObjectParent);
      sparkParticals.Add(partical);
    }
  }

  public void SpawnPartical(Vector3 position) {
    foreach (ParticleSystem partical in sparkParticals) {
      if (partical.isStopped) {
        partical.transform.position = position;
        partical.Play();
        return;
      }
    }

    var newPartical = Instantiate(sparkPartical, position, sparkPartical.transform.rotation, sparkObjectParent);
    sparkParticals.Add(newPartical);
    newPartical.Play();
  }
}