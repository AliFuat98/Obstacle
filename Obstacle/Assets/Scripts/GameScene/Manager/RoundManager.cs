using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
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
  List<Button> buttons;

  [Header("RoundSettings")]
  [SerializeField] int objectPerRound;

  [Range(0f, 2f)]
  [SerializeField] float scaleFactor;

  [SerializeField] Slider roundSlider;
  [SerializeField] float fillSpeed = 0.5f;
  float targetProgress = 0f;

  [SerializeField] TextMeshProUGUI roundCountText;
  [SerializeField] TextMeshProUGUI roundCountTextOnProgressBar;

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

  [SerializeField] TextMeshProUGUI laserDescriptionText;

  int currentRound;
  int totalSpawnedObject = 1;

  public int TotalSpawnedObject {
    get {
      return totalSpawnedObject;
    }
    set {
      if (value % objectPerRound == 0) {
        currentRound++;
        objectPerRound += (int)(objectPerRound * scaleFactor);
        value = 1;
        RoundOver();
      }
      totalSpawnedObject = value;
      targetProgress = (float)totalSpawnedObject / objectPerRound;
    }
  }

  HealthSystem playerHealthSystem;
  PlayerLaser playerLaser;
  PlayerJump playerJump;

  IRoundFeature extraLifeFeature;
  IRoundFeature laserFeature;
  IRoundFeature extraJumpFeature;

  private void Awake() {
    lifeButton.onClick.AddListener(() => {
      HandleRoundFeature(extraLifeFeature);
    });
    LaserButton.onClick.AddListener(() => {
      HandleRoundFeature(laserFeature);
    });
    JumpButton.onClick.AddListener(() => {
      HandleRoundFeature(extraJumpFeature);
    });

    NextRoundButton.onClick.AddListener(() => {
      pauseGameControl.ResumeGame();
      obstacleSpawner.ResetRoundInfos();
      RoundOverMenu.SetActive(false);
      targetProgress = 0f;
      roundSlider.value = 0f;
    });
  }

  void Start() {
    RoundOverMenu.SetActive(false);
    currentRound = 1;
    targetProgress = 0f;
    roundSlider.value = 0f;

    // initilize features
    var player = FindObjectOfType<PlayerMarker>();
    playerHealthSystem = player.GetComponent<HealthSystem>();
    playerLaser = player.GetComponent<PlayerLaser>();
    playerJump = player.GetComponent<PlayerJump>();

    extraLifeFeature = new ExtraLifeRoundFeature(extraLifePrice, extraLifePriceScaleFactor, playerHealthSystem);
    laserFeature = new LaserRoundFeature(laserPrice, laserPriceScaleFactor, playerLaser);
    extraJumpFeature = new ExtraJumpRoundFeature(extraJumpPrice, extraJumpPriceScaleFactor, playerJump);

    roundCountTextOnProgressBar.text = "Round: 1";

    buttons = new() {
      lifeButton,
      JumpButton,
      LaserButton,
      NextRoundButton,
    };
  }

  void Update() {
    if (roundSlider.value < targetProgress) {
      roundSlider.value += fillSpeed * Time.deltaTime;
    }
  }

  private void HandleRoundFeature(IRoundFeature feature) {
    if (feature.CanPay(ScoreHandle.Instance.GetEggCount()) && feature.IsPossible()) {
      feature.Execute();
      feature.UpdatePrice();
      UpdatePriceVisual();
    }
  }

  private void UpdatePriceVisual() {
    extraLifePriceText.text = (playerHealthSystem.GetHealt() >= playerHealthSystem.GetMaxHealt()) ? "Max": extraLifeFeature.GetPriceText();

    extraJumpPriceText.text = extraJumpFeature.GetPriceText();

    laserPriceText.text = laserFeature.GetPriceText();
    laserDescriptionText.text = $"Decrease laser cooldown ({playerLaser.cooldownDuration} -> {playerLaser.cooldownDuration - 1 })";
    if (playerLaser.cooldownDuration == playerLaser.MinCooldownDuration) {
      laserDescriptionText.text = $"Decrease laser cooldown ({playerLaser.cooldownDuration})";
      laserPriceText.text = "Max";
    }
  }

  void RoundOver() {
    pauseGameControl.PauseGame();
    StartCoroutine(DisableButtonsTemporarily());
    roundCountText.text = $"Round {currentRound}";
    roundCountTextOnProgressBar.text = $"Round: {currentRound}";
    roundSlider.value = 1;
    UpdatePriceVisual();

    RoundOverMenu.SetActive(true);
  }

  IEnumerator DisableButtonsTemporarily() {
    // Disable all buttons
    foreach (var button in buttons) {
      button.interactable = false;
    }

    // Wait for 0.5 seconds using unscaled time
    float elapsed = 0;
    while (elapsed < .5f) {
      elapsed += Time.unscaledDeltaTime;
      yield return null;
    }

    // Enable all buttons
    foreach (var button in buttons) {
      button.interactable = true;
    }
  }

  public int GetObjectPerRound() {
    return objectPerRound;
  }
}