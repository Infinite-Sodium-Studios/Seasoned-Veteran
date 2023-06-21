using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class ParticleSystemCleanup : MonoBehaviour
{
    ParticleSystem ps;

    void Start()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void Update()
    {
        if (ps && !ps.IsAlive())
        {
            Destroy(gameObject);
        }
    }
}
