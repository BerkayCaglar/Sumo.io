using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }
    private Animator inGameCanvasAnimator;
    [SerializeField] private GameObject pauseMenu, joystickCanvas, pauseButton, countDownParent, gameOverMenu, playersCountMenu;
    [SerializeField] private TMP_Text countDownText, placementText, playersCountText, remainingTimeText, gameOverTitleText;
    [SerializeField] private Image placementSprite;
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

    /// <summary>
    /// Prepare the restart of the game.
    /// </summary>
    public void PrepareRestart(string gameOverTitle = null)
    {
        // If the remaining time has elapsed, the title is changed.
        if (gameOverTitle != null)
        {
            gameOverTitleText.text = gameOverTitle;
        }
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
        placementText.text = playersCountText.text;

        SetPlacementAwardImage();
    }

    /// <summary>
    ///  Set the placement award image.
    /// </summary>
    private void SetPlacementAwardImage()
    {
        // Get the placement of the player.
        int placement = int.Parse(playersCountText.text);

        // Set the placement award image.
        if (placement == 1)
        {
            placementSprite.sprite = placementSprites[0];
        }
        else if (placement <= 3)
        {
            placementSprite.sprite = placementSprites[1];
        }
        else
        {
            placementSprite.sprite = placementSprites[2];
        }
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

    /// <summary>
    /// Decrease the players count by 1.
    /// </summary>
    public void DecreasePlayersCount()
    {
        // Get the current count and -1 from it.
        int count = int.Parse(playersCountText.text);
        count--;

        // Play the players count animation.
        inGameCanvasAnimator.SetTrigger("Players Count");

        // Set the new count to the text.
        playersCountText.text = count.ToString();

        if (count == 1)
        {
            PrepareRestart("You're The Winner!");
        }
    }

    /// <summary>
    /// Increase the players count by 1.
    /// </summary>
    public void IncreasePlayersCount()
    {
        // Get the current count and +1 from it.
        int count = int.Parse(playersCountText.text);
        count++;

        // Set the new count to the text.
        playersCountText.text = count.ToString();
    }

    #endregion

    #region Remaining Time Methods

    /// <summary>
    /// Update the remaining time text.
    /// </summary>
    /// <param name="remainingTime"></param>
    public void UpdateRemainingTime(int remainingTime)
    {
        // Play the remaining time animation.
        inGameCanvasAnimator.SetTrigger("Remaining Time");
        // Set the remaining time to the text.
        remainingTimeText.text = remainingTime.ToString();
    }

    #endregion
}
