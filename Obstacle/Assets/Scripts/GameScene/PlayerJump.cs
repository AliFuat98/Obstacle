using UnityEngine;

public class PlayerJump : MonoBehaviour {
  [SerializeField] float jumpForce = 7f; // Adjust the jump force as needed
  [SerializeField] int maxJumps = 2;
  [SerializeField] float maxYVelocity = 10f;

  Rigidbody rb;
  bool isGrounded;
  bool shouldJump;
  int jumpCount = 0;

  void Start() {
    rb = GetComponent<Rigidbody>();
    GameInput.Instance.OnJumpAction += GameInput_OnJumpAction;
  }

  private void GameInput_OnJumpAction(object sender, System.EventArgs e) {
    if (isGrounded || jumpCount < maxJumps) {
      shouldJump = true;
    }
  }

  void FixedUpdate() {
    // Apply forces in FixedUpdate
    if (shouldJump) {
      rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
      shouldJump = false; // Reset the jump flag
      jumpCount++;
    }

    if (rb.velocity.y > maxYVelocity) {
      rb.velocity = new Vector3(rb.velocity.x, maxYVelocity, rb.velocity.z);
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = true;
      jumpCount = 0;
    }
  }

  void OnCollisionExit(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = false;
    }
  }
}