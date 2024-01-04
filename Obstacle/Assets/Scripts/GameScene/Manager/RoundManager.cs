using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class RoundManager : MonoBehaviour {

  [Header("Scripts")]
  [SerializeField] ObstacleSpawner obstacleSpawner;

  [SerializeField] PauseGameControl pauseGameControl;
  [SerializeField] GameObject RoundOverMenu;

  [Header("Buttons")]
  [SerializeField] Button lifeButton;

  [SerializeField] Button LaserButton;
  [SerializeField] Button JumpButton;
  [SerializeField] Button NextRoundButton;

  [Header("RoundSettings")]
  [SerializeField] int objectPerRound;

  [Range(0f, 2f)]
  [SerializeField] float scaleFactor;

  [Header("Price Text")]
  [SerializeField] TextMeshProUGUI extraLifePriceText;

  [SerializeField] TextMeshProUGUI laserPriceText;
  [SerializeField] TextMeshProUGUI extraJumpPriceText;
  [SerializeField] int extraLifePrice = 1;

  [Range(0f, 2f)]
  [SerializeField] float extraLifePriceScaleFactor = 1;

  [SerializeField] int laserPrice = 1;

  [Range(0f, 2f)]
  [SerializeField] float laserPriceScaleFactor = 1;

  [SerializeField] int extraJumpPrice = 1;

  [Range(0f, 2f)]
  [SerializeField] float extraJumpPriceScaleFactor = 1;

  int currentRound;
  int totalSpawnedObject = 1;

  public int TotalSpawnedObject {
    get {
      return totalSpawnedObject;
    }
    set {
      if (value % objectPerRound == 0) {
        currentRound++;
        objectPerRound = (int)(objectPerRound * scaleFactor);
        value = 1;
        RoundOver();
      }
      totalSpawnedObject = value;
    }
  }

  HealthSystem playerHealthSystem;
  PlayerLaser playerLaser;
  PlayerJump playerJump;

  private void Awake() {
    lifeButton.onClick.AddListener(() => {
      if (!CanPayExtraLifePrice()) {
        return;
      }
      GetExtraLife();
      UpdateLifePrice();
      UpdatePriceVisual();
    });
    LaserButton.onClick.AddListener(() => {
      if (!CanPayLaserPrice()) {
        return;
      }
      DecreaseLaserCooldown();
      UpdateLaserPrice();
      UpdatePriceVisual();
    });
    JumpButton.onClick.AddListener(() => {
      if (!CanPayExtraJumpPrice()) {
        return;
      }
      IncreaseJumpCount();
      UpdateJumpPrice();
      UpdatePriceVisual();
    });

    NextRoundButton.onClick.AddListener(() => {
      pauseGameControl.ResumeGame();
      RoundOverMenu.SetActive(false);
    });
  }

  void Start() {
    RoundOverMenu.SetActive(false);
    currentRound = 1;

    var player = FindObjectOfType<PlayerMarker>();
    playerHealthSystem = player.GetComponent<HealthSystem>();
    playerLaser = player.GetComponent<PlayerLaser>();
    playerJump = player.GetComponent<PlayerJump>();
  }

  void RoundOver() {
    pauseGameControl.PauseGame();
    RoundOverMenu.SetActive(true);
    UpdatePriceVisual();
  }

  bool CanPayExtraLifePrice() {
    var eggcount = ScoreHandle.Instance.GetEggCount();
    if (eggcount >= extraLifePrice) {
      return true;
    }
    return false;
  }

  bool CanPayLaserPrice() {
    var eggcount = ScoreHandle.Instance.GetEggCount();
    if (eggcount >= laserPrice) {
      return true;
    }
    return false;
  }

  bool CanPayExtraJumpPrice() {
    var eggcount = ScoreHandle.Instance.GetEggCount();
    if (eggcount >= extraJumpPrice) {
      return true;
    }
    return false;
  }

  void GetExtraLife() {
    playerHealthSystem.Heal(1);
    ScoreHandle.Instance.DecreaseEggCount(extraLifePrice);
  }

  void DecreaseLaserCooldown() {
    playerLaser.DecreaseCooldown(1f);
    ScoreHandle.Instance.DecreaseEggCount(laserPrice);
  }

  void IncreaseJumpCount() {
    playerJump.IncreaseMaxJumpCount(1);
    ScoreHandle.Instance.DecreaseEggCount(extraJumpPrice);
  }

  void UpdateLifePrice() {
    extraLifePrice += (int)(extraLifePrice * extraLifePriceScaleFactor);
  }

  void UpdateLaserPrice() {
    laserPrice += (int)(laserPrice * laserPriceScaleFactor);
  }

  void UpdateJumpPrice() {
    extraJumpPrice += (int)(extraJumpPrice * extraJumpPriceScaleFactor);
  }

  void UpdatePriceVisual() {
    extraLifePriceText.text = $"{extraLifePrice} Egg";
    laserPriceText.text = $"{laserPrice} Egg";
    extraJumpPriceText.text = $"{extraJumpPrice} Egg";
  }
}