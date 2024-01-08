using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HighScoreManager : MonoBehaviour {
  [SerializeField] GameModeHandler gameModeHandler;
  [SerializeField] Transform ParentHighManager;
  [SerializeField] TextMeshProUGUI LastScoreText;
  public HighScoreList highScoresList = new();

  private HealthSystem healthSystem;

  private void Start() {
    LoadHighScores();
    healthSystem = FindObjectOfType<PlayerMarker>().GetComponent<HealthSystem>();
    healthSystem.OnDeath += HealthSystem_OnDeath;
  }

  private void HealthSystem_OnDeath(object sender, System.EventArgs e) {
    var score = ScoreHandle.Instance.GetScore();
    AddHighScore((int)score);
    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
  }

  public void LoadHighScores() {
    string json = PlayerPrefs.GetString($"HighScores{gameModeHandler.GetGameMode()}");
    highScoresList = JsonUtility.FromJson<HighScoreList>(json);

    highScoresList ??= new HighScoreList() {
      scoreList = new List<int> { }
    };

    // update UI
    var index = 0;
    foreach (Transform child in ParentHighManager) {
      if (index == 0) {
        index++;
        continue;
      }

      if (highScoresList.scoreList.Count < index) {
        child.GetComponent<TextMeshProUGUI>().text = "-";
      } else {
        child.GetComponent<TextMeshProUGUI>().text = highScoresList.scoreList[index - 1].ToString();
      }

      index++;
    }

    // assign last score
    LastScoreText.text = highScoresList.LastScore.ToString();
  }

  public void AddHighScore(int score) {
    highScoresList.scoreList.Add(score);
    highScoresList.scoreList.Sort();
    highScoresList.scoreList.Reverse();
    while (highScoresList.scoreList.Count > 3) // Keep only the top 3 scores
    {
      highScoresList.scoreList.RemoveAt(3);
    }

    highScoresList.LastScore = score;

    // Save the updated high scores list
    SaveHighScores();
  }

  private void SaveHighScores() {
    string json = JsonUtility.ToJson(highScoresList);
    PlayerPrefs.SetString($"HighScores{gameModeHandler.GetGameMode()}", json);
    PlayerPrefs.Save();
  }
}

[System.Serializable]
public class HighScoreList {
  public List<int> scoreList;
  public int LastScore;
}