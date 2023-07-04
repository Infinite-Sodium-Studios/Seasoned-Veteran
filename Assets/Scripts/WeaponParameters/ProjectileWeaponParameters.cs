using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "ProjectileWeapon", menuName = "Weapon Parameters/Projectile")]
public class ProjectileWeaponParameters : IWeaponParameters
{
    public int damage;
    public float msBetweenProjectileShots;
    public float projectileSpeed;
    public GameObject projectilePrefab;
    public List<GameObject> hittableEnemyTypes;
    public ExplosionParameters explosionParameters;

    public override BaseWeapon ToBaseWeapon()
    {
        var weaponStats = new WeaponStats(hittableEnemyTypes, damage);
        return new ProjectileWeapon(msBetweenProjectileShots, projectileSpeed, weaponStats, projectilePrefab, explosionParameters);
    }
}
