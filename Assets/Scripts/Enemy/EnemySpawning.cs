using System;

public struct SpawnInfo 
{
    public SpawnInfo(int _enemyIndex, int _spawnIndex, int _targetIndex)
    {
        enemyIndex = _enemyIndex;
        spawnIndex = _spawnIndex;
        targetIndex = _targetIndex;
    }

    public int enemyIndex;
    public int spawnIndex;
    public int targetIndex;
}

public class EnemySpawning
{
    private long respawnFrequencyMs;
    private int countEnemyTypes;
    private int countSpawnPoints;
    private int countTargetPoints;
    public event Action<SpawnInfo> OnSpawn;

    private float msSinceLastSpawn = 0;
    private int remainingEnemiesForWave = 0;
    private int spawnPointIndexForWave = 0;
    private Random randomGenerator = new Random();


    public EnemySpawning(long _respawnFrequencyMs, int _countEnemyTypes, int _countSpawnPoints, int _countTargetPoints)
    {
        respawnFrequencyMs = _respawnFrequencyMs;
        countEnemyTypes = _countEnemyTypes;
        countSpawnPoints = _countSpawnPoints;
        countTargetPoints = _countTargetPoints;
    }

    public void tick(float deltaTimeMs)
    {
        msSinceLastSpawn += deltaTimeMs;
        UnityEngine.Debug.Log("Ms since last spawn: " + msSinceLastSpawn + " vs " + respawnFrequencyMs);
        if (msSinceLastSpawn >= respawnFrequencyMs)
        {
            var spawnInfo = Respawn();
            OnSpawn?.Invoke(spawnInfo);
            msSinceLastSpawn = 0;
        }
    }

    private int GetNewSpawnPoint() {
        var newSpawnPointIndex = spawnPointIndexForWave;
        while (newSpawnPointIndex == spawnPointIndexForWave && countSpawnPoints > 1) {
            newSpawnPointIndex = randomGenerator.Next(0, countSpawnPoints);
        }
        return newSpawnPointIndex;
    }

    private int GetSpawnPointIndexForWave() {
        if (remainingEnemiesForWave <= 0) {
            spawnPointIndexForWave = GetNewSpawnPoint();
            remainingEnemiesForWave = randomGenerator.Next(3, 5);
        }
        --remainingEnemiesForWave;
        return spawnPointIndexForWave;
    }

    private int GetEnemyIndex()
    {
        return randomGenerator.Next(0, countEnemyTypes);
    }

    private SpawnInfo Respawn()
    {
        var spawnIndex = GetSpawnPointIndexForWave();
        var targetIndex = spawnIndex % countTargetPoints;
        var enemyIndex = GetEnemyIndex();
        return new SpawnInfo(enemyIndex, spawnIndex, targetIndex);
    }
}
