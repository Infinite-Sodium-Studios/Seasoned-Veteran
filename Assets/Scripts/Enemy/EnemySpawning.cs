using System;

public struct SpawnInfo 
{
    public SpawnInfo(int _enemyIndex, int _pathIndex)
    {
        enemyIndex = _enemyIndex;
        pathIndex = _pathIndex;
    }

    public int enemyIndex;
    public int pathIndex;
}

public class EnemySpawning
{
    private long respawnFrequencyMs;
    private int countEnemyTypes;
    private int countPaths;
    public event Action<SpawnInfo> OnSpawn;

    private float msSinceLastSpawn = 0;
    private int remainingEnemiesForWave = 0;
    private int pathIndexForWave = 0;
    private Random randomGenerator = new Random();


    public EnemySpawning(long _respawnFrequencyMs, int _countEnemyTypes, int _countPaths)
    {
        respawnFrequencyMs = _respawnFrequencyMs;
        countEnemyTypes = _countEnemyTypes;
        countPaths = _countPaths;
    }

    public void tick(float deltaTimeMs)
    {
        msSinceLastSpawn += deltaTimeMs;
        if (msSinceLastSpawn >= respawnFrequencyMs)
        {
            var spawnInfo = Respawn();
            OnSpawn?.Invoke(spawnInfo);
            msSinceLastSpawn = 0;
        }
    }

    private int GetNewPath() {
        var newPathIndex = pathIndexForWave;
        while (newPathIndex == pathIndexForWave && countPaths > 1) {
            newPathIndex = randomGenerator.Next(0, countPaths);
        }
        return newPathIndex;
    }

    private int GetSpawnPointIndexForWave() {
        if (remainingEnemiesForWave <= 0) {
            pathIndexForWave = GetNewPath();
            remainingEnemiesForWave = randomGenerator.Next(3, 5);
        }
        --remainingEnemiesForWave;
        return pathIndexForWave;
    }

    private int GetEnemyIndex()
    {
        return randomGenerator.Next(0, countEnemyTypes);
    }

    private SpawnInfo Respawn()
    {
        var pathIndex = GetSpawnPointIndexForWave();
        var enemyIndex = GetEnemyIndex();
        return new SpawnInfo(enemyIndex, pathIndex);
    }
}
