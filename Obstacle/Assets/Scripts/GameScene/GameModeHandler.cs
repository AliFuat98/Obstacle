using TMPro;
using UnityEngine;

public enum GameMode {
  Easy,
  Normal,
  Hard
}

public class GameModeHandler : MonoBehaviour {
  [SerializeField] ObstacleSpawner obstacleSpawner;
  [SerializeField] TMP_Dropdown dropdown;

  [Header("Spawn Rate")]
  [SerializeField] int easy;

  [SerializeField] int normal;
  [SerializeField] int hard;

  private void Start() {
    dropdown.onValueChanged.AddListener((int index) => {
      GameMode selectedMode = (GameMode)index;
      AdjustDifficulty(selectedMode);
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
}