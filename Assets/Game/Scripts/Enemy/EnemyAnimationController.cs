using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnimationController : MonoBehaviour
{
    private Enemy m_enemy;
    private void Start()
    {
        m_enemy = GetComponent<Enemy>();
    }
    private void FixedUpdate()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        MovementAnimation();
    }

    #region Animation Methods

    /// <summary>
    /// Set the speed of the enemy in the animator
    /// </summary>
    private void MovementAnimation()
    {
        m_enemy.Animator.SetFloat("Speed", m_enemy.NavMeshAgent.velocity.magnitude);
    }
    /// <summary>
    /// Play the attack animation
    /// </summary>
    public void PlayAttackAnimation()
    {
        m_enemy.Animator.SetTrigger("Attack");
    }
    /// <summary>
    ///  Play the defend animation
    /// </summary>
    public void PlayDefendAnimation()
    {
        m_enemy.Animator.SetTrigger("Defend");
    }

    #endregion
}
