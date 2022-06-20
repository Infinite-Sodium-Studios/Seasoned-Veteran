
using UnityEngine;

public abstract class BaseWeapon
{
    private EventRateLimiter rateLimiter;
    protected WeaponStats weaponStats;

    protected BaseWeapon(float _minMsBetweenShots, WeaponStats _weaponStats)
    {
        rateLimiter = new EventRateLimiter(_minMsBetweenShots);
        weaponStats = _weaponStats;
    }

    public abstract void OnShoot(GameObject shooter);
    public void ShootFrom(GameObject shooter)
    {
        if (!rateLimiter.ShouldAllowEvent())
        {
            return;
        }
        OnShoot(shooter);
    }
}
