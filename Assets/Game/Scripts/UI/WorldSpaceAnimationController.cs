using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class WorldSpaceAnimationController : MonoBehaviour
{
    private Animator m_animator;
    private void Start()
    {
        m_animator = GetComponent<Animator>();
    }
    private void Update()
    {
        // The critical hit canvas object is constantly looking at the camera.
        transform.LookAt(Camera.main.transform);
    }

    #region Animation Methods

    /// <summary>
    /// Play the critical hit animation
    /// </summary>
    public void PlayCriticalHitAnimation()
    {
        m_animator.SetTrigger("Hit");
    }

    #endregion
}
