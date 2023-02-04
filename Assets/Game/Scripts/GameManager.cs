using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private GameObject m_player;
    private int m_remainingTime = 80;
    private bool m_isTimeRunning = false;
    private static GameManager m_Instance;
    public static GameManager Instance { get { return m_Instance; } }
    public enum GameState { Paused, Playing, GameOver }
    public GameState CurrentGameState { get; set; }
    public GameObject Player { get { return m_player; } set { m_player = value; } }
    private void Awake()
    {
        if (m_Instance == null)
        {
            m_Instance = this;
        }
    }
    private void Update()
    {
        if (CurrentGameState == GameState.Playing && !m_isTimeRunning)
        {
            m_isTimeRunning = true;
            StartCoroutine(UpdateRemainingTimeInUIManager());
        }
    }

    #region Collision Methods

    /// <summary>
    ///  Calculates the direction of the hit and pushes the object.
    /// </summary>
    /// <param name="collision"></param>
    /// <param name="thisGameObject"></param>
    /// <param name="m_enemy"></param>
    public void CalculateTheDirectionOfHitAndPush(Collision collision, GameObject thisGameObject, Enemy m_enemy = null, Player m_player = null)
    {
        Vector3 pushDirection = ReturnPushDirection(collision.gameObject, thisGameObject);

        // It is determined whether the hit is made from the back or the front of the opponent.
        if (CheckTheHitCameFromFrontOrBack(collision))
        {
            // If this function called by Enemy
            if (m_enemy != null)
            {
                if (collision.transform.tag == "Player")
                {
                    // Play critical hit animation
                    collision.gameObject.GetComponent<Player>().WorldSpaceAnimationController.PlayCriticalHitAnimation();
                }
                else if (collision.transform.tag == "Enemy")
                {
                    // Play critical hit animation
                    collision.gameObject.GetComponent<Enemy>().WorldSpaceAnimationController.PlayCriticalHitAnimation();
                }

                // Add force to the other gameobject
                collision.rigidbody.AddForce((pushDirection) * m_enemy.PushForce * m_enemy.CriticalPushMultiplier, ForceMode.Impulse);
            }
            // If this function called by Player
            else if (m_player != null)
            {
                // Play critical hit animation
                collision.gameObject.GetComponent<Enemy>().WorldSpaceAnimationController.PlayCriticalHitAnimation();

                // Add force to the other gameobject
                collision.rigidbody.AddForce((pushDirection) * m_player.PushForce * m_player.CriticalPushMultiplier, ForceMode.Impulse);
            }
        }
        else
        {
            if (CheckTheHitCameFromFrontOrBack(collision, thisGameObject.transform))
            {
                // If this function called by Enemy
                if (m_enemy != null)
                {
                    // Add force to the other gameobject
                    collision.rigidbody.AddForce(((pushDirection) * m_enemy.PushForce) / (m_enemy.CriticalPushMultiplier * 1.5f), ForceMode.Impulse);
                }
                // If this function called by Player
                else if (m_player != null)
                {
                    // Add force to the other gameobject
                    collision.rigidbody.AddForce(((pushDirection) * m_player.PushForce) / (m_player.CriticalPushMultiplier * 1.5f), ForceMode.Impulse);
                }
            }
            else
            {
                // If this function called by Enemy
                if (m_enemy != null)
                {
                    // Add force to the other gameobject
                    collision.rigidbody.AddForce((pushDirection) * m_enemy.PushForce, ForceMode.Impulse);
                }
                // If this function called by Player
                else if (m_player != null)
                {
                    // Add force to the other gameobject
                    collision.rigidbody.AddForce((pushDirection) * m_player.PushForce, ForceMode.Impulse);
                }
            }

        }
    }
    /// <summary>
    /// Returns the direction of the push.
    /// </summary>
    /// <param name="otherGameObject"> Hit(other) GameObject </param>
    /// <param name="thisGameObject"> Your GameObject </param>
    /// <returns></returns>
    private Vector3 ReturnPushDirection(GameObject otherGameObject, GameObject thisGameObject)
    {
        return (otherGameObject.transform.position - thisGameObject.transform.position).normalized;
    }

    /// <summary>
    /// It is determined whether the hit is made from the back or the front of the opponent.
    /// </summary>
    /// <param name="collision"></param>
    /// <returns></returns>
    public virtual bool CheckTheHitCameFromFrontOrBack(Collision collision, Transform thisTransform = null)
    {
        // The reason why thisTransform value is checked and action is taken accordingly, if thisTransform value is null, operations are done according to "collision.transform.forward" statement. But if it is not null, the operations are performed according to the "thisTransform.forward" statement.

        //The purpose of using these conditions is because I don't want to write a small copy of the same function twice as a method.

        Vector3 forwardDirection = thisTransform == null ? collision.transform.forward : thisTransform.forward;

        Vector3 collisionNormal = collision.contacts[0].normal;
        float dotProduct = Vector3.Dot(forwardDirection, collisionNormal);

        if (thisTransform == null)
        {
            return dotProduct < 0 ? true : false;
        }
        else
        {
            return dotProduct > 0 ? true : false;
        }
    }


    #endregion


    #region Game State Methods

    public void PauseGame()
    {
        CurrentGameState = GameState.Paused;
    }
    public void ResumeGame()
    {
        CurrentGameState = GameState.Playing;
    }
    public void GameOver()
    {
        CurrentGameState = GameState.GameOver;
    }

    #endregion

    #region UI Manager Methods

    /// <summary>
    ///  Used once per player spawn.
    /// </summary>
    public void IncreasePlayersCountInUIManager()
    {
        UIManager.Instance.IncreasePlayersCount();
    }
    /// <summary>
    /// Used once per player death.
    /// </summary>
    public void DecreasePlayersCountInUIManager()
    {
        UIManager.Instance.DecreasePlayersCount();
    }

    private IEnumerator UpdateRemainingTimeInUIManager()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            if (m_remainingTime <= 0)
            {
                UIManager.Instance.PrepareRestart("Time's Up!");
                yield break;
            }
            m_remainingTime--;
            UIManager.Instance.UpdateRemainingTime(m_remainingTime);
        }
    }
    #endregion
}