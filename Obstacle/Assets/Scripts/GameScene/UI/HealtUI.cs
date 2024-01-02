using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealtUI : MonoBehaviour {
  [SerializeField] HealthSystem playerHealtSystem;
  [SerializeField] Transform healtBarContainer;
  [SerializeField] GameObject prefabHealtIcon;

  private void Start() {
    playerHealtSystem.OnHealthChanged += PlayerHealtSystem_OnHealthChanged;
  }

  private void PlayerHealtSystem_OnHealthChanged(object sender, HealthSystem.OnHealthChangedEventArgs e) {
    // Delete all children
    int childCount = healtBarContainer.childCount;
    for (int i = childCount - 1; i >= 0; i--) {
      Destroy(healtBarContainer.GetChild(i).gameObject);
    }

    // recreate Them
    for (int i = 0; i < e.health; i++) {
      Instantiate(prefabHealtIcon, healtBarContainer);
    }
  }
}