using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random; 

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawner Configuration")]
    [SerializeField] private GameObject enemyPrefab; 
    [SerializeField] private float spawnInterval = 2f;  //Tiempo entre spawns
    [SerializeField] private int maxEnemiesAlive = 5;   //Maxima cantidad de enemigos vivos
    
    [Header("Spawn Area")]
    [SerializeField] private Transform[] spawnPoints;   //Puntos de spawn

    private int currentEnemiesAlive = 0;

    void Start()
    {
        StartCoroutine(SpawnRoutine());    
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentEnemiesAlive < maxEnemiesAlive)
            {
                SpawnEnemy();
            }
            yield return new WaitForSeconds(spawnInterval);
        }
    }

    private void SpawnEnemy()
    {
        //Seleccionar enemigo y puntos de spawn
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        currentEnemiesAlive++;

        EnemyBase enemyBase = newEnemy.GetComponent<EnemyBase>();
        if (enemyBase != null)
        {
            enemyBase.OnEnemyDeath += HandleEnemyDeath;
        }
    }

    private void HandleEnemyDeath(EnemyBase enemy)
    {
        currentEnemiesAlive--;

        enemy.OnEnemyDeath -= HandleEnemyDeath;
    }
}
