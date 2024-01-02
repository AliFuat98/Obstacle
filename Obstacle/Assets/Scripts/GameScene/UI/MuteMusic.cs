using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class MuteMusic : MonoBehaviour {
  [SerializeField] Button muteButton;

  private void Awake() {
    muteButton.onClick.AddListener(() => {
      MusicManager.Instance.ToggleMute();
    });
  }
}