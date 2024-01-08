using UnityEngine;

public class Heart : MonoBehaviour, ICollectible {
  [SerializeField] FloatingTextEventChannelSO eventChannel;

  float scorePoint = 100f;
  public void Collect(PlayerMarker player) {
    player.GetComponent<HealthSystem>().Heal(1);
    ScoreHandle.Instance.IncreaseScore(scorePoint);
    SoundManager.Instance.PlayerCollect();


    if (eventChannel != null) {
      eventChannel.RaiseEvent(scorePoint.ToString(), transform.position + Vector3.up);
    }

    gameObject.SetActive(false);
  }
}