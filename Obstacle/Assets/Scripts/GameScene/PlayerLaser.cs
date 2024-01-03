using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour
{
  void Start() {
    GameInput.Instance.OnLaserAction += Instance_OnLaserAction;
  }

  private void Instance_OnLaserAction(object sender, System.EventArgs e) {
    Debug.Log("laser Fired");
  }
}
