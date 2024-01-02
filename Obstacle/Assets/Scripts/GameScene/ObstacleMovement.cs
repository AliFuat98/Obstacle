using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour {
  [SerializeField] float speed = -5.0f;

  private void Update() {
    transform.Translate(speed * Time.deltaTime * Vector3.forward);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.GetComponent<PlayerMarker>() != null) {

      HealthSystem healthSystem = other.gameObject.GetComponent<HealthSystem>();
      if (!healthSystem.IsInvulnerabile()) {
        healthSystem.TakeDamage(1);
      }
    }
  }

  private void OnBecameInvisible() {
    Destroy(gameObject);
  }
}