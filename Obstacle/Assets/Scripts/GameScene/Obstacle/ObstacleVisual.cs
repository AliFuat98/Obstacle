using System.Collections.Generic;
using UnityEngine;

public class ObstacleVisual : MonoBehaviour {
  [SerializeField] List<GameObject> visualList;

  private void OnEnable() {
    foreach (var visual in visualList) {
      visual.SetActive(false);
    }

    var randomIndex = Random.Range(0, visualList.Count);
    visualList[randomIndex].SetActive(true);
  }
}