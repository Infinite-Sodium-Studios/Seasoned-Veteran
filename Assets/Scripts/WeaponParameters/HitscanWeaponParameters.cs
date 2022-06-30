using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
public class HitscanWeaponParameters : IWeaponParameters
{

    public float msBetweenHitscanShots;
    public float hitscanRange;
    public GameObject hitscanPrefab;
    public GameObject hitscanWeaponModel;
    public List<GameObject> hittableEnemyTypes;

    public BaseWeapon ToBaseWeapon()
    {
        return new HitscanShoot(msBetweenHitscanShots, hitscanRange, new WeaponStats(hittableEnemyTypes), hitscanPrefab);
    }

    public GameObject GetWeaponModel()
    {
        return hitscanWeaponModel;
    }
}
