using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentRotater : MonoBehaviour
{
    private float m_rotationSpeed = 10f;
    private void Update()
    {
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;
        // Rotate the environment around the Y axis
        transform.Rotate(Vector3.up, m_rotationSpeed * Time.deltaTime);
    }
}
