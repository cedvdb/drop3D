
using UnityEngine;
using UnityEngine.UIElements;

public class GameUI : MonoBehaviour
{

  [SerializeField] GameObject player;
  ScoreKeeper scoreKeeper;
  VisualElement root;

  void OnEnable()
  {
    root = GetComponent<UIDocument>().rootVisualElement;
    scoreKeeper = player.GetComponent<ScoreKeeper>();
    scoreKeeper.ScoreChanged += OnScoreChanged;
  }

  void OnScoreChanged(int score)
  {
    root.Q<Label>("score-label").text = $"Score: {score}";
  }
}
