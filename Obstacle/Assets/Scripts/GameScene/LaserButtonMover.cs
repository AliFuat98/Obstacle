using UnityEngine;
using UnityEngine.EventSystems; // Required for UI event handling
using UnityEngine.UI;

public class LaserButtonMover : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler {
  private Button button;
  private float pressTime = 0;
  private bool isDragging = false;
  private const float HoldTime = 1.0f; // Time in seconds to trigger the move

  void Awake() {
    button = GetComponentInChildren<Button>();
  }

  public void OnPointerDown(PointerEventData eventData) {
    pressTime = Time.time;
    isDragging = false;
  }

  public void OnPointerUp(PointerEventData eventData) {
    if (isDragging) {
      // Handle button repositioning here
      transform.position = eventData.position;
    } else if (Time.time - pressTime < HoldTime) {
      // Perform normal button action
      button.onClick.Invoke();
    }
  }

  public void OnDrag(PointerEventData eventData) {
    if (Time.time - pressTime >= HoldTime) {
      isDragging = true;
      // Update button position while dragging
      transform.position = eventData.position;
    }
  }
}
