using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {
  [SerializeField] GameObject laserVisual;
  [SerializeField] float detectionRadius;
  [SerializeField] LayerMask obstacleLayer;

  List<GameObject> createdVisuals;

  float duration;

  public bool IsOnCooldown => Time.time - lastLaunchTime < cooldownDuration;
  public float CooldownRemaining => Mathf.Max(0, cooldownDuration - (Time.time - lastLaunchTime));

  public float cooldownDuration;
  public float MinCooldownDuration;
  private float lastLaunchTime;

  void Start() {
    createdVisuals = new List<GameObject>();

    GameInput.Instance.OnLaserAction += Instance_OnLaserAction;
    duration = laserVisual.GetComponent<ParticleSystem>().main.duration;

    lastLaunchTime = -cooldownDuration;
  }

  private void Instance_OnLaserAction(object sender, System.EventArgs e) {
    if (IsOnCooldown) {
      return;
    }
    lastLaunchTime = Time.time;

    GameObject createdVisual = Instantiate(laserVisual, transform);

    GameObject secondCreatedVisual = Instantiate(laserVisual, transform);
    secondCreatedVisual.transform.rotation = Quaternion.Euler(0, -90, 0);

    createdVisuals.Add(createdVisual);
    createdVisuals.Add(secondCreatedVisual);

    StartCoroutine(DestroyItseltf());

    StartCoroutine(DetectObstacleCoroutine());
  }

  // every half of a second it will execute detectObstacle function until the duration finish
  IEnumerator DetectObstacleCoroutine() {
    var rate = 0.5f;
    var totalCyle = (int)(duration / rate);
    for (int i = 0; i < totalCyle; i++) {
      DetectObstacles();
      yield return new WaitForSeconds(rate);
    }
  }

  void DetectObstacles() {
    Vector3 playerPosition = transform.position; // Assuming this script is attached to the player
    Collider[] hitColliders = Physics.OverlapSphere(playerPosition, detectionRadius, obstacleLayer);

    foreach (var hitCollider in hitColliders) {
      // if the obstacle is pass then return
      if (hitCollider.transform.position.x > transform.position.y + 1) {
        continue;
      }

      if (hitCollider.transform.position.z < transform.position.z -1) {
        continue;
      }
      hitCollider.GetComponent<ObstacleMovement>().LaserTouch();
    }
  }

  IEnumerator DestroyItseltf() {
    yield return new WaitForSeconds(duration);
    foreach (var visual in createdVisuals) {
      Destroy(visual);
    }
  }

  public void DecreaseCooldown(float amount) {
    cooldownDuration = Mathf.Max(0, cooldownDuration - amount);
  }

  void OnDrawGizmos() {
    Gizmos.color = Color.red; // Set the color of the Gizmo
    Gizmos.DrawWireSphere(transform.position, detectionRadius);
  }
}