using UnityEngine;
using System.Collections.Generic;

public class WeaponStats
{
    private List<GameObject> hittableEnemies;
    public int damage { get; private set; }

    public WeaponStats()
    {
        hittableEnemies = new List<GameObject>();
        damage = 0;
    }
    public WeaponStats(List<GameObject> _hittableEnemies, int _damage)
    {
        hittableEnemies = _hittableEnemies;
        damage = _damage;
    }
    public WeaponStats(WeaponStats other)
    {
        hittableEnemies = other.hittableEnemies;
        damage = other.damage;
    }

    public WeaponStats WithDamage(int damage)
    {
        WeaponStats weaponStats = new WeaponStats(this);
        weaponStats.damage = damage;
        return weaponStats;
    }

    public bool CanHitEnemy(GameObject enemyObject)
    {
        Debug.Assert(enemyObject != null, "Cannot check hittability of non-existent enemyObject");
        var enemyTypeManager = enemyObject.GetComponent<EnemyTypeManager>();
        Debug.Assert(enemyTypeManager != null, "All enemies should have an EnemyTypeManager component");
        foreach (var hittableEnemy in hittableEnemies)
        {
            if (enemyTypeManager.IsSameTypeAs(hittableEnemy))
            {
                return true;
            }
        }
        return false;
    }

    public int DamageToEnemy(GameObject enemyObject)
    {
        return damage;
    }
}
