using UnityEngine;
using System.Diagnostics;

struct SpawnMotion
{
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
    public GameObject[] enemyPrefabs;
    private Stopwatch stopwatch;
    private GameObject player;
    private GameObject[] respawnPoints;
    private long msSinceLastSpawn;
    readonly private long respawnFrequencyMs = 2_000;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        stopwatch = new Stopwatch();
        msSinceLastSpawn = 0;
    }

    void FixedUpdate()
    {
        msSinceLastSpawn += stopwatch.ElapsedMilliseconds;
        stopwatch.Reset();
        stopwatch.Start();

        if (msSinceLastSpawn >= respawnFrequencyMs)
        {
            var spawnIndex = Random.Range(0, respawnPoints.Length);
            var respawnPoint = respawnPoints[spawnIndex];
            var spawnedEnemy = Respawn(new SpawnMotion(respawnPoint.GetComponent<BoxCollider>()));
            msSinceLastSpawn = 0;
        }
    }

    GameObject Respawn(SpawnMotion motion)
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[enemyIndex];
        var enemy = Instantiate(enemyPrefab, motion.location, new Quaternion());
        var movement = enemy.GetComponent<EnemyMovement>();
        movement.Init(player);
        return enemy;
    }
}
