using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneController : MonoBehaviour
{
    public static SceneController Instance { get; private set; }
    private void Awake()
    {
        // If there is no instance of this class, set it to this
        if (Instance == null)
        {
            // Set the instance to this
            Instance = this;
        }
    }
    /// <summary>
    /// Loads the scene with the given name
    /// </summary>
    /// <param name="sceneName"></param>
    public void LoadSceneWithName(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    /// <summary>
    /// Loads the scene with the given index
    /// </summary>
    /// <param name="sceneIndex"></param>
    public void LoadSceneWithIndex(int sceneIndex)
    {
        SceneManager.LoadScene(sceneIndex);
    }
    /// <summary>
    /// Loads the next scene in the build index
    /// </summary>
    public void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    /// <summary>
    /// Loads the previous scene in the build index
    /// </summary>
    public void LoadPreviousScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
    /// <summary>
    /// Reloads the current scene
    /// </summary>
    public void ReloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    /// <summary>
    /// Quits the game
    /// </summary>
    public void QuitGame()
    {

#if UNITY_EDITOR // If we are running in the editor

        // Stop playing the scene
        UnityEditor.EditorApplication.isPlaying = false;

#else // Otherwise...

        // Quit the application
        Application.Quit();

#endif // End of editor test

    }
}