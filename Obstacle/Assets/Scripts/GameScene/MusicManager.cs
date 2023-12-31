using UnityEngine;

public class MusicManager : MonoBehaviour {
  public static MusicManager Instance { get; private set; }

  [SerializeField] private float volume = 0.2f;
  private AudioSource audioSource;

  private void Awake() {
    if (Instance == null) {
      Instance = this;
      DontDestroyOnLoad(gameObject); 
      audioSource = GetComponent<AudioSource>();
      audioSource.volume = volume;
    } else if (Instance != this) {
      Destroy(gameObject);
    }
  }

  public float GetVolume() {
    return volume;
  }

  public void Mute() {
    audioSource.volume = 0f;
  }
}
