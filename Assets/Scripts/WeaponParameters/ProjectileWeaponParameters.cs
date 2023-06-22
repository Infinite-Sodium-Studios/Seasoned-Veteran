using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProjectileWeaponParameters : IWeaponParameters
{
    public int damage;
    public float msBetweenProjectileShots;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject projectileWeaponModel;
    public List<GameObject> hittableEnemyTypes;
    public ExplosionParameters explosionParameters;

    public BaseWeapon ToBaseWeapon()
    {
        var weaponStats = new WeaponStats(hittableEnemyTypes, damage);
        return new ProjectileWeapon(msBetweenProjectileShots, projectileSpeed, weaponStats, projectilePrefab, explosionParameters);
    }

    public GameObject GetWeaponModel()
    {
        return projectileWeaponModel;
    }
}
