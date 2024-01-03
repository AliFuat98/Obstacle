
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
  [SerializeField] List<GameObject> obstaclePrefabs;
  [SerializeField] Vector2 spawnIntervalRange;
  [SerializeField] Transform[] spawnPoints;
  [SerializeField] int poolSize = 10;
  [SerializeField] Transform poolObjectsParent;

  private List<GameObject>[] pools;

  private void Start() {
    InitializePools();

    for (int i = 0; i < spawnPoints.Length; ++i) {
      StartCoroutine(SpawnObstacles(i));
    }
  }

  void InitializePools() {
    pools = new List<GameObject>[obstaclePrefabs.Count];

    for (int i = 0; i < obstaclePrefabs.Count; i++) {
      pools[i] = new List<GameObject>();

      for (int j = 0; j < poolSize; j++) {
        GameObject obj = Instantiate(obstaclePrefabs[i]);
        obj.transform.SetParent(poolObjectsParent, false);
        obj.SetActive(false);
        pools[i].Add(obj);
      }
    }
  }

  GameObject GetPooledObstacle(int prefabIndex) {
    foreach (var obj in pools[prefabIndex]) {
      if (!obj.activeInHierarchy) {
        return obj;
      }
    }

    // Optional: Expand pool if all objects are active
    GameObject newObj = Instantiate(obstaclePrefabs[prefabIndex]);
    newObj.SetActive(false);
    pools[prefabIndex].Add(newObj);
    return newObj;
  }

  public void ReturnPool(GameObject obj) {
    obj.SetActive(false);
  }

  IEnumerator SpawnObstacles(int index) {
    while (true) {
      yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));

      int obstacleIndex = Random.Range(0, obstaclePrefabs.Count);
      GameObject obstacle = GetPooledObstacle(obstacleIndex); // Use pooled obstacle
      obstacle.SetActive(true);
      obstacle.transform.position = new Vector3(spawnPoints[index].position.x, obstacle.transform.position.y, spawnPoints[index].position.z);
      obstacle.transform.SetParent(poolObjectsParent, false);
      obstacle.transform.forward = (Vector3.zero - spawnPoints[index].position).normalized;
    }
  }
}