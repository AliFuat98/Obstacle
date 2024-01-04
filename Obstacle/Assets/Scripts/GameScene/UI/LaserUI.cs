using UnityEngine;
using UnityEngine.UI;

public class LaserUI : MonoBehaviour {
  [SerializeField] Button laserButton;
  PlayerLaser playerLaser;
  Image cooldownImage;

  private void Awake() {
    laserButton.onClick.AddListener(() => {
      GameInput.Instance.Laser_performed(new());
    });
  }

  private void Start() {
    cooldownImage = laserButton.GetComponent<Image>();
    cooldownImage.fillAmount = 0;

    playerLaser = FindObjectOfType<PlayerMarker>().GetComponent<PlayerLaser>();
  }

  private void Update() {
    if (playerLaser.IsOnCooldown) {
      cooldownImage.fillAmount = playerLaser.CooldownRemaining / playerLaser.cooldownDuration;
    } else {
      cooldownImage.fillAmount = 0;
    }
  }
}