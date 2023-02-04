using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCollisionController : MonoBehaviour
{
    private Enemy m_enemy;
    private void Start()
    {
        m_enemy = GetComponent<Enemy>();
    }

    #region Collision And Trigger Methods

    private void OnCollisionEnter(Collision collision)
    {
        // If the collision is with the player or enemy, prepare for impact and perform impact
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            PrepareForImpact();
            PerformImpact(collision);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the collision is with the boost, perform boost
        if (other.gameObject.CompareTag("Boost"))
        {
            PerformBoost(other);
        }
        // If the collision is with the death zone, destroy the enemy object
        else if (other.gameObject.CompareTag("Death Zone"))
        {
            // Decrease the players count in UI Manager
            GameManager.Instance.DecreasePlayersCountInUIManager();

            // Adds the powers of the deceased to the powers of the Killing Person.
            KilledBySomeone();

            // Destroy the enemy object
            Destroy(gameObject);
        }
    }

    #endregion


    #region Perform Methods

    /// <summary>
    /// Disable the nav mesh agent of the enemy object. Because the enemy object will be pushed by the player or enemy object.
    /// </summary>
    private void PrepareForImpact()
    {
        m_enemy.NavMeshAgent.enabled = false;
    }

    /// <summary>
    /// Play attack animation, play explosion particle, get the direction of the collision and add force to the player object.
    /// </summary>
    /// <param name="collision"></param>
    private void PerformImpact(Collision collision)
    {
        // Set the last hit
        m_enemy.LastHitToMe = collision.gameObject;

        // Get the direction of the collision and add force to the player object
        GameManager.Instance.CalculateTheDirectionOfHitAndPush(collision, gameObject, m_enemy: this.m_enemy);

        // Check the hit from front or back. If the hit is from the back, return.
        if (GameManager.Instance.CheckTheHitCameFromFrontOrBack(collision, transform))
        {
            // Play defend animation
            m_enemy.EnemyAnimationController.PlayDefendAnimation();
            return;
        }

        // Play attack animation
        m_enemy.EnemyAnimationController.PlayAttackAnimation();

        // Play explosion particle
        m_enemy.ParticleEffectController.PlayExplosionParticle();
    }
    /// <summary>
    /// Increase the push force, increase the scale and increase the mass of the enemy object.
    /// </summary>
    /// <param name="other"></param>
    private void PerformBoost(Collider other)
    {
        // Increase the push force, increase the scale and increase the mass of the object
        m_enemy.PushForce += 4f;
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        m_enemy.RigidBody.mass += 0.5f;

        // Destroy the boost object
        Destroy(other.gameObject);
    }
    /// <summary>
    /// If the enemy object is killed by the player or enemy object, increase the push force, increase the scale and increase the mass of the player or enemy object.
    /// </summary>
    private void KilledBySomeone()
    {
        if (m_enemy.LastHitToMe != null)
        {
            if (m_enemy.LastHitToMe.CompareTag("Player"))
            {
                Player player = m_enemy.LastHitToMe.GetComponent<Player>();

                // Increase the score of the player
                player.PushForce += 4f;
                player.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                player.RigidBody.mass += 0.5f;
            }
            else if (m_enemy.LastHitToMe.CompareTag("Enemy"))
            {
                Enemy enemy = m_enemy.LastHitToMe.GetComponent<Enemy>();

                // Increase the score of the enemy
                enemy.PushForce += 4f;
                enemy.transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
                enemy.RigidBody.mass += 0.5f;
            }
        }
    }

    #endregion
}
