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
    var floatingTextParentInstance = objectPool.GetObject();

    floatingTextParentInstance.transform.position = position;
    var tmp = floatingTextParentInstance.GetComponentInChildren<TextMeshPro>();
    tmp.text = text;

    // Trigger the animation
    var animator = floatingTextParentInstance.GetComponentInChildren<Animator>();
    animator.SetTrigger("PlayAnimation"); // Ensure this trigger matches your animation trigger

    // Start coroutine to return to pool after animation
    StartCoroutine(ReturnToPoolAfterAnimation(floatingTextParentInstance));
  }

  IEnumerator ReturnToPoolAfterAnimation(GameObject floatingTextParentInstance) {
    Animator animator = floatingTextParentInstance.GetComponentInChildren<Animator>();
    yield return new WaitUntil(() => animator.GetCurrentAnimatorStateInfo(0).normalizedTime >= 1);

    objectPool.ReturnObject(floatingTextParentInstance);
  }
}