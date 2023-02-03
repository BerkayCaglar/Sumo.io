using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator)),
RequireComponent(typeof(EnemyAnimationController)), RequireComponent(typeof(ParticleEffectController)),
RequireComponent(typeof(NavMeshAgent)), RequireComponent(typeof(EnemyCollisionController)), RequireComponent(typeof(EnemyAI))]
public class Enemy : MonoBehaviour
{
    #region Private Variables

    private Rigidbody m_rigidBody;
    private Animator m_animator;
    private EnemyAnimationController m_enemyAnimationController;
    private ParticleEffectController m_particleEffectController;
    private WorldSpaceAnimationController m_worldSpaceAnimationController;
    private NavMeshAgent m_navMeshAgent;
    private float m_stopDistance = 0.1f, m_pushForce = 6f, m_criticalPushMultiplier = 1.5f, m_detectionRadius = 40f;
    [SerializeField] private LayerMask m_detectableLayerMask;

    #endregion

    #region Setters and Getters (Encapsulation)

    public Rigidbody RigidBody { get { return m_rigidBody; } }
    public Animator Animator { get { return m_animator; } }
    public EnemyAnimationController EnemyAnimationController { get { return m_enemyAnimationController; } }
    public ParticleEffectController ParticleEffectController { get { return m_particleEffectController; } }
    public WorldSpaceAnimationController WorldSpaceAnimationController { get { return m_worldSpaceAnimationController; } }
    public NavMeshAgent NavMeshAgent { get { return m_navMeshAgent; } }
    public float StopDistance { get { return m_stopDistance; } }
    public float DetectionRadius { get { return m_detectionRadius; } }
    public LayerMask DetectableLayerMask { get { return m_detectableLayerMask; } }
    public float CriticalPushMultiplier { get { return m_criticalPushMultiplier; } }
    public float PushForce
    {
        get { return m_pushForce; }
        set
        {
            // Clamp the value between 0 and 20.
            m_pushForce = Mathf.Clamp(value, 0, 20);
        }
    }

    #endregion
    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_enemyAnimationController = GetComponent<EnemyAnimationController>();
        m_particleEffectController = GetComponent<ParticleEffectController>();
        m_navMeshAgent = GetComponent<NavMeshAgent>();
        m_worldSpaceAnimationController = GetComponentInChildren<WorldSpaceAnimationController>();
    }
}
