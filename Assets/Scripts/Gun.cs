﻿using System;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] private string gunId;
    [SerializeField] private MeshRenderer _renderer;
    [SerializeField] protected Transform _shootPoint;
    public OutlineController outline;
    protected Pool<Bullet> _pool;
    private float _fireRateTimer;
    public Vector3 shootPoint => _shootPoint.position;
    public Vector3 aimDirection => _shootPoint.forward;
    public GunData Data { get; private set; }
    public virtual bool CanShoot => _fireRateTimer >= Data.firingRate;
    private void Awake()
    {
        Data = Resources.Load<GunData>("Guns/" + gunId);
        _fireRateTimer = 0f;
        Initialize();
    }

    protected virtual void Initialize()
    {
        _pool = new Pool<Bullet>(Data.bulletPrefab);

    }
    private void Update()
    {
        if (!CanShoot)
            _fireRateTimer += Time.deltaTime;
    }
    public bool Shoot() {
        if (!CanShoot) return false;
        ShootGun();
        _fireRateTimer = 0;
        return true; 
    }
    protected virtual void ShootGun()
    {
        var bullet = _pool.Get();
        bullet.gameObject.SetActive(true);
        bullet.transform.position = shootPoint;
        Vector3 initialSpeed = aimDirection.normalized * Data.bulletPower;
        bullet.Rb.useGravity = Data.useGravity;
        bullet.Rb.velocity = initialSpeed;
        bullet.transform.forward = initialSpeed.normalized;
        bullet.AssignPool(_pool);
    }
    public MeshRenderer GetMeshRenderer() => _renderer;
}
