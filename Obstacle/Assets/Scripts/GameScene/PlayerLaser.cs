using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaser : MonoBehaviour {
  [SerializeField] GameObject laserVisual;

  List<GameObject> createdVisuals;
  float duration;

  void Start() {
    createdVisuals = new List<GameObject>();

    GameInput.Instance.OnLaserAction += Instance_OnLaserAction;
    duration = laserVisual.GetComponent<ParticleSystem>().main.duration;
  }

  private void Instance_OnLaserAction(object sender, System.EventArgs e) {
    GameObject createdVisual = Instantiate(laserVisual, transform);

    GameObject secondCreatedVisual = Instantiate(laserVisual, transform);
    secondCreatedVisual.transform.rotation = Quaternion.Euler(0, -90, 0);

    createdVisuals.Add(createdVisual);
    createdVisuals.Add(secondCreatedVisual);

    StartCoroutine(DestroyItseltf());
  }

  IEnumerator DestroyItseltf() {
    yield return new WaitForSeconds(duration);
    foreach (var visual in createdVisuals) {
      Destroy(visual);
    }
  }
}