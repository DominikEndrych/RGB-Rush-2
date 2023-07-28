using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public bool SpawnEnemies = true;

    [SerializeField] private GameObject _enemyPrefab;

    [Header("Boundaries")]
    [SerializeField] private Transform _leftBoundary;
    [SerializeField] private Transform _rightBoundary;

    [Header("Wait time and speed")]
    [SerializeField] private float _minWaitTime;
    [SerializeField] private float _maxWaitTime;
    [SerializeField] private float _waitTimeDecrease;
    [SerializeField] private float _minSpeedBoost;
    [SerializeField] private float _maxSpeedBoost;

    [Header("Other")]
    [SerializeField] private GameController _gameController;


    public void Stop()
    {
        SpawnEnemies = false;
    }

    public void Run()
    {
        SpawnEnemies = true;
        StartCoroutine(SpawRoutine());  // Start spawning routine
    }

    IEnumerator SpawRoutine()
    {
        // There might be more enemies in the future, but right now just one

        while(SpawnEnemies)
        {
            Debug.Log("Routine still run");
            GameObject newEnemy = Spawn(_enemyPrefab);                              // Spawn the enemy
            float speedBoost = RandomSpeedBoost() * (float)_gameController.Round;   // Speed boost to set
            newEnemy.GetComponent<Enemy>().SetSpeedBoost(speedBoost);               // Set speed boost
            _gameController.AddEnemy(newEnemy);                                     // Add new enemy to collection

            float waitTime = Random.Range(_minWaitTime, _maxWaitTime);              // Get random time to wait

            // Decrease wait time based on round number so enemies spawn faster
            if(_gameController.Round > 1)
            {
                waitTime -= _waitTimeDecrease;
            }

            yield return new WaitForSeconds(waitTime);
        }
    }

    private GameObject Spawn(GameObject prefab)
    {
        // Take X position of new Enemy as random position between boundaries
        // Y position is always the same

        float position_x = Random.Range(_leftBoundary.transform.position.x, _rightBoundary.transform.position.x);   // X position of new enemy
        Vector2 position = new Vector2(position_x, _leftBoundary.transform.position.y);                             // Final position

        return GameObject.Instantiate(prefab, position, Quaternion.identity);      // Create instance
    }
    private float RandomSpeedBoost()
    {
        return Random.Range(_minSpeedBoost, _maxSpeedBoost);
    }
}
