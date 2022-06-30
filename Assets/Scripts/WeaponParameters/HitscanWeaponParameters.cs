using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HitscanWeaponParameters : IWeaponParameters
{
    public int damage;
    public float msBetweenHitscanShots;
    public float hitscanRange;
    public GameObject hitscanPrefab;
    public GameObject hitscanWeaponModel;
    public List<GameObject> hittableEnemyTypes;

    public BaseWeapon ToBaseWeapon()
    {
        var weaponStats = new WeaponStats(hittableEnemyTypes, damage);
        return new HitscanShoot(msBetweenHitscanShots, hitscanRange, weaponStats, hitscanPrefab);
    }

    public GameObject GetWeaponModel()
    {
        return hitscanWeaponModel;
    }
}
