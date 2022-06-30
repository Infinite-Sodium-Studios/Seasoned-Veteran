using UnityEngine;
using System.Collections.Generic;

public class WeaponStats
{
    private List<GameObject> hittableEnemies;

    public WeaponStats()
    {
        hittableEnemies = new List<GameObject>();
    }
    public WeaponStats(List<GameObject> _hittableEnemies)
    {
        hittableEnemies = _hittableEnemies;
    }

    public bool CanHitEnemy(GameObject enemyObject)
    {

        Debug.Assert(enemyObject != null, "Cannot check hittability of non-existent enemyObject");
        var enemyTypeManager = enemyObject.GetComponent<EnemyTypeManager>();
        Debug.Assert(enemyTypeManager != null, "All enemies should have an EnemyTypeManager component");
        foreach (var hittableEnemy in hittableEnemies)
        {
            if (enemyTypeManager.IsSameEnemyType(hittableEnemy))
            {
                return true;
            }
        }
        return false;
    }
}
