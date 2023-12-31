using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreHandle : MonoBehaviour
{
  [SerializeField] TextMeshProUGUI scoreText;
  public float score = 0; // Current score
  public float scoreRate = .5f; // Points added per second
  [SerializeField] private PauseGameControl pauseGameControl; // Reference to the GameControl script

  void Start() {
    //pauseGameControl = FindObjectOfType<PauseGameControl>(); // Find the GameControl script
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

  private void UpdateScoreDisplay() {
    if (scoreText != null) {
      scoreText.text = Mathf.CeilToInt(score).ToString();
    }
  }
}
