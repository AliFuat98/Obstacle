using UnityEngine;

public class Egg : MonoBehaviour, ICollectible {
  float eggScorePoint = 20f;

  public void Collect() {
    // Code for what happens when the egg is collected
    ScoreHandle.Instance.IncreaseEggCount();
    ScoreHandle.Instance.IncreaseScore(eggScorePoint);
    SoundManager.Instance.PlayerCollect();

    gameObject.SetActive(false);
  }
}