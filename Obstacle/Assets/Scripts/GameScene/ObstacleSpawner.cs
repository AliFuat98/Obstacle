using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
  [SerializeField] GameObject obstaclePrefab;
  [SerializeField] Vector2 spawnIntervalRange = new(1.0f, 3.0f);

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
    var obstacle = Instantiate(obstaclePrefab, transform.position, Quaternion.identity,transform);
    obstacle.transform.forward = (Vector3.zero - transform.position).normalized;
  }

  void ResetTimer() {
    // Set the timer to a random value within the specified range
    timer = Random.Range(spawnIntervalRange.x, spawnIntervalRange.y);
  }
}