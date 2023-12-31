using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
  public float jumpForce = 7f; // Adjust the jump force as needed
  Rigidbody rb;
  bool isGrounded;
  bool shouldJump;

  void Start() {
    rb = GetComponent<Rigidbody>();
    GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
  }

  private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
    if (isGrounded) {
      shouldJump = true;
    }
  }

  void FixedUpdate() {
    // Apply forces in FixedUpdate
    if (shouldJump) {
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      shouldJump = false; // Reset the jump flag
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = true;
    }
  }

  void OnCollisionExit(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = false;
    }
  }
}