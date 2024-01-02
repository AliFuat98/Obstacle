using System.Collections;
using UnityEngine;

public class InvulnerableVisual : MonoBehaviour {
  [SerializeField] SkinnedMeshRenderer skinnedMeshRenderer;

  [Range(0f, 1f)]
  [SerializeField] float flashEffectRate;

  HealthSystem playerHealtSystem;

  Coroutine lastFlashEffectCoroutine;

  private void Start() {
    playerHealtSystem = GetComponent<HealthSystem>();
    playerHealtSystem.OnIsInvulnerableChanged += PlayerHealtSystem_OnIsInvulnerableChanged;
  }

  private void PlayerHealtSystem_OnIsInvulnerableChanged(object sender, HealthSystem.OnIsInvulnerableChangedEventArgs e) {
    if (e.isInvulnerable) {
      lastFlashEffectCoroutine = StartCoroutine(FlashEffect());
    } else {
      StopCoroutine(lastFlashEffectCoroutine);
      skinnedMeshRenderer.enabled = true;
    }
  }

  IEnumerator FlashEffect() {
    while (true) {
      skinnedMeshRenderer.enabled = false;
      yield return new WaitForSeconds(flashEffectRate);
      skinnedMeshRenderer.enabled = true;
      yield return new WaitForSeconds(flashEffectRate);
    }
  }
}