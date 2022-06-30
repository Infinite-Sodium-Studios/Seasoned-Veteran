using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class ProjectileWeaponParameters : IWeaponParameters
{

    public float msBetweenProjectileShots;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject projectileWeaponModel;
    public List<GameObject> hittableEnemyTypes;
    public ExplosionParameters explosionParameters;

    public BaseWeapon ToBaseWeapon()
    {
        return new ProjectileShoot(msBetweenProjectileShots, projectileSpeed, new WeaponStats(hittableEnemyTypes), projectilePrefab, explosionParameters);
    }

    public GameObject GetWeaponModel()
    {
        return projectileWeaponModel;
    }
}
