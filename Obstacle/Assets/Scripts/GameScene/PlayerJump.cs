using UnityEngine;

public class PlayerJump : MonoBehaviour {
  [SerializeField] float firstJumpForce = 17f; // Adjust the jump force as needed
  [SerializeField] float jumpForce = 10f; 
  [SerializeField] int maxJumps = 2;
  [SerializeField] float maxYVelocity = 10f;

  Rigidbody rb;
  bool isGrounded;
  bool isFirstJump;
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
      rb.velocity = Vector3.zero;
      var forceToApply = isFirstJump ? firstJumpForce : jumpForce;
      rb.AddForce(Vector3.up * forceToApply, ForceMode.Impulse);

      shouldJump = false; // Reset the jump flag
      jumpCount++;
      isFirstJump = false;
    }

    if (rb.velocity.y > maxYVelocity) {
      rb.velocity = new Vector3(rb.velocity.x, maxYVelocity, rb.velocity.z);
    }
  }

  void OnCollisionEnter(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = true;
      jumpCount = 0;
      isFirstJump = true;
    }
  }

  void OnCollisionExit(Collision collision) {
    if (collision.gameObject.GetComponent<GroundMarker>() != null) {
      isGrounded = false;
    }
  }
}