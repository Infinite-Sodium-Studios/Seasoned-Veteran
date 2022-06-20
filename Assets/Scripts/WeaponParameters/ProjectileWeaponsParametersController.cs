using UnityEngine;

public class ProjectileWeaponsParametersController : MonoBehaviour, IWeaponParametersController
{
    [SerializeField] private ProjectileWeaponParameters parameters;

    public IWeaponParameters GetParameters()
    {
        return parameters;
    }
}
