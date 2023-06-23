using UnityEngine;

[System.Serializable]
public class EnemyPath
{
    public EnemyPath(GameObject spawnPoint, GameObject targetPoint)
    {
        this.spawnPoint = spawnPoint;
        this.targetPoint = targetPoint;
    }

    public GameObject spawnPoint;
    public GameObject targetPoint;
}

public class EnemySpawningBehavior : MonoBehaviour
{
    private EnemySpawning enemySpawning;
    [SerializeField] private GameObject[] enemyPrefabs;
    [SerializeField] private EnemyPath[] paths;
    [SerializeField] private long respawnFrequencyMs;

    void Awake()
    {
        enemySpawning = new EnemySpawning(respawnFrequencyMs, enemyPrefabs.Length, paths.Length);
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

    private void initEnemy(GameObject enemy, GameObject target, GameObject baseEnemyObject)
    {
        if (enemy.TryGetComponent<EnemyTypeManager>(out var type))
        {
            type.Init(baseEnemyObject);
        }
        if (enemy.TryGetComponent<EnemyMovement>(out var movement))
        {
            movement.Init(target);
        }
    }

    private void Respawn(SpawnInfo info)
    {
        var baseEnemyObject = enemyPrefabs[info.enemyIndex];
        var path = paths[info.pathIndex];
        var spawnLocation = CalculateSpawnLocation(path.spawnPoint);
        var enemy = Instantiate(baseEnemyObject, spawnLocation, new Quaternion());
        var target = paths[info.pathIndex].targetPoint;
        initEnemy(enemy, target, baseEnemyObject);
    }
}
