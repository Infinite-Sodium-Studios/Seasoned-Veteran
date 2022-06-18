using UnityEngine;

public class ProjectileShoot : BaseWeapon
{
    private float projectileSpeed;
    private GameObject projectilePrefab;

    public ProjectileShoot(float _minMsBetweenShots, float _projectileSpeed, GameObject _projectilePrefab) : base(_minMsBetweenShots)
    {
        projectileSpeed = _projectileSpeed;
        projectilePrefab = _projectilePrefab;
    }

    public override void OnShoot(GameObject shooter)
    {

        Vector3 outOfWorldPosition = new Vector3(0, -5, 0);
        var projectile = Object.Instantiate(projectilePrefab, outOfWorldPosition, new Quaternion());

        Physics.IgnoreCollision(projectile.GetComponent<Collider>(), shooter.GetComponent<Collider>());
        Debug.Log("Ignore between " + projectile.name + " and " + shooter.name);

        var projectileBehavior = projectile.GetComponent<ProjectileBehavior>();
        projectileBehavior.Init(projectileSpeed);
        projectileBehavior.StartMovement();
    }
}
