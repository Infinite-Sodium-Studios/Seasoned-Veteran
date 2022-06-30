using UnityEngine;
using System;
using System.Collections.Generic;

public class ProjectileBehavior : MonoBehaviour
{
    bool initialized = false;
    private float speed;
    private WeaponStats weaponStats;
    private ExplosionParameters explosionParameters;

    public void Init(float _speed, WeaponStats _weaponStats, ExplosionParameters _explosionParameters)
    {
        initialized = true;
        speed = _speed;
        weaponStats = _weaponStats;
        explosionParameters = _explosionParameters;
    }

    public void StartMovement()
    {
        Debug.Assert(initialized);

        Ray ray = Camera.main.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        var position = ray.origin;
        var rotation = Camera.main.transform.rotation;
        gameObject.transform.SetPositionAndRotation(position, rotation);

        var rigidBody = GetComponent<Rigidbody>();
        rigidBody.AddForce(speed * rigidBody.transform.forward);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool isExplosive = explosionParameters.explosionPrefab != null;
        if (isExplosive)
        {
            ExplosiveCollision(collision);

        }
        else
        {
            NonExplosiveCollision(collision);
        }
    }

    int DamageWithFalloffCallback(int baseDamage, float distance)
    {
        float damageScale = 1f - distance / explosionParameters.explosionRadius;
        float damage = baseDamage * Mathf.Clamp01(damageScale);
        return Convert.ToInt32(Math.Ceiling(damage));
    }

    void NonExplosiveCollision(Collision collision)
    {
        if (collision.gameObject.TryGetComponent<IHittable>(out var hittable))
        {
            hittable.HitEvent(gameObject, weaponStats);
        }
        Destroy(gameObject);
    }

    void ExplosiveCollision(Collision collision)
    {
        var contacts = new ContactPoint[collision.contactCount];
        int numContacts = collision.GetContacts(contacts);
        var minDistanceForObject = new Dictionary<GameObject, float>();
        foreach (var contact in contacts)
        {
            Instantiate(explosionParameters.explosionPrefab, contact.point, new Quaternion());
            var overlappingColliders = Physics.OverlapSphere(contact.point, explosionParameters.explosionRadius);
            foreach (var collider in overlappingColliders)
            {
                var currGameObject = collider.gameObject;
                if (currGameObject == gameObject)
                {
                    continue;
                }
                Vector3 closestPointOnObject = collider.ClosestPointOnBounds(contact.point);
                float distance = Vector3.Distance(closestPointOnObject, contact.point);
                if (!minDistanceForObject.ContainsKey(currGameObject))
                {
                    minDistanceForObject[currGameObject] = float.PositiveInfinity;
                }
                minDistanceForObject[currGameObject] = Math.Min(minDistanceForObject[currGameObject], distance);
            }
        }
        foreach (var (currGameObject, distance) in minDistanceForObject)
        {
            if (currGameObject.TryGetComponent<IHittable>(out var hittable))
            {
                int actualDamage = DamageWithFalloffCallback(explosionParameters.damage, distance);
                var weaponStatsWithFalloff = weaponStats.WithDamage(actualDamage);
                hittable.HitEvent(gameObject, weaponStatsWithFalloff);
            }

        }
        Destroy(gameObject);
    }

}
