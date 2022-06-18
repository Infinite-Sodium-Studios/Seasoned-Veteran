using UnityEngine;

public class HitscanShoot : BaseWeapon
{
    private float maxRange;
    private EventRateLimiter rateLimiter;

    public HitscanShoot(float _minMsBetweenShots, float _maxRange) : base(_minMsBetweenShots)
    {
        maxRange = _maxRange;
    }

    public override void OnShoot(GameObject shooter)
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bool haveHit = PhysicsUtils.ClosestRaycastHit(ray, out RaycastHit hit, maxRange);
        Debug.DrawRay(ray.origin, ray.direction.normalized * maxRange, Color.red, 1f);
        if (!haveHit)
        {
            return;
        }

        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.TryGetComponent<IHittable>(out var hittable))
        {
            hittable.HitEvent(shooter);
        }
    }
}
