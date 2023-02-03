using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class EnemyAI : MonoBehaviour
{
    private Enemy m_enemy;
    private GameObject target;
    private Dictionary<float, GameObject> m_distanceAndTarget = new Dictionary<float, GameObject>();
    private void Start()
    {
        m_enemy = GetComponent<Enemy>();

        // Check around every second
        InvokeRepeating("CheckAround", 0f, 0.5f);

        // Decide what to do every half second
        InvokeRepeating("DecideWhatToDo", 0f, 0.2f);
    }
    private void FixedUpdate()
    {
        // If the game is not playing, return
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        Movement();
        CheckNavmeshEnabledAndVelocity();
    }
    /// <summary>
    /// Enemy checks around for any targets or boosts. If there are any, it will add them to a dictionary with the distance as the key.
    /// </summary>
    private void CheckAround()
    {
        // If the game is not playing, return
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        // Clear the dictionary
        m_distanceAndTarget.Clear();

        // Check for any targets
        Collider[] colliders = Physics.OverlapSphere(transform.position, m_enemy.DetectionRadius, m_enemy.DetectableLayerMask);
        foreach (Collider collider in colliders)
        {
            // If the collider is the enemy itself, continue
            if (collider.gameObject == this.gameObject) continue;

            // Add the target to the dictionary
            m_distanceAndTarget.Add(Vector3.Distance(transform.position, collider.transform.position), collider.gameObject);
        }
    }
    /// <summary>
    /// Enemy decides what to do based on the dictionary. If there are any targets, it will choose the nearest one.
    /// </summary>
    private void DecideWhatToDo()
    {
        // If the game is not playing, return
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        // If there are any targets
        if (m_distanceAndTarget.Count > 0)
        {
            // Choose the nearest target
            float nearestDistance = float.MaxValue;
            GameObject nearestTarget = null;
            foreach (KeyValuePair<float, GameObject> distanceAndTarget in m_distanceAndTarget)
            {
                // If the distance is less than the nearest distance
                if (distanceAndTarget.Key < nearestDistance)
                {
                    // Set the nearest distance and target
                    nearestDistance = distanceAndTarget.Key;
                    nearestTarget = distanceAndTarget.Value;
                }
            }
            // Set the target to the nearest target
            target = nearestTarget;
        }
        // If there are no targets
        else
        {
            // Set the target to null
            target = null;
        }
    }
    /// <summary>
    /// Enemy moves towards the target.
    /// </summary>
    private void Movement()
    {
        // If the enemy has a navmesh agent and a target
        if (m_enemy.NavMeshAgent.enabled && target != null)
        {
            // Move towards the target
            m_enemy.NavMeshAgent.SetDestination(target.transform.position);
            m_enemy.RigidBody.velocity = m_enemy.NavMeshAgent.velocity;
        }
    }
    /// <summary>
    /// Check navmesh enabled and velocity.
    /// </summary>
    private void CheckNavmeshEnabledAndVelocity()
    {
        // If the enemy has a navmesh agent and the navmesh agent is disabled and the enemy's velocity is less than the stop distance
        if (m_enemy.NavMeshAgent != null)
        {
            if (m_enemy.NavMeshAgent.enabled == false && m_enemy.RigidBody.velocity.magnitude < m_enemy.StopDistance)
            {
                // Enable the navmesh agent
                EnableNavMeshAgent();
            }
        }
    }
    /// <summary>
    /// Enable navmesh agent.
    /// </summary>
    private void EnableNavMeshAgent()
    {
        m_enemy.NavMeshAgent.enabled = true;
    }
}
