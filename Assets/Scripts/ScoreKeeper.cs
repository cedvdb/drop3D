using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScoreKeeper : MonoBehaviour
{
  [SerializeField] TileConfig tileConfig;
  [HideInInspector]
  public static int score = 0;
  private int beginningOffset = 10;
  public delegate void ScoreChangeEventHandler(int score);
  public event ScoreChangeEventHandler ScoreChanged;

  void Start()
  {
    StartCoroutine(DoCheck());
  }

  IEnumerator DoCheck()
  {
    while (true)
    {
      ComputeScore();
      yield return new WaitForSeconds(.1f);
    }
  }

  void ComputeScore()
  {
    int currentY = (int)transform.position.y;
    int newScore = (int)((-currentY + beginningOffset) / tileConfig.floorHeight);
    bool hasChanged = newScore != score;
    score = newScore;
    if (hasChanged)
    {
      OnScoreChanged();
    }
  }

  protected virtual void OnScoreChanged()
  {
    if (ScoreChanged != null)
    {
      ScoreChanged(score);
    }
  }
}
