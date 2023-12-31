using UnityEngine;

public class SoundManager : MonoBehaviour {
  public static SoundManager Instance { get; private set; }

  [SerializeField] PlayerJump playerJump;
  [SerializeField] private AudioClipSO audioClipSO;

  int lastJumpAudioClipIndex;

  private void Awake() {
    Instance = this;
  }

  private void Start() {
    playerJump.OnJumped += PlayerJump_OnJumped1;
  }

  private void PlayerJump_OnJumped1(object sender, PlayerJump.OnJumpedEventArgs e) {
    PlaySound(audioClipSO.jump, Vector3.zero, e.useLastJumpAudioClip);
  }

  private void PlaySound(AudioClip[] audioClipArray, Vector3 position, bool useLastJumpAudioClip, float volume = .5f) {
    var index = Random.Range(0, audioClipArray.Length);

    // first jump voice will be there until it hits the ground
    if (useLastJumpAudioClip) {
      PlaySound(audioClipArray[lastJumpAudioClipIndex], position, volume);
    } else {
      PlaySound(audioClipArray[index], position, volume);
      lastJumpAudioClipIndex = index;
    }
  }

  private void PlaySound(AudioClip audioClip, Vector3 position, float volumeMultiplier = 1f) {
    AudioSource.PlayClipAtPoint(audioClip, position, volumeMultiplier);
  }
}