using UnityEngine;
using System.Diagnostics;

public struct SpawnMotion
{
    public SpawnMotion(Vector3 _location, Vector3 _direction)
    {
        location = _location;
        direction = _direction;
    }
    public SpawnMotion(BoxCollider boxCollider)
    {
        Vector3 position = boxCollider.transform.position;
        Vector3 size = boxCollider.size;
        Vector3 orientation = boxCollider.transform.right;
        direction = orientation;
        location = position + orientation * size.magnitude;
    }
    public Vector3 location;
    public Vector3 direction;
}

public class EnemySpawning : MonoBehaviour
{
    private Stopwatch stopwatch;
    private GameObject player;
    private long msSinceLastSpawn;
    private int remainingEnemiesForWave;
    private int spawnPointIndexForWave;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] respawnPoints;
    [SerializeField] private GameObject[] targetPoints;
    [SerializeField] private long respawnFrequencyMs;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        stopwatch = new Stopwatch();
        msSinceLastSpawn = 0;
        remainingEnemiesForWave = 0;
        spawnPointIndexForWave = 0;
    }

    void FixedUpdate()
    {
        msSinceLastSpawn += stopwatch.ElapsedMilliseconds;
        stopwatch.Reset();
        stopwatch.Start();

        if (msSinceLastSpawn >= respawnFrequencyMs)
        {
            var spawnIndex = GetSpawnPointIndexForWave();
            var spawnedEnemy = Respawn(spawnIndex);
            msSinceLastSpawn = 0;
        }
    }

    private static int GetNewSpawnPoint(int numRespawnPoints, int previousSpawnPointIndex) {
        var newSpawnPointIndex = previousSpawnPointIndex;
        while (newSpawnPointIndex == previousSpawnPointIndex && numRespawnPoints > 1) {
            newSpawnPointIndex = Random.Range(0, numRespawnPoints);
        }
        return newSpawnPointIndex;
    }

    private int GetSpawnPointIndexForWave() {
        if (remainingEnemiesForWave <= 0) {
            spawnPointIndexForWave = GetNewSpawnPoint(respawnPoints.Length, spawnPointIndexForWave);
            remainingEnemiesForWave = Random.Range(3, 5);
        }
        --remainingEnemiesForWave;
        return spawnPointIndexForWave;
    }

    private GameObject Respawn(int spawnIndex)
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[enemyIndex];
        return Respawn(enemyPrefab, spawnIndex);
    }

    private GameObject Respawn(GameObject baseEnemyObject, int spawnIndex)
    {
        var motion = new SpawnMotion(respawnPoints[spawnIndex].GetComponent<BoxCollider>());
        var enemy = Instantiate(baseEnemyObject, motion.location, new Quaternion());
        var movement = enemy.GetComponent<EnemyMovement>();
        var targetIndex = spawnIndex % targetPoints.Length;
        var target = targetPoints[targetIndex];
        movement.Init(target);
        var enemyTypeManager = enemy.GetComponent<EnemyTypeManager>();
        enemyTypeManager.Init(baseEnemyObject);
        return enemy;
    }
}
