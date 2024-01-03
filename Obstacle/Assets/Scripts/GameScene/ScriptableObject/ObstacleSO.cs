using UnityEngine;

[CreateAssetMenu()]
public class ObstacleSO : ScriptableObject {
  public GameObject prefab;
  public float spawnProbability;
  public int poolSize;
  public float moveSpeed;
}