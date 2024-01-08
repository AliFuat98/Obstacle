using UnityEngine;

public class Egg : MonoBehaviour, ICollectible {
  [SerializeField] FloatingTextEventChannelSO eventChannel;

  float eggScorePoint = 20f;

  public void Collect(PlayerMarker player) {
    // Code for what happens when the egg is collected
    ScoreHandle.Instance.IncreaseEggCount(1);
    ScoreHandle.Instance.IncreaseScore(eggScorePoint);
    SoundManager.Instance.PlayerCollect();

    if (eventChannel != null) {
      eventChannel.RaiseEvent(eggScorePoint.ToString(), transform.position + Vector3.up);
    }

    gameObject.SetActive(false);
  }
}