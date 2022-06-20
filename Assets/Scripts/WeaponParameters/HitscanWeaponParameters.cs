using UnityEngine;

[System.Serializable]
public class HitscanWeaponParameters : IWeaponParameters
{

    public float msBetweenHitscanShots;
    public float hitscanRange;
    public GameObject hitscanPrefab;
    public GameObject hitscanWeaponModel;
    public int[] hittableEnemyTypes;

    public BaseWeapon ToBaseWeapon()
    {
        return new HitscanShoot(msBetweenHitscanShots, hitscanRange, new WeaponStats(hittableEnemyTypes), hitscanPrefab);
    }

    public GameObject GetWeaponModel()
    {
        return hitscanWeaponModel;
    }
}
