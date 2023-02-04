using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
public class SpawnManager : MonoBehaviour
{
    private GameObject[] m_spawnPoints;
    [SerializeField] private List<GameObject> m_spawnObjects = new List<GameObject>();
    private bool m_playerRandomlySelected;
    private void Awake()
    {
        // Get all the spawn points
        m_spawnPoints = GameObject.FindGameObjectsWithTag("Spawn Point");
    }

    private void Start()
    {
        // Start the spawn coroutine
        StartCoroutine(SpawnObjects());
    }

    /// <summary>
    /// Spawn objects at the spawn points
    /// </summary>
    /// <returns></returns>
    private IEnumerator SpawnObjects()
    {
        foreach (GameObject spawnPoint in m_spawnPoints)
        {
            // Increase the players count in the UI manager
            GameManager.Instance.IncreasePlayersCountInUIManager();

            // Get a random object to spawn
            GameObject spawnObject;
            spawnObject = m_spawnObjects[Random.Range(0, m_spawnObjects.Count)];

            // Check if the object is the player
            if (spawnObject.CompareTag("Player"))
            {
                // Check if the player is already randomly selected
                if (m_playerRandomlySelected)
                {
                    // Remove the player from the spawn objects list
                    m_spawnObjects.Remove(spawnObject);

                    // Get a random object to spawn
                    spawnObject = m_spawnObjects[Random.Range(0, m_spawnObjects.Count)];

                    // Spawn the player at the random spawn point
                    Instantiate(spawnObject, spawnPoint.transform.position, Quaternion.identity);

                    // Wait for 0.2 seconds
                    yield return new WaitForSeconds(0.2f);

                    // Continue to the next iteration
                    continue;
                };
                // Spawn the player at the random spawn point
                GameObject spawnedPlayerObject = Instantiate(spawnObject, spawnPoint.transform.position, Quaternion.identity);

                // Set the player randomly selected to true
                m_playerRandomlySelected = true;

                // Remove the player from the spawn objects list
                m_spawnObjects.Remove(spawnObject);

                GameManager.Instance.Player = spawnedPlayerObject;
                // Wait for 0.2 seconds
                yield return new WaitForSeconds(0.2f);

                // Continue to the next iteration
                continue;
            }

            // Spawn the object
            Instantiate(spawnObject, spawnPoint.transform.position, Quaternion.identity);

            // Wait for 0.2 seconds
            yield return new WaitForSeconds(0.2f);
        }
    }
}
