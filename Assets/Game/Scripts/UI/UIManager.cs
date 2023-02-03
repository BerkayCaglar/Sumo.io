using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Animator inGameCanvasAnimator;
    [SerializeField] private GameObject pauseMenu, joystickCanvas, pauseButton, countDownParent, gameOverMenu, playersCountMenu;
    [SerializeField] private TMP_Text countDownText, placementText, playersCountText;
    [SerializeField] private Sprite[] placementSprites;
    private int countDown = 3;
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Start()
    {
        SetTimeScaleToOne();

        // Get the animator component of the in game canvas.
        inGameCanvasAnimator = GetComponent<Animator>();

        // Deactivate the pause menu and the count down parent. Because we don't need them at the start of the game.
        pauseMenu.SetActive(false);
        pauseButton.SetActive(false);

        // Set the GameState to Paused.
        GameManager.Instance.PauseGame();

        // Start the count down coroutine.
        StartCoroutine(CountDown());
    }


    #region Button Methods

    /// <summary>
    /// Pause the game and set the time scale to 0
    /// </summary>
    public void PauseButton()
    {
        inGameCanvasAnimator.SetTrigger("Pause");
        pauseButton.SetActive(false);
        joystickCanvas.SetActive(false);
        pauseMenu.SetActive(true);
        SetTimeScaleToZero();
    }
    /// <summary>
    /// Resume the game and set the time scale to 1
    /// </summary>
    public void ResumeButton()
    {
        inGameCanvasAnimator.SetTrigger("Resume");
        pauseButton.SetActive(true);
        joystickCanvas.SetActive(true);
        pauseMenu.SetActive(false);
        SetTimeScaleToOne();
    }

    public void RestartButton()
    {
        // Reload the current scene.
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    #endregion

    #region Prepare Restart Methods

    public void PrepareRestart()
    {
        // Set the game state to game over.
        GameManager.Instance.GameOver();

        // Set time scale to 0
        SetTimeScaleToZero();

        // Set active menus and canvases.
        gameOverMenu.SetActive(true);
        pauseButton.SetActive(false);
        joystickCanvas.SetActive(false);
        pauseMenu.SetActive(false);
        countDownParent.SetActive(false);

        // Set the placement text and image.
        //placementText.text = GameManager.Instance.GetPlacementText();
        //placementText.GetComponent<TMP_Text>().sprite = placementSprites[GameManager.Instance.GetPlacementIndex()];
    }

    #endregion


    #region TimeScale Methods

    /// <summary>
    /// Set the time scale to 0
    /// </summary>
    private void SetTimeScaleToZero()
    {
        Time.timeScale = 0;
    }
    /// <summary>
    /// Set the time scale to 1
    /// </summary>
    private void SetTimeScaleToOne()
    {
        Time.timeScale = 1;
    }

    #endregion


    #region CountDown Methods

    /// <summary>
    /// CountDown Coroutine. Starts the game after 3 seconds.
    /// </summary>
    /// <returns></returns>
    private IEnumerator CountDown()
    {
        // Wait for 1 second before starting the count down
        yield return new WaitForSeconds(1f);
        countDown--;

        // Count down from 3 to 1
        while (countDown > 0)
        {
            // Play the countdown animation
            inGameCanvasAnimator.SetTrigger("Countdown");

            // Set the text to the current count down number and -1 from the count down
            countDownText.text = countDown.ToString();
            countDown--;

            // Wait for 1 second before starting the next count down
            yield return new WaitForSeconds(1f);
        }
        // Play the countdown animation
        inGameCanvasAnimator.SetTrigger("Countdown");

        // Set the text to GO! and start the game
        GameManager.Instance.ResumeGame();
        countDownText.fontSize = 140;
        countDownText.text = "GO!";

        // Wait for 1 second before disabling the count down
        yield return new WaitForSeconds(1f);
        countDownParent.SetActive(false);
        pauseButton.SetActive(true);
    }

    #endregion


    #region Players Count Methods

    public void UpdatePlayersCount(int count)
    {
        playersCountText.text = count.ToString();
    }

    #endregion
}
