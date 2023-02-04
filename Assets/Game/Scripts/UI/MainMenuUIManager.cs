using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuUIManager : MonoBehaviour
{
    /// <summary>
    ///  Loads the game scene
    /// </summary>
    public void PlayButton()
    {
        SceneController.Instance.LoadSceneWithName("Game");
    }
    /// <summary>
    /// Loads the main menu scene
    /// </summary>
    public void QuitButton()
    {
        SceneController.Instance.QuitGame();
    }
}
