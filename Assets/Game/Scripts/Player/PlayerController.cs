using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Player m_player;
    private void Start()
    {
        m_player = GetComponent<Player>();
    }
    private void FixedUpdate()
    {
        // If the game is not playing, return.
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        Movement();
    }
    #region Movement Methods

    /// <summary>
    /// Move the player
    /// </summary>
    private void Movement()
    {
        // if the player is not on impact, move the player
        if (!m_player.IsOnImpact && transform.position.y >= -0.2f)
        {
            // Move the player forward
            Vector3 movement = transform.forward;
            // Add force to the player rigidbody
            m_player.RigidBody.AddForce(movement * m_player.Speed * Time.fixedDeltaTime * m_player.RigidBody.mass, ForceMode.Impulse);

            // Limit the player's speed
            if (m_player.RigidBody.velocity.magnitude > m_player.MaxSpeed)
            {
                m_player.RigidBody.velocity = m_player.RigidBody.velocity.normalized * m_player.MaxSpeed;
            }
        }
        else if (m_player.RigidBody.velocity.magnitude < m_player.StopDistance)
        {
            m_player.IsOnImpact = false;
        }

        // Rotate Player to the direction of the joystick
        if (m_player.FixedJoystick.Horizontal != 0 || m_player.FixedJoystick.Vertical != 0)
        {
            Quaternion desiredRotation = Quaternion.LookRotation(new Vector3((-m_player.FixedJoystick.Horizontal), 0, (-m_player.FixedJoystick.Vertical)));
            transform.rotation = Quaternion.Slerp(transform.rotation, desiredRotation, m_player.TurnSpeed * Time.fixedDeltaTime);
        }
    }

    #endregion
}
