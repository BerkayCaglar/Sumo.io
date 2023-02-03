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
        m_explosionParticle.Play();
    }
}
