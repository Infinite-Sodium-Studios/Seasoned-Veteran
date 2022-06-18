using UnityEngine;

public class PhysicsUtils
{
    public static bool ClosestRaycastHit(Ray ray, out RaycastHit hitInfo, float maxDistance)
    {
        RaycastHit[] hits = Physics.RaycastAll(ray, maxDistance);

        bool haveHit = false;
        hitInfo = new RaycastHit();
        foreach (var candidateHitInfo in hits)
        {
            if (!haveHit || candidateHitInfo.distance < hitInfo.distance)
            {
                hitInfo = candidateHitInfo;
                haveHit = true;
            }
        }
        return haveHit;
    }
}
