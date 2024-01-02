using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour {
  Button button;

  void Start() {
    button = GetComponent<Button>();

    // Ensure the button has an EventTrigger component
    EventTrigger trigger = button.gameObject.GetComponent<EventTrigger>()
      ?? button.gameObject.AddComponent<EventTrigger>();

    // Create a new entry for the PointerDown event
    EventTrigger.Entry entry = new() {
      eventID = EventTriggerType.PointerDown
    };

    // Define the method to call on PointerDown event
    entry.callback.AddListener((data) => OnPointerDownDelegate((PointerEventData)data));

    // Add the entry to the triggers list of the EventTrigger
    trigger.triggers.Add(entry);
  }

  private void OnPointerDownDelegate(PointerEventData data) {
    GameInput.Instance.Jump_performed(new()); // Call your jump logic here
  }
}