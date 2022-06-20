using UnityEngine;
using StarterAssets;
using System.Collections.Generic;

public class PlayerShootController : MonoBehaviour
{
    private StarterAssetsInputs _input;
    private GameObject _player;
    private List<BaseWeapon> weapons;
    private List<GameObject> weaponModels;
    int activeWeaponIndex;

    [SerializeReference] private List<GameObject> weaponObjects;

    IWeaponParameters GetParametersFromWeaponObject(GameObject weaponObject)
    {
        if (weaponObject.TryGetComponent<IWeaponParametersController>(out var weaponParams))
        {
            return weaponParams.GetParameters();
        }
        string errorMessage = "Weapon object must be assigned parameters";
        Debug.LogException(new System.Exception(errorMessage));
        return null;
    }

    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
        _player = gameObject;

        var weaponParameters = weaponObjects.ConvertAll<IWeaponParameters>(weaponObject => GetParametersFromWeaponObject(weaponObject));
        weapons = weaponParameters.ConvertAll<BaseWeapon>(weaponParams => weaponParams.ToBaseWeapon());
        weaponModels = weaponParameters.ConvertAll<GameObject>(weaponParams => weaponParams.GetWeaponModel());
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
