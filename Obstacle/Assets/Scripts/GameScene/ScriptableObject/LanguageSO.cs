using UnityEngine;

[CreateAssetMenu()]
public class LanguageSO : ScriptableObject {
  [TextArea(4, 10)]
  public string Tr;
  [TextArea(4, 10)]
  public string En;
}