using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ObstacleMovement : MonoBehaviour {
  public float speed = -5.0f;

  private void Update() {
    transform.Translate(speed * Time.deltaTime * Vector3.forward);
  }

  private void OnTriggerEnter(Collider other) {
    if (other.gameObject.GetComponent<PlayerMarker>() != null) {
      SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
  }

  private void OnBecameInvisible() {
    Destroy(gameObject);
  }
}