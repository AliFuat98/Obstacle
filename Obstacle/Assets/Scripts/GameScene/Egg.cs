using UnityEngine;

public class Egg : MonoBehaviour, ICollectible {
  public void Collect() {
    // Code for what happens when the egg is collected
    ScoreHandle.Instance.IncreaseEggCount();
    SoundManager.Instance.PlayerCollect();

    gameObject.SetActive(false);
  }
}