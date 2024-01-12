using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InformationTranslate : MonoBehaviour {
  [SerializeField] GameObject textFieldsParent;
  [SerializeField] Button TrEnButton;

  [SerializeField] List<LanguageSO> informationList;

  TextMeshProUGUI[] textList;
  bool isEnglish = false;

  private void Awake() {
    TrEnButton.onClick.AddListener(() => {
      UpdateInfoList();
    });
  }

  void Start() {
    textList = textFieldsParent.GetComponentsInChildren<TextMeshProUGUI>();

    UpdateInfoList();
  }

  void UpdateInfoList() {
    isEnglish = !isEnglish;
    var index = 0;
    foreach (var text in textList) {
      text.text = isEnglish ? $"- {informationList[index].En}" : $"- {informationList[index].Tr}";
      index++;
    }
  }
}