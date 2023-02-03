using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
public class BoostSpawner : MonoBehaviour
{
    [SerializeField] private GameObject boostPrefab;
    [SerializeField] private float spawnDelay = 4f;
    [SerializeField] private float spawnRadius = 10f;
    [SerializeField] private float spawnHeight = 0.5f;
    private float spawnTimer = 0f;
    void Update()
    {
        // If the game is not playing, return
        if (GameManager.Instance.CurrentGameState != GameManager.GameState.Playing) return;

        // Spawn a boost every spawnDelay seconds
        spawnTimer += Time.deltaTime;
        if (spawnTimer >= spawnDelay)
        {
            spawnTimer = 0f;
            SpawnBoost();
        }
    }

    #region Spawn Boost

    /// <summary>
    /// Spawns a boost at a random position within the spawnRadius
    /// </summary>
    void SpawnBoost()
    {
        // The boost will spawn at a random position within the spawnRadius
        Vector3 spawnPos = transform.position + transform.forward * spawnRadius;
        spawnPos = Random.insideUnitSphere * spawnRadius;

        // Make sure the boost spawns on the navmesh
        NavMeshHit hit;
        if (NavMesh.SamplePosition(spawnPos, out hit, spawnRadius, NavMesh.AllAreas))
        {
            spawnPos = hit.position;
        }

        // Spawn the boost
        GameObject boost = Instantiate(boostPrefab, spawnPos, boostPrefab.transform.rotation);

        // Set the boost's height to spawnHeight
        boost.transform.position = new Vector3(boost.transform.position.x, spawnHeight, boost.transform.position.z);
    }

    #endregion
}
