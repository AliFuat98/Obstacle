using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
  [SerializeField] List<GameObject> obstaclePrefabs;
  [SerializeField] Vector2 spawnIntervalRange = new(1.0f, 3.0f);
  [SerializeField] Transform[] spawnPoints;

  private float timer;

  private void Start() {
    for (int i = 0; i < spawnPoints.Length; ++i) {
      StartCoroutine(SpawnObstacles(i));
    }
  }

  // get random obstacle
  GameObject ObstacleFactory() {
    int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
    return obstaclePrefabs[obstacleIndex];
  }

  IEnumerator SpawnObstacles(int index) {
    while (true) // Infinite loop to keep spawning obstacles
    {
      yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));

      GameObject obstacle = ObstacleFactory(); // Get an obstacle from the factory
      obstacle = Instantiate(obstacle);

      obstacle.transform.position = new Vector3(spawnPoints[index].position.x, obstacle.transform.position.y, spawnPoints[index].position.z);
      obstacle.transform.SetParent(transform, false);
      obstacle.transform.forward = (Vector3.zero - spawnPoints[index].position).normalized;
    }
  }
}