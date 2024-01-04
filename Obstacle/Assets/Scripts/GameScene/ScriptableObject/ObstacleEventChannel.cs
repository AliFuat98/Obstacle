using UnityEngine;

[CreateAssetMenu(fileName = "ObstacleEventChannel", menuName = "Events/ObstacleEventChannel")]
public class ObstacleEventChannel : ScriptableObject {
  public delegate void ObstacleAction(string text, Vector3 position);
  public event ObstacleAction OnObstacleDestroyed;

  public void RaiseEvent(string text, Vector3 position) {
    OnObstacleDestroyed?.Invoke(text, position);
  }
}