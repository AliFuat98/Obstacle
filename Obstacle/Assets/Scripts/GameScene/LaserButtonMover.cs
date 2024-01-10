using UnityEngine;
using UnityEngine.EventSystems; // Required for UI event handling
using UnityEngine.UI;

public class LaserButtonMover : MonoBehaviour, IDragHandler, IEndDragHandler {

  private void Start() {
    Vector3 loadedPosition = LoadVector3("LaserPosition");

    if (loadedPosition != Vector3.zero) {
      transform.position = loadedPosition;
    }
  }

  public void OnDrag(PointerEventData eventData) {
    transform.position = eventData.position;
  }

  public void OnEndDrag(PointerEventData eventData) {
    SaveVector3("LaserPosition", new Vector3(eventData.position.x, eventData.position.y, 0));
  }

  void SaveVector3(string key, Vector3 value) {
    PlayerPrefs.SetFloat(key + "_X", value.x);
    PlayerPrefs.SetFloat(key + "_Y", value.y);
    PlayerPrefs.SetFloat(key + "_Z", value.z);
    PlayerPrefs.Save();
  }

  Vector3 LoadVector3(string key) {
    float x = PlayerPrefs.GetFloat(key + "_X");
    float y = PlayerPrefs.GetFloat(key + "_Y");
    float z = PlayerPrefs.GetFloat(key + "_Z");

    return new Vector3(x, y, z);
  }
}