using UnityEngine;
using UnityEngine.UI;

public class PauseGameControl : MonoBehaviour {
  private bool isGamePaused = false;

  [SerializeField] Button pauseButton;
  [SerializeField] Button StartButton;

  [SerializeField] Button InfoButton;
  [SerializeField] Button InfoCloseButton;
  [SerializeField] GameObject InfoPageGO;
  [SerializeField] GameObject MainMenuGO;

  private void Start() {
    pauseButton.onClick.AddListener(() => {
      PauseGame();
    });
    StartButton.onClick.AddListener(() => {
      ResumeGame();
      MainMenuGO.SetActive(false);
    });
    InfoButton.onClick.AddListener(() => {
      InfoPageGO.SetActive(true);
    });
    InfoCloseButton.onClick.AddListener(() => {
      InfoPageGO.SetActive(false);
    });
    PauseGame();
  }

  // Call this method when the 'Start' button is clicked
  public void ResumeGame() {
    Time.timeScale = 1;
    isGamePaused = false;
  }

  public void PauseGame() {
    isGamePaused = !isGamePaused;

    if (isGamePaused) {
      // Pause the game
      Time.timeScale = 0;
    } else {
      // Resume the game
      ResumeGame();
    }
  }

  public bool IsGamePaused() {
    return isGamePaused;
  }
}