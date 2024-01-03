using UnityEngine;

public class ObstacleMovement : MonoBehaviour {
  [SerializeField] ObstacleSO obstacleSO;

  [SerializeField] float destroyTimerMax;
  float destroyTimer;

  private void OnEnable() {
    destroyTimer = 0f;
  }

  private void Update() {
    transform.Translate(obstacleSO.moveSpeed * Time.deltaTime * Vector3.forward);

    destroyTimer += Time.deltaTime;
    if (destroyTimer > destroyTimerMax) {
      gameObject.SetActive(false);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.GetComponent<PlayerMarker>() != null) {
      HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
      if (!healthSystem.IsInvulnerabile()) {
        healthSystem.TakeDamage(1);
      }
    }
  }
}