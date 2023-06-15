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
    public GameObject[] enemyPrefabs;
    private Stopwatch stopwatch;
    private GameObject player;
    private GameObject[] respawnPoints;
    private GameObject[] targetPoints;
    private long msSinceLastSpawn;
    [SerializeField] private long respawnFrequencyMs;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        respawnPoints = GameObject.FindGameObjectsWithTag("Respawn");
        targetPoints = GameObject.FindGameObjectsWithTag("Target");
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

    public GameObject Respawn(SpawnMotion motion)
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[enemyIndex];
        return Respawn(enemyPrefab, motion);
    }

    public GameObject Respawn(GameObject baseEnemyObject, SpawnMotion motion)
    {
        var enemy = Instantiate(baseEnemyObject, motion.location, new Quaternion());
        var movement = enemy.GetComponent<EnemyMovement>();
        var targetIndex = Random.Range(0, targetPoints.Length);
        var target = targetPoints[targetIndex];
        movement.Init(target);
        var enemyTypeManager = enemy.GetComponent<EnemyTypeManager>();
        enemyTypeManager.Init(baseEnemyObject);
        return enemy;
    }
}
