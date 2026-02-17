using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random; 

public class EnemySpawner : MonoBehaviour
{
    [Header("Scripts")]
    [SerializeField] private GameManager gameManager;   //Asignar en el inspector

    [Header("Spawner Configuration")]
    [SerializeField] private GameObject enemyPrefab;

    [SerializeField] private float baseSpawnInterval = 5f;
    [SerializeField] private float minSpawnInterval = 2f;

    private float currentSpawnInterval;  //Tiempo entre spawns
    [SerializeField] private int maxEnemiesAlive = 5;   //Maxima cantidad de enemigos vivos
    
    [Header("Spawn Area")]
    [SerializeField] private Transform[] spawnPoints;   //Puntos de spawn

    private int currentEnemiesAlive = 0;

    private void OnEnable()
    {
        if (gameManager != null)
            gameManager.OnInfectionChanged += HandleInfectionChanged;
    }

    private void Start()
    {
        currentSpawnInterval = baseSpawnInterval;
        HandleInfectionChanged(gameManager.Infection01);
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            if (currentEnemiesAlive < maxEnemiesAlive)
            {
                SpawnEnemy();
                //Debug.Log("Spawn Interval: " + currentSpawnInterval);

            }
            yield return new WaitForSeconds(currentSpawnInterval);
        }
    }

    private void OnDisable()
    {
        if (gameManager != null)
            gameManager.OnInfectionChanged -= HandleInfectionChanged;
    }

    private void HandleInfectionChanged(float infection01)
    {
        float evaluated = gameManager.InfectionEvaluated;
        currentSpawnInterval = Mathf.Lerp(baseSpawnInterval, minSpawnInterval, evaluated);
    }

    private void SpawnEnemy()
    {
        //Seleccionar enemigo y puntos de spawn
        Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject newEnemy = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        currentEnemiesAlive++;

        EnemyBase enemyBase = newEnemy.GetComponent<EnemyBase>();
        enemyBase.Initialize(gameManager);
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
