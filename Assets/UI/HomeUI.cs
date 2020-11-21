using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

[DisallowMultipleComponent]
public class HomeUI : MonoBehaviour
{
  VisualElement root;

  void OnEnable()
  {
    root = GetComponent<UIDocument>().rootVisualElement;
    VisualElement playBtn = root.Q("play-btn");
    playBtn.RegisterCallback<ClickEvent>(_ => SceneLoader.LoadGame());
  }

}
