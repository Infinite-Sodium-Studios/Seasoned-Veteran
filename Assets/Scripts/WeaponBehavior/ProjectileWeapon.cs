using UnityEngine;

public class ProjectileWeapon : BaseWeapon
{
    private float projectileSpeed;
    private GameObject projectilePrefab;
    private ExplosionParameters explosionParameters;

    public ProjectileWeapon(float _minMsBetweenShots, float _projectileSpeed, WeaponStats _weaponStats, GameObject _projectilePrefab, ExplosionParameters _explosionParameters = null) : base(_minMsBetweenShots, _weaponStats)
    {
        projectileSpeed = _projectileSpeed;
        projectilePrefab = _projectilePrefab;
        explosionParameters = _explosionParameters;
    }

    public override void OnShoot(GameObject shooter)
    {

        Vector3 outOfWorldPosition = new Vector3(0, -5, 0);
        var projectile = Object.Instantiate(projectilePrefab, outOfWorldPosition, new Quaternion());

        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), shooter.GetComponent<Collider>());

        var projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
        projectileBehavior.Init(projectileSpeed, weaponStats, explosionParameters);
        projectileBehavior.StartMovement();
    }
}
