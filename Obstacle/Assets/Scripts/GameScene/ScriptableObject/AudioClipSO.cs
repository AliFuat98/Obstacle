using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu()]
public class AudioClipSO : ScriptableObject {
  public AudioClip[] jump;
  public AudioClip[] collect;
  public AudioClip[] takeDamage;
  public AudioClip[] Death;
}
