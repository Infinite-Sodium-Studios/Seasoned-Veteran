
using UnityEngine;

public abstract class BaseWeapon
{
    private EventRateLimiter rateLimiter;

    protected BaseWeapon(float _minMsBetweenShots)
    {
        rateLimiter = new EventRateLimiter(_minMsBetweenShots);
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
