using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationController : MonoBehaviour
{
    private Player m_player;
    private void Start()
    {
        m_player = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        MovementAnimation();
    }

    #region Animation Methods

    /// <summary>
    /// Set the speed of the player in the animator
    /// </summary>
    private void MovementAnimation()
    {
        m_player.Animator.SetFloat("Speed", m_player.RigidBody.velocity.magnitude);
    }
    /// <summary>
    /// Play the attack animation
    /// </summary>
    public void PlayAttackAnimation()
    {
        m_player.Animator.SetTrigger("Attack");
    }
    /// <summary>
    /// Play the defend animation
    /// </summary>
    public void PlayDefendAnimation()
    {
        m_player.Animator.SetTrigger("Defend");
    }

    #endregion
}
