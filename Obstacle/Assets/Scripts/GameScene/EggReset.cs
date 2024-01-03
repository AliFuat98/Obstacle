using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggReset : MonoBehaviour
{
  [SerializeField] GameObject EggGameObject;

  private void OnEnable() {
    EggGameObject.SetActive(true);
  }
}
