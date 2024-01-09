using UnityEngine;

public class EggReset : MonoBehaviour {
  [SerializeField] GameObject EggGameObject;
  [SerializeField] float probability;

  private void OnEnable() {
    float randomValue = Random.value;

    if (randomValue >= probability) {
      EggGameObject.SetActive(true);
    }
  }
}