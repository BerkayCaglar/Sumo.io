using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRotater : MonoBehaviour
{
    private float m_rotationSpeed = 10f;
    private void Update()
    {
        // Rotate the environment around the Y axis
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}
