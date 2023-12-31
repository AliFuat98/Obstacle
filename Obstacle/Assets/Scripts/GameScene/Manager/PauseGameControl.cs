using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseGameControl : MonoBehaviour {
  private bool isGamePaused = false;
  [SerializeField] Button pauseButton;

  private void Start() {
    pauseButton.onClick.AddListener(() => {
      PauseGame();
    });
  }

  // Call this method when the 'Start' button is clicked
  public void StartGame() {
    if (!isGamePaused) {
      Time.timeScale = 1;
    }
  }

  public void PauseGame() {
    isGamePaused = !isGamePaused;

    if (isGamePaused) {
      // Pause the game
      Time.timeScale = 0;
    } else {
      // Resume the game
      Time.timeScale = 1;
    }
  }

  public bool IsGamePaused() {
    return isGamePaused;
  }
}