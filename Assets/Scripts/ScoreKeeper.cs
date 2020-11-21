using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class ScoreKeeper : MonoBehaviour
{
  [HideInInspector]
  public int score = 0;
  int startScoreY = 5;

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
    score = startScoreY + Mathf.Abs(currentY);
  }
}
