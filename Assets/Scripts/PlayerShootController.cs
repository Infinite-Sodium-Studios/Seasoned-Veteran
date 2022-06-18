using UnityEngine;
using StarterAssets;

public class PlayerShootController : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private GameObject _player;
    private BaseWeapon[] weapons;
    private BaseWeapon activeWeapon;

    [SerializeField] private float msBetweenHitscanShots;
    [SerializeField] private float hitscanRange;

    [SerializeField] private float msBetweenProjectileShots;
    [SerializeField] private float projectileSpeed;
    [SerializeField] private GameObject projectilePrefab;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _player = gameObject;
        BaseWeapon railGun = new HitscanShoot(msBetweenHitscanShots, hitscanRange);
        BaseWeapon rocketLauncher = new ProjectileShoot(msBetweenProjectileShots, projectileSpeed, projectilePrefab);
        weapons = new[] { railGun, rocketLauncher };
        activeWeapon = weapons[0];
    }

    void Update()
    {
        CheckSelectedWeapon();
    }

    void FixedUpdate()
    {
        Shoot();
    }

    void CheckSelectedWeapon()
    {
        activeWeapon = weapons[_input.selectedWeapon];
    }

    void Shoot()
    {
        if (_input.shoot)
        {
            activeWeapon.ShootFrom(_player);
        }
    }
}
