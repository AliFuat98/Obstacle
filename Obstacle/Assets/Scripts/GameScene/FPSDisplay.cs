using TMPro;
using UnityEngine;

public class FPSDisplay : MonoBehaviour {
  public TextMeshProUGUI fpsText;
  public float updateInterval = 0.5f; // The interval at which the FPS is updated
  private float accum = 0.0f;
  private int frames = 0;
  private float timeLeft;

  private void Start() {
    // Ensure you have assigned the Text UI element to fpsText in the Inspector
    if (fpsText == null) {
      Debug.LogError("FPS Text UI element is not assigned!");
      enabled = false;
      return;
    }

    timeLeft = updateInterval;

    Application.targetFrameRate = 60;
  }

  private void Update() {
    timeLeft -= Time.deltaTime;
    accum += Time.timeScale / Time.deltaTime;
    frames++;

    if (timeLeft <= 0.0f) {
      if (frames > 0) {
        // Calculate smoothed average FPS
        float fps = accum / frames;

        // Update the UI text
        fpsText.text = "FPS: " + Mathf.Round(fps);
      } else {
        // Handle the case where there's not enough data yet
        fpsText.text = "FPS: Calculating...";
      }

      // Reset variables for the next interval
      timeLeft = updateInterval;
      accum = 0.0f;
      frames = 0;
    }
  }
}