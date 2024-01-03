using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PlayerLaser : MonoBehaviour {
  [SerializeField] GameObject laserVisual;
  [SerializeField] float detectionRadius;
  [SerializeField] LayerMask obstacleLayer;

  List<GameObject> createdVisuals;

  float duration;

  void Start() {
    createdVisuals = new List<GameObject>();

    GameInput.Instance.OnLaserAction += Instance_OnLaserAction;
    duration = laserVisual.GetComponent<ParticleSystem>().main.duration;
  }

  private void Instance_OnLaserAction(object sender, System.EventArgs e) {
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
      hitCollider.GetComponent<ObstacleMovement>().LaserTouch();
    }
  }

  IEnumerator DestroyItseltf() {
    yield return new WaitForSeconds(duration);
    foreach (var visual in createdVisuals) {
      Destroy(visual);
    }
  }

  void OnDrawGizmos() {
    Gizmos.color = Color.red; // Set the color of the Gizmo
    Gizmos.DrawWireSphere(transform.position, detectionRadius);
  }
}