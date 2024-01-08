using TMPro;
using UnityEngine;

public enum GameMode {
  Easy,
  Normal,
  Hard
}

public class GameModeHandler : MonoBehaviour {
  [SerializeField] RoundManager roundManager;
  [SerializeField] ObstacleSpawner obstacleSpawner;
  [SerializeField] HighScoreManager highScoreManager;
  [SerializeField] TMP_Dropdown dropdown;

  [Header("Spawn Rate")]
  [SerializeField] int easy;

  [SerializeField] int normal;
  [SerializeField] int hard;

  GameMode selectedGameMode;

  private void Start() {
    dropdown.onValueChanged.AddListener((int index) => {
      selectedGameMode = (GameMode)index;
      AdjustDifficulty(selectedGameMode);
      highScoreManager.LoadHighScores();
      roundManager.SetroundCountTextOnProgressBar($"{selectedGameMode} Round 1");
    });

    AdjustDifficulty(GameMode.Easy);
  }

  void AdjustDifficulty(GameMode mode) {
    switch (mode) {
      case GameMode.Easy:
        obstacleSpawner.SetshrinkRateForIntervalRange(easy);
        break;

      case GameMode.Normal:
        obstacleSpawner.SetshrinkRateForIntervalRange(normal);
        break;

      case GameMode.Hard:
        obstacleSpawner.SetshrinkRateForIntervalRange(hard);
        break;
    }
  }

  public GameMode GetGameMode() {
    return selectedGameMode;
  }
}