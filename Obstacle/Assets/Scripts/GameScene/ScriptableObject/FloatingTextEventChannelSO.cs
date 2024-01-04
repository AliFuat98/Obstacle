using UnityEngine;

[CreateAssetMenu(fileName = "FloatingTextEventChannel", menuName = "Events/FloatingTextEventChannel")]
public class FloatingTextEventChannelSO : ScriptableObject {
  public delegate void FloatingTextAction(string text, Vector3 position);
  public event FloatingTextAction OnFloatTextPopUp;

  public void RaiseEvent(string text, Vector3 position) {
    OnFloatTextPopUp?.Invoke(text, position);
  }
}