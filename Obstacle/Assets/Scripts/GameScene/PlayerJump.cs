using System;
using UnityEngine;

public class PlayerJump : MonoBehaviour {

  public event EventHandler<OnJumpedEventArgs> OnJumped;

  public class OnJumpedEventArgs : EventArgs {
    public bool useLastJumpAudioClip;
  }

  [SerializeField] float firstJumpForce; // Adjust the jump force as needed
  [SerializeField] float jumpForce;
  [SerializeField] int maxJumps;
  [SerializeField] float maxYVelocity;
  [SerializeField] ParticleSystem jumpParticleSystem;

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
      jumpParticleSystem.Play();
    }
  }

  void FixedUpdate() {
    // Apply forces in FixedUpdate
    if (shouldJump) {
      // if it is going down reset velocity to jump nicely
      if (rb.linearVelocity.y < 0) {
        rb.linearVelocity = Vector3.zero;
      }

      // first jump has more force
      var forceToApply = isFirstJump ? firstJumpForce : jumpForce;
      rb.AddForce(Vector3.up * forceToApply, ForceMode.Impulse);

      OnJumped?.Invoke(this, new OnJumpedEventArgs {
        useLastJumpAudioClip = !isFirstJump,
      });

      shouldJump = false; // Reset the jump flag
      jumpCount++;
      isFirstJump = false;
    }

    if (rb.linearVelocity.y > maxYVelocity) {
      rb.linearVelocity = new Vector3(rb.linearVelocity.x, maxYVelocity, rb.linearVelocity.z);
    }
  }

  public void IncreaseMaxJumpCount(int amount) {
    maxJumps += amount;
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