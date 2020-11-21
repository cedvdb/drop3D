using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

enum Scene
{
  Game,
  HomeUI,
}

public class SceneLoader
{
  public static void ShowHomeUI()
  {
    Time.timeScale = 0;
    SceneManager.LoadSceneAsync(Scene.HomeUI.ToString(), LoadSceneMode.Additive);
  }

  public static void LoadGame()
  {
    Time.timeScale = 1;
    SceneManager.UnloadSceneAsync(Scene.HomeUI.ToString());
    SceneManager.UnloadSceneAsync(Scene.Game.ToString());
    SceneManager.LoadSceneAsync(Scene.Game.ToString());
  }
}
