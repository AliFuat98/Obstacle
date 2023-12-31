using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
  public GameObject obstaclePrefab;
  public Vector2 spawnIntervalRange = new(1.0f, 3.0f);
  public Vector3 spawnPoint = new(0, 0, 0);

  private float timer;

  private void Start() {
    ResetTimer();
  }

  private void Update() {
    timer -= Time.deltaTime;
    if (timer <= 0) {
      SpawnObstacle();
      ResetTimer();
    }
  }

  void SpawnObstacle() {
    // Instantiate the obstacle prefab at the spawn point
    Instantiate(obstaclePrefab, spawnPoint, Quaternion.identity, transform);
  }

  void ResetTimer() {
    // Set the timer to a random value within the specified range
    timer = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
  }
}