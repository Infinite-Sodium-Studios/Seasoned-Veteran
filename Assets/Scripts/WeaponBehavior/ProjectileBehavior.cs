using UnityEngine;
using UnityEngine.AI;
using System.Collections;
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
        Debug.Log("Projectile " + gameObject + " just hit " + collision.gameObject);
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
        var hitObjectIds = new Dictionary<int, GameObject>();
        foreach (var contact in contacts)
        {
            Debug.Log("Explosion Contact at " + contact.point);
            Instantiate(explosionParameters.explosionPrefab, contact.point, new Quaternion());
            var overlappingColliders = Physics.OverlapSphere(contact.point, explosionParameters.explosionRadius);
            foreach (var collider in overlappingColliders)
            {
                var currGameObject = collider.gameObject;
                if (currGameObject == gameObject)
                {
                    continue;
                }
                hitObjectIds[currGameObject.GetInstanceID()] = currGameObject;
            }
        }
        foreach (var (id, currGameObject) in hitObjectIds)
        {
            if (currGameObject.TryGetComponent<IHittable>(out var hittable))
            {
                hittable.HitEvent(gameObject, weaponStats);
            }

        }
        Destroy(gameObject);
    }

}
