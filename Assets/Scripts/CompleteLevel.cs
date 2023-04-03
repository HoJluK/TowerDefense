using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CompleteLevel : MonoBehaviour
{
    public string menuScene = "MainMenu";
    public SceneFader sceneFader;
    public string nextLevel = "Level02";
    public int LevelToUnlock = 2;

    public void Continue()
    {
        PlayerPrefs.SetInt("levelReached", LevelToUnlock);
        sceneFader.FadeTo(nextLevel);
    }

    public void Menu()
    {
        sceneFader.FadeTo(menuScene);
    }

}
