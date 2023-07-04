using UnityEngine;
using System.Collections.Generic;

[System.Serializable]
[CreateAssetMenu(fileName = "HitscanWeapon", menuName = "Weapon Parameters/Hitscan")]
public class HitscanWeaponParameters : IWeaponParameters
{
    public int damage;
    public float msBetweenHitscanShots;
    public float hitscanRange;
    public float hitscanVisualizeRange;
    public GameObject hitscanPrefab;
    public List<GameObject> hittableEnemyTypes;

    public override BaseWeapon ToBaseWeapon()
    {
        var weaponStats = new WeaponStats(hittableEnemyTypes, damage);
        return new HitscanWeapon(msBetweenHitscanShots, hitscanRange, hitscanVisualizeRange, weaponStats, hitscanPrefab);
    }
}
