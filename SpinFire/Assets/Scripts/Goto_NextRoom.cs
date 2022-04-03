using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Goto_NextRoom : MonoBehaviour
{
    public int currentScene;
    public int maxScene;
    public int index;

    private void Awake()
    {
        Globs.maxScenes = SceneManager.sceneCountInBuildSettings;
        //Debug.Log();
        Globs.currentScene = SceneManager.GetActiveScene().buildIndex;
    }

    public void NextScene()
    {
        //maxScene%++currentScene  ;
        //var t = 5 % 1;
        Globs.currentScene = ++Globs.currentScene % Globs.maxScenes;
        SceneManager.LoadScene(Globs.currentScene);
        Debug.Log(Globs.currentScene);
    }
}
