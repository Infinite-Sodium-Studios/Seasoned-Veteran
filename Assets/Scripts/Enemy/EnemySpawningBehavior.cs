using UnityEngine;

public class EnemySpawningBehavior : MonoBehaviour
{
    private EnemySpawning enemySpawning;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private GameObject[] respawnPoints;
    [SerializeField] private GameObject[] targetPoints;
    [SerializeField] private long respawnFrequencyMs;

    void Awake()
    {
        enemySpawning = new EnemySpawning(respawnFrequencyMs, enemyPrefabs.Length, respawnPoints.Length, targetPoints.Length);
        enemySpawning.OnSpawn += Respawn;
    }

    void FixedUpdate()
    {
        enemySpawning.tick(1000f * Time.deltaTime);
    }

    private Vector3 CalculateSpawnLocation(GameObject spawnPoint)
    {
        var boxCollider = spawnPoint.GetComponent<BoxCollider>();
        var position = boxCollider.transform.position;
        var size = boxCollider.size;
        var orientation = boxCollider.transform.right;
        var location = position + orientation * size.magnitude;
        return location;
    }

    private void Respawn(SpawnInfo info)
    {
        GameObject baseEnemyObject = enemyPrefabs[info.enemyIndex];
        var spawnLocation = CalculateSpawnLocation(respawnPoints[info.spawnIndex]);
        var enemy = Instantiate(baseEnemyObject, spawnLocation, new Quaternion());
        var movement = enemy.GetComponent<EnemyMovement>();
        var target = targetPoints[info.targetIndex];
        movement.Init(target);
        var enemyTypeManager = enemy.GetComponent<EnemyTypeManager>();
        enemyTypeManager.Init(baseEnemyObject);
    }
}
