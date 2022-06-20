using UnityEngine;

public class HitscanWeaponsParametersController : MonoBehaviour, IWeaponParametersController
{
    [SerializeField] private HitscanWeaponParameters parameters;

    public IWeaponParameters GetParameters()
    {
        return parameters;
    }
}
