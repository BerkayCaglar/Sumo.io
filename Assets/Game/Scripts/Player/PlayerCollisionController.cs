using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollisionController : MonoBehaviour
{
    private Player m_player;
    private void Start()
    {
        m_player = GetComponent<Player>();
    }

    #region Collision And Trigger Methods

    private void OnCollisionEnter(Collision collision)
    {
        // If the collision is with the enemy, perform impact.
        if (collision.gameObject.tag == "Enemy")
        {
            PerformImpact(collision);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        // If the trigger is with the boost, perform boost.
        if (other.gameObject.CompareTag("Boost"))
        {
            PerformBoost(other);
        }
        // If the trigger is with the death zone, destroy the player object and set the game state to game over.
        else if (other.gameObject.CompareTag("Death Zone"))
        {
            // Prepare restart the game
            UIManager.Instance.PrepareRestart();

            // Destroy the player object
            Destroy(gameObject);
        }
    }

    #endregion


    #region Perform Methods

    /// <summary>
    /// True the impact flag, play attack animation, play explosion particle, get the direction of the collision and add force to the enemy object.
    /// </summary>
    /// <param name="collision"></param>
    private void PerformImpact(Collision collision)
    {
        // True the impact flag
        m_player.IsOnImpact = true;

        // Get the direction of the collision and add force to the player object
        GameManager.Instance.CalculateTheDirectionOfHitAndPush(collision, gameObject, m_player: this.m_player);

        // Check the hit from front or back. If the hit is from the back, return.
        if (GameManager.Instance.CheckTheHitCameFromFrontOrBack(collision, transform))
        {
            // Play defend animation
            m_player.PlayerAnimationController.PlayDefendAnimation();
            return;
        }

        // Play attack animation
        m_player.PlayerAnimationController.PlayAttackAnimation();

        // Play explosion particle
        m_player.ParticleEffectController.PlayExplosionParticle();
    }

    /// <summary>
    /// Increase the push force, increase the scale and increase the mass of the player object.
    /// </summary>
    /// <param name="other"></param>
    private void PerformBoost(Collider other)
    {
        // Increase the push force, increase the scale and increase the mass of the object
        m_player.PushForce += 4f;
        transform.localScale += new Vector3(0.2f, 0.2f, 0.2f);
        m_player.Speed += 2f;
        m_player.RigidBody.mass += 0.5f;

        // Destroy the boost object
        Destroy(other.gameObject);
    }

    #endregion
}