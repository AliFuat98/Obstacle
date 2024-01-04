using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameInput : MonoBehaviour {
  public static GameInput Instance { get; private set; }
  private PlayerInputActions playerInputActions;

  public event EventHandler OnJumpAction;

  public event EventHandler OnLaserAction;

  private void Awake() {
    Instance = this;

    /// open new input system
    playerInputActions = new PlayerInputActions();

    /// sistemi aç
    playerInputActions.Player.Enable();

    playerInputActions.Player.Jump.performed += Jump_performed;
    playerInputActions.Player.Laser.performed += Laser_performed;
  }

  public void Laser_performed(InputAction.CallbackContext obj) {
    OnLaserAction?.Invoke(this, EventArgs.Empty);
  }

  public void Jump_performed(InputAction.CallbackContext context) {
    OnJumpAction?.Invoke(this, EventArgs.Empty);
  }

  private void OnDestroy() {
    playerInputActions.Player.Jump.performed -= Jump_performed;
    playerInputActions.Player.Laser.performed -= Laser_performed;
    playerInputActions.Dispose();
  }
}