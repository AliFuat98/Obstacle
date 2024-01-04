using UnityEngine;

public class FloatingTextController : MonoBehaviour {
  public ObstacleEventChannel eventChannel;
  public GameObject floatingTextPrefab;

  void OnEnable() {
    eventChannel.OnObstacleDestroyed += ShowFloatingText;
  }

  void OnDisable() {
    eventChannel.OnObstacleDestroyed -= ShowFloatingText;
  }

  public void ShowFloatingText(string text, Vector3 position) {
    var floatingTextInstance = Instantiate(floatingTextPrefab, position, Quaternion.identity);
    // Additional code...
  }
}
