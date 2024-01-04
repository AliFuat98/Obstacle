using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour {
  [SerializeField] int poolSize = 10;
  [SerializeField] Transform parentForObject;
  [SerializeField] GameObject prefab;
  Queue<GameObject> pool = new();

  private void Start() {
    InitializePool();
  }

  public void InitializePool() {
    for (int i = 0; i < poolSize; i++) {
      GameObject obj = Instantiate(prefab, parentForObject);
      obj.SetActive(false);
      pool.Enqueue(obj);
    }
  }

  public GameObject GetObject() {
    if (pool.Count == 0) {
      ExpandPool(1);
    }

    GameObject obj = pool.Dequeue();
    obj.SetActive(true);
    return obj;
  }

  public void ReturnObject(GameObject obj) {
    obj.SetActive(false);
    pool.Enqueue(obj);
  }

  private void ExpandPool(int count) {
    for (int i = 0; i < count; i++) {
      GameObject obj = Instantiate(prefab, parentForObject);
      obj.SetActive(false);
      pool.Enqueue(obj);
    }
  }
}