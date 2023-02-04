using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleEffectController : MonoBehaviour
{
    [SerializeField] private ParticleSystem m_explosionParticle;

    /// <summary>
    ///  Play the explosion particle
    /// </summary>
    public void PlayExplosionParticle()
    {
        // Play the particle
        m_explosionParticle.Play();
    }
}
