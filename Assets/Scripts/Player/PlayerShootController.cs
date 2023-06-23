using UnityEngine;
using StarterAssets;
using System.Collections.Generic;

[System.Serializable]
public class Weapon
{
    [SerializeField] public GameObject model;
    [SerializeField] public IWeaponParameters parameters;
}

public class PlayerShootController : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private GameObject _player;
    private List<BaseWeapon> weapons;
    private List<GameObject> weaponModels;
    int activeWeaponIndex;
    [SerializeField] private List<Weapon> weaponObjects;

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _player = gameObject;

        weapons = weaponObjects.ConvertAll<BaseWeapon>(weapon => weapon.parameters.ToBaseWeapon());
        weaponModels = weaponObjects.ConvertAll<GameObject>(weapon => weapon.model);
        activeWeaponIndex = 0;
        for (int i = 0; i < weaponModels.Count; i++)
        {
            weaponModels[i].SetActive(false);
        }
        weaponModels[activeWeaponIndex].SetActive(true);
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
        int currentActiveWeaponIndex = _input.selectedWeapon;
        int previousActiveWeaponIndex = activeWeaponIndex;
        if (currentActiveWeaponIndex == previousActiveWeaponIndex)
        {
            return;
        }
        weaponModels[currentActiveWeaponIndex].SetActive(true);
        weaponModels[previousActiveWeaponIndex].SetActive(false);
        activeWeaponIndex = currentActiveWeaponIndex;

    }

    void Shoot()
    {
        if (_input.shoot)
        {
            Debug.Assert(activeWeaponIndex >= 0);
            Debug.Assert(activeWeaponIndex < weapons.Count);
            weapons[activeWeaponIndex].ShootFrom(_player);
        }
    }
}
