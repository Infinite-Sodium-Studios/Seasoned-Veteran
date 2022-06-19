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
        Quaternion rotation = boxCollider.transform.rotation;
        Vector3 forwardDirection = new Vector3(size.x, 0, 0);
        direction = rotation * forwardDirection;
        location = position + rotation * size;
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

    public GameObject Respawn(SpawnMotion motion)
    {
        var enemyIndex = Random.Range(0, enemyPrefabs.Length);
        return Respawn(enemyIndex, motion);
    }

    public GameObject Respawn(int enemyIndex, SpawnMotion motion)
    {
        UnityEngine.Debug.Assert(enemyIndex >= 0 && enemyIndex < enemyPrefabs.Length);
        var enemyPrefab = enemyPrefabs[enemyIndex];
        var enemy = Instantiate(enemyPrefab, motion.location, new Quaternion());
        var movement = enemy.GetComponent<EnemyMovement>();
        movement.Init(player);
        var typeManager = enemy.GetComponent<EnemyTypeManager>();
        UnityEngine.Debug.Assert(typeManager != null);
        typeManager.SetEnemyType(enemyIndex);
        return enemy;
    }
}
