using System;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWaveManager : MonoBehaviour
{
    // Define the possible states for the wave manager
    public enum State
    {
        WaitingForNextWave,
        SpawningWave
    }
    public static event Action<State> OnStateChanged;

    [Header("Wave Configuration")]
    [SerializeField] private float waveInterval = 10f;
    [SerializeField] private int intialEnemiesPerWave = 5;
    [SerializeField] private float initialDelay = 4f;

    [Header("Spawning Configuration")]
    [SerializeField] private List<Transform> spawnPositionTranform;
    [SerializeField] private float minSpawnInterval = 0.1f;
    [SerializeField] private float maxSpawnInterval = 0.4f;
    [SerializeField] private Transform nextWaveSpawnPosition;

    private State currentState;
    private float waveTimer;
    private int waveCount;
    private float nextEnemySpawnTimer;
    private int remainingEnemiesToSpawn;
    private Transform currentWaveSpawnTransform; // Stores the spawn point for the current wave

    private void Start()
    {
        // Set the initial state and timer for the first wave
        currentState = State.WaitingForNextWave;
        waveTimer = initialDelay;
        if (spawnPositionTranform != null && spawnPositionTranform.Count > 0)
        {
            currentWaveSpawnTransform = spawnPositionTranform[UnityEngine.Random.Range(0, spawnPositionTranform.Count)];
            nextWaveSpawnPosition.position = currentWaveSpawnTransform.position;
        }
    }

    private void Update()
    {
        // The switch statement acts as our state machine
        switch (currentState)
        {
            case State.WaitingForNextWave:
                HandleWaitingState();
                break;

            case State.SpawningWave:
                HandleSpawningState();
                break;
        }
        //Debug.Log($"Wave: {waveCount}, Timer: {waveTimer:F1}");
    }
    private void SetState(State newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        OnStateChanged?.Invoke(currentState);
    }
    private void HandleWaitingState()
    {
        waveTimer -= Time.deltaTime;
        if (waveTimer <= 0f)
        {
            // Timer is up, begin the spawning phase
            StartNewWave();
            SetState(State.SpawningWave); // Transition to the next state
        }
    }

    private void HandleSpawningState()
    {
        nextEnemySpawnTimer -= Time.deltaTime;
        if (nextEnemySpawnTimer <= 0f)
        {
            SpawnSingleEnemy();

            // If all enemies for the wave have been spawned, transition back to waiting
            if (remainingEnemiesToSpawn <= 0)
            {
                SetState(State.WaitingForNextWave);
            }
        }
    }

    private void StartNewWave()
    {
        remainingEnemiesToSpawn = intialEnemiesPerWave + (waveCount * 2);
        waveTimer = waveInterval; // Reset the timer for the *next* wave

        // Choose a single random spawn point for the entire upcoming wave
        currentWaveSpawnTransform = spawnPositionTranform[UnityEngine.Random.Range(0, spawnPositionTranform.Count)];
        nextWaveSpawnPosition.position = currentWaveSpawnTransform.position;
        waveCount++;
    }

    private void SpawnSingleEnemy()
    {
        // Reset the timer for the next individual enemy spawn
        nextEnemySpawnTimer = UnityEngine.Random.Range(minSpawnInterval, maxSpawnInterval);

        // Spawn the enemy at a random position around the wave's chosen spawn point
        Vector3 randomOffset = UtilsClass.GetRandomDirection() * UnityEngine.Random.Range(0f, 10f);
        Enemy.CreateEnemy(currentWaveSpawnTransform.position + randomOffset);

        remainingEnemiesToSpawn--;
    }
    public float GetNextWaveTimer()
    {
        return waveTimer;
    }
    public int GetWaveCount()
    {
        return waveCount;
    }
    public Vector3 GetCurrentWaveSpawnTransform()
    {
        return currentWaveSpawnTransform.position;
    }
}