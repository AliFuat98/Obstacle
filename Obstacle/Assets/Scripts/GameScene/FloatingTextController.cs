using System.Collections;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(ObjectPool))]
public class FloatingTextController : MonoBehaviour {
  [SerializeField] FloatingTextEventChannelSO eventChannel;

  ObjectPool objectPool;

  private void Start() {
    objectPool = GetComponent<ObjectPool>();
  }

  void OnEnable() {
    eventChannel.OnFloatTextPopUp += ShowFloatingText;
  }

  void OnDisable() {
    eventChannel.OnFloatTextPopUp -= ShowFloatingText;
  }

  public void ShowFloatingText(string text, Vector3 position) {
    var floatingTextInstance = objectPool.GetObject();

    floatingTextInstance.transform.position = position;
    var tmp = floatingTextInstance.GetComponent<TextMeshPro>();
    tmp.text = text;

    // Trigger the animation
    if (floatingTextInstance.TryGetComponent<Animator>(out var animator)) {
      animator.SetTrigger("PlayAnimation"); // Ensure this trigger matches your animation trigger
    } else {
      return;
    }

    // Start coroutine to return to pool after animation
    StartCoroutine(ReturnToPoolAfterAnimation(floatingTextInstance));
  }

  IEnumerator ReturnToPoolAfterAnimation(GameObject floatingTextInstance) {
    Animator animator = floatingTextInstance.GetComponent<Animator>();
    yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

    objectPool.ReturnObject(floatingTextInstance);
  }
}