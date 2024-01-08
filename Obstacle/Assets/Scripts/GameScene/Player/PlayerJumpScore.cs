using UnityEngine;

public class PlayerJumpScore : MonoBehaviour {
  [SerializeField] FloatingTextEventChannelSO eventChannel;
  [SerializeField] float rayLength = 10f;
  [SerializeField] LayerMask layerMask;

  private bool xIsHitting;

  bool IsHitting {
    get { return xIsHitting; }
    set {
      if (xIsHitting == true && value == false) {
        // get point
        GetScore();
      }
      xIsHitting = value;
    }
  }

  RaycastHit lastHit;
  PlayerJump playerJump;

  private void Start() {
    playerJump = GetComponent<PlayerJump>();
  }

  void Update() {
    RaycastHit hit;
    Vector3 start = transform.position; // Start at the player's position
    Vector3 direction = Vector3.down;

    // Perform the raycast with the layer mask
    if (Physics.Raycast(start, direction, out hit, rayLength, layerMask)) {
      lastHit = hit;
      IsHitting = true;
    } else {
      IsHitting = false;
    }
  }

  void GetScore() {
    if (playerJump.JumpCount == 1) {
      var obstacle = lastHit.transform.GetComponent<ObstacleMovement>();
      var score = obstacle.GetObstacleSO().jumpScorePoint;
      ScoreHandle.Instance.IncreaseScore(score);
      eventChannel.RaiseEvent(score.ToString(), obstacle.transform.position + Vector3.up);
    }
  }
}