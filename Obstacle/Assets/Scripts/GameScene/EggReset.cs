using UnityEngine;

public class EggReset : MonoBehaviour {
  [SerializeField] GameObject EggGameObject;

  private void OnEnable() {
    float randomValue = Random.value;

    if (randomValue >= 0.5f) {
      EggGameObject.SetActive(true);
    }
  }
}