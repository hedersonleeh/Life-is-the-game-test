using UnityEngine;

public class ParabolicGun : Gun
{
    public override bool Shoot()
    {
        if (!CanShoot) return false;
        var bullet = _pool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootPoint;
        bullet.Rb.useGravity = true;
        Vector3 initialSpeed = aimDirection.normalized * Data.bulletPower + Vector3.up * Data.bulletSpeedYMultiplier;
        bullet.Rb.velocity = initialSpeed;
        bullet.transform.forward = initialSpeed.normalized;
        _fireRateTimer = 0f;
        return true;
    }
}