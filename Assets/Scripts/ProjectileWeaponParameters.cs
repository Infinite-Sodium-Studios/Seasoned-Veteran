using UnityEngine;

[System.Serializable]
public class ProjectileWeaponParameters : IWeaponParameters
{

    public float msBetweenProjectileShots;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public GameObject projectileWeaponModel;
    public int[] hittableEnemyTypes;
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
