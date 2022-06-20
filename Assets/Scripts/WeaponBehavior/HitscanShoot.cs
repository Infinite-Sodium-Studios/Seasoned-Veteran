using UnityEngine;

public class HitscanShoot : BaseWeapon
{
    private float maxRange;
    private EventRateLimiter rateLimiter;
    GameObject hitscanPrefab;

    public HitscanShoot(float _minMsBetweenShots, float _maxRange, WeaponStats _weaponStats, GameObject _hitscanPrefab) : base(_minMsBetweenShots, _weaponStats)
    {
        maxRange = _maxRange;
        hitscanPrefab = _hitscanPrefab;
    }

    public override void OnShoot(GameObject shooter)
    {
        var ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        bool haveHit = PhysicsUtils.ClosestRaycastHit(ray, out RaycastHit hit, maxRange);
        Debug.DrawRay(ray.origin, ray.direction.normalized * maxRange, Color.red, 1f);
        VisualizeRing(ray.origin);
        if (!haveHit)
        {
            return;
        }

        GameObject hitObject = hit.collider.gameObject;
        if (hitObject.TryGetComponent<IHittable>(out var hittable))
        {
            hittable.HitEvent(shooter, weaponStats);
        }
    }

    void VisualizeRing(Vector3 origin)
    {
        const float radius = 0.03f;
        const float forwardOffset = 0.5f;
        const int samples = 128;
        const float surviveTime = 0.1f;
        for (int iter = 0; iter < samples; iter++)
        {
            float angle = iter * 2 * Mathf.PI / samples;
            float dx = Mathf.Cos(angle);
            float dy = Mathf.Sin(angle);

            var hitscan = Object.Instantiate(hitscanPrefab, origin, Camera.main.transform.rotation);
            var lineRenderer = hitscan.GetComponent<LineRenderer>();
            int numPositions = lineRenderer.positionCount;
            for (int i = 0; i < numPositions; i++)
            {
                Vector3 oldPosition = lineRenderer.GetPosition(i);
                Vector3 newPosition = oldPosition + new Vector3(dx * radius, dy * radius, forwardOffset);
                lineRenderer.SetPosition(i, newPosition);
            }
            Object.Destroy(hitscan, surviveTime);
        }
    }
}
