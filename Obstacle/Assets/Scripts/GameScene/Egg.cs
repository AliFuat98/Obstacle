using UnityEngine;

public class Egg : MonoBehaviour, ICollectible {
  float eggScorePoint = 20f;

  public FloatingTextEventChannelSO eventChannel;

  public void Collect() {
    // Code for what happens when the egg is collected
    ScoreHandle.Instance.IncreaseEggCount();
    ScoreHandle.Instance.IncreaseScore(eggScorePoint);
    SoundManager.Instance.PlayerCollect();

    if (eventChannel != null) {
      eventChannel.RaiseEvent(eggScorePoint.ToString(), transform.position + Vector3.up * 2);
    }

    gameObject.SetActive(false);
  }
}