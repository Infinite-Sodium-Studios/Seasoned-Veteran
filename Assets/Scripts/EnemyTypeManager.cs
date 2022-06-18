using UnityEngine;

public class EnemyTypeManager : MonoBehaviour
{
    public int enemyType = -1;
    private EnemySpawning spawner;


    void Start()
    {
        spawner = GameObject.Find("EnemySpawnObject").GetComponent<EnemySpawning>();
    }

    bool IsValidType(int type)
    {
        return type >= 0 && type < spawner.enemyPrefabs.Length;
    }

    public void SetEnemyType(int type)
    {
        Debug.Assert(enemyType == -1, "Cannot set type of enemy twice");
        enemyType = type;
    }

    public int GetEnemyType()
    {
        Debug.Assert(IsValidType(enemyType), "Must have a valid enemy type");
        return enemyType;
    }
}
