using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody)), RequireComponent(typeof(Animator)), RequireComponent(typeof(PlayerAnimationController)), RequireComponent(typeof(ParticleEffectController)), RequireComponent(typeof(PlayerCollisionController)), RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    #region Private Variables

    private FixedJoystick m_fixedJoystick;
    private Canvas m_joystickCanvas;
    private Rigidbody m_rigidBody;
    private Animator m_animator;
    private PlayerAnimationController m_playerAnimationController;
    private ParticleEffectController m_particleEffectController;
    private WorldSpaceAnimationController m_worldSpaceAnimationController;
    private float m_speed = 12f, m_turnSpeed = 3f, m_stopDistance = 0.1f, m_pushForce = 6f, m_criticalPushMultiplier = 1.5f, m_maxSpeed = 3.5f;
    private bool m_isOnImpact;

    #endregion

    #region Setters and Getters (Encapsulation)

    public FixedJoystick FixedJoystick { get { return m_fixedJoystick; } }
    public Canvas JoystickCanvas { get { return m_joystickCanvas; } }
    public Rigidbody RigidBody { get { return m_rigidBody; } }
    public Animator Animator { get { return m_animator; } }
    public PlayerAnimationController PlayerAnimationController { get { return m_playerAnimationController; } }
    public ParticleEffectController ParticleEffectController { get { return m_particleEffectController; } }
    public WorldSpaceAnimationController WorldSpaceAnimationController { get { return m_worldSpaceAnimationController; } }
    public float TurnSpeed { get { return m_turnSpeed; } }
    public float StopDistance { get { return m_stopDistance; } }
    public float MaxSpeed { get { return m_maxSpeed; } }
    public float CriticalPushMultiplier { get { return m_criticalPushMultiplier; } }
    public bool IsOnImpact { get { return m_isOnImpact; } set { m_isOnImpact = value; } }
    public float Speed
    {
        get { return m_speed; }
        // If the speed + value is less than 20, set the speed to the value.
        set { if (m_speed + value <= 20) m_speed = value; }
    }
    public float PushForce
    {
        get { return m_pushForce; }
        // If the push force + value is less than 20, set the push force to the value.
        set { if (m_pushForce + value <= 20) m_pushForce = value; }
    }

    #endregion
    private void Start()
    {
        m_rigidBody = GetComponent<Rigidbody>();
        m_animator = GetComponent<Animator>();
        m_fixedJoystick = FindObjectOfType<FixedJoystick>();
        m_joystickCanvas = GameObject.Find("Joystick Canvas").GetComponent<Canvas>();
        m_playerAnimationController = GetComponent<PlayerAnimationController>();
        m_particleEffectController = GetComponent<ParticleEffectController>();
        m_worldSpaceAnimationController = GetComponentInChildren<WorldSpaceAnimationController>();
    }
}
