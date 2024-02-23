using System;
using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public Rigidbody Rb => _rb;
    private Pool<Bullet> _pool;
    protected void Start()
    {
        Invoke(nameof(DestroyBullet), GetLifeTime());
    }

    private void DestroyBullet()
    {
        _pool.BackToPool(this);
    }

    public void AssignPool(Pool<Bullet> pool)
    {
        _pool = pool;
    }
    protected virtual float GetLifeTime() => 10f;
}
