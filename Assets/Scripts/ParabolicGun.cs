using UnityEngine;

public class ParabolicGun : Gun
{
    protected override void ShootGun()
    {
        var bullet = _pool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootPoint;
        bullet.Rb.useGravity = Data.useGravity;
        Vector3 initialSpeed = aimDirection.normalized * Data.bulletPower + Vector3.up * Data.bulletSpeedYMultiplier;
        bullet.Rb.velocity = initialSpeed;
        bullet.transform.forward = initialSpeed.normalized;
        bullet.AssignPool(_pool);

    }
}