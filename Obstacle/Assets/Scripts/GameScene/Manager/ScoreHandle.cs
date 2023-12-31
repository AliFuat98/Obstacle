using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreHandle : MonoBehaviour {
  public static ScoreHandle Instance { get; private set; }

  [SerializeField] TextMeshProUGUI scoreText;
  [SerializeField] TextMeshProUGUI eggCountText;
  [SerializeField] private PauseGameControl pauseGameControl;

  int eggCount = 0;
  float score = 0;
  float scoreRate = .5f;

  private void Awake() {
    Instance = this;
  }

  void Start() {
    StartCoroutine(IncreaseScoreRate());
  }

  void Update() {
    if (pauseGameControl != null && !pauseGameControl.IsGamePaused()) {
      score += scoreRate * Time.deltaTime;
      UpdateScoreDisplay();
    }
  }

  IEnumerator IncreaseScoreRate() {
    while (true) {
      yield return new WaitForSeconds(5);
      scoreRate += 0.1f;
    }
  }

  void UpdateScoreDisplay() {
    if (scoreText != null) {
      scoreText.text = $"Score\n{Mathf.CeilToInt(score)}";
    }
  }

  public void IncreaseEggCount(int amount) {
    eggCount += amount;
    UpadateEggDisplay();
  }

  public void DecreaseEggCount(int amount) {
    eggCount -= amount;
    if (eggCount < 0) {
      eggCount = 0;
      Debug.LogError("negatif egg count");
    }
    UpadateEggDisplay();
  }

  void UpadateEggDisplay() {
    if (eggCountText != null) {
      eggCountText.text = $"Egg\n{eggCount}";
    }
  }

  public float GetScore() {
    return score;
  }

  public int GetEggCount() {
    return eggCount;
  }

  public void IncreaseScore(float amount) {
    score += amount;
  }
}