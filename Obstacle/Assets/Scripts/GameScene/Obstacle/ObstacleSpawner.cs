using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour {
  [SerializeField] RoundManager roundManager;

  [SerializeField] List<ObstacleSO> obstacleSOList;
  [SerializeField] Transform[] spawnPoints;
  [SerializeField] Transform poolObjectsParent;

  [Header("Spawn Settings")]
  [SerializeField] Vector2 spawnIntervalRange;

  [SerializeField] float minInternalRangeY;
  [SerializeField] int shrinkRateForIntervalRange = 40;

  List<GameObject>[] pools;
  float startInternalRangeY;

  private void Start() {
    InitializePools();

    for (int i = 0; i < spawnPoints.Length; ++i) {
      StartCoroutine(SpawnObstacles(i));
    }

    startInternalRangeY = spawnIntervalRange.y;
  }

  void InitializePools() {
    // obstacles
    pools = new List<GameObject>[obstacleSOList.Count];

    for (int i = 0; i < obstacleSOList.Count; i++) {
      pools[i] = new List<GameObject>();

      for (int j = 0; j < obstacleSOList[i].poolSize; j++) {
        GameObject obj = Instantiate(obstacleSOList[i].prefab);
        obj.transform.SetParent(poolObjectsParent, false);
        obj.SetActive(false);
        pools[i].Add(obj);
      }
    }
  }

  IEnumerator SpawnObstacles(int index) {
    while (true) {
      yield return new WaitForSeconds(Random.Range(spawnIntervalRange.x, spawnIntervalRange.y));

      GameObject obstacle = GetPooledObstacle(); // Use pooled obstacle
      obstacle.SetActive(true);
      obstacle.transform.position = new Vector3(spawnPoints[index].position.x, obstacle.transform.position.y, spawnPoints[index].position.z);
      obstacle.transform.SetParent(poolObjectsParent, false);
      obstacle.transform.forward = (Vector3.zero - spawnPoints[index].position).normalized;
      roundManager.TotalSpawnedObject++;

      spawnIntervalRange.y -= 1 / (float)shrinkRateForIntervalRange;
      spawnIntervalRange.y = Mathf.Clamp(spawnIntervalRange.y - (1 / (float)shrinkRateForIntervalRange), minInternalRangeY, startInternalRangeY);
    }
  }

  GameObject GetPooledObstacle() {
    float randomValue = Random.value;  // between 0-1
    float cumulativeProbability = 0f;

    int index = 0;
    foreach (ObstacleSO obstacleSO in obstacleSOList) {
      cumulativeProbability += obstacleSO.spawnProbability;
      if (randomValue <= cumulativeProbability) {
        // find and return an inactive object
        foreach (GameObject obj in pools[index]) {
          if (!obj.activeInHierarchy) {
            return obj;
          }
        }

        break;
      }
      index++;
    }

    // Optional: Expand pool if all objects are active
    index = 0;
    foreach (ObstacleSO obstacleSO in obstacleSOList) {
      cumulativeProbability += obstacleSO.spawnProbability;
      if (randomValue <= cumulativeProbability) {
        break;
      }
      index++;
    }

    GameObject newObj = Instantiate(obstacleSOList[index].prefab);
    newObj.SetActive(false);
    pools[index].Add(newObj);
    return newObj;
  }

  public void ReturnPool(GameObject obj) {
    obj.SetActive(false);
  }

  public void ResetRoundInfos() {
    for (int i = 0; i < pools.Length; i++) {
      for (int j = 0; j < pools[i].Count; j++) {
        ReturnPool(pools[i][j]);
      }
    }

    spawnIntervalRange.y = startInternalRangeY;
  }
}