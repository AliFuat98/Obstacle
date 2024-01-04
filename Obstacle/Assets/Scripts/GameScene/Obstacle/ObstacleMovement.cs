using System.Collections;
using System.Diagnostics.Tracing;
using UnityEngine;

public class ObstacleMovement : MonoBehaviour {
  [SerializeField] ObstacleSO obstacleSO;

  [SerializeField] float destroyTimerMax;
  [SerializeField] FloatingTextEventChannelSO eventChannel;
  float destroyTimer;

  bool canMove = true;
  bool isFirstLaserCoroutine = true;

  private void OnEnable() {
    destroyTimer = 0f;
  }

  private void Update() {
    if (!canMove) {
      return;
    }
    transform.Translate(obstacleSO.moveSpeed * Time.deltaTime * Vector3.forward);

    destroyTimer += Time.deltaTime;
    if (destroyTimer > destroyTimerMax) {
      gameObject.SetActive(false);
    }
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.GetComponent<PlayerMarker>() != null) {
      HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
      if (!healthSystem.IsInvulnerabile() && canMove) {
        healthSystem.TakeDamage(1);
      }
    }
  }

  public void LaserTouch() {
    canMove = false;
    if (isFirstLaserCoroutine) {
      StartCoroutine(LaserTouchCoroutine());
    } else {
      isFirstLaserCoroutine = false;
    }
  }

  IEnumerator LaserTouchCoroutine() {
    yield return new WaitForSeconds(Random.Range(1, 2));

    ParticalSpawner.Instance.SpawnPartical(transform.position);

    // floating Text
    eventChannel.RaiseEvent(obstacleSO.scorePoint.ToString(), transform.position + Vector3.up * 1);

    gameObject.SetActive(false);
  }

  public ObstacleSO GetObstacleSO() {
    return obstacleSO;
  }
}