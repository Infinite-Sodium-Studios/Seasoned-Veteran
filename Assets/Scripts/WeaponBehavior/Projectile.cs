using System;

public class Projectile
{
    private float speed;
    private WeaponStats weaponStats;
    private ExplosionParameters explosionParameters;

    public Projectile(float _speed, WeaponStats _weaponStats, ExplosionParameters _explosionParameters)
    {
        speed = _speed;
        weaponStats = _weaponStats;
        explosionParameters = _explosionParameters;
    }

    int DamageWithFalloffCallback(int baseDamage, float distance)
    {
        float damageScale = 1f - distance / explosionParameters.explosionRadius;
        float damage = baseDamage * Math.Clamp(damageScale, 0f, 1f);
        return Convert.ToInt32(Math.Ceiling(damage));
    }
}
