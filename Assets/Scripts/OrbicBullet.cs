using System.Collections.Generic;
using UnityEngine;

public class OrbicBullet : Bullet
{
    List<UnityEngine.Collider> _colliders;
    private void Awake()
    {
        Rb.constraints = RigidbodyConstraints.FreezePositionY;
    }

    private void FixedUpdate()
    {
        Rb.velocity -= Rb.velocity * Time.smoothDeltaTime;

    }
    private void OnEnable()
    {
        _colliders = new List<Collider>();
    }
    private void OnDisable()
    {
        foreach (var collider in _colliders)
        {
            if (collider.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
        }
    }

    private void OnTriggerStay(UnityEngine.Collider other)
    {
        if (other.gameObject.layer == 3)//Stage layer
        {

            Random.InitState(other.GetHashCode());//repeat the same axis to move in
            other.transform.RotateAround(transform.position, Random.insideUnitSphere, 100 * Time.deltaTime);
            if (!_colliders.Contains(other))
            {
                _colliders.Add(other);
                if (other.TryGetComponent<Rigidbody>(out var rb))
                {
                    rb.useGravity = false;
                    rb.isKinematic = true;
                }
            }


        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.layer == 3)//Stage layer
        {

            if (other.TryGetComponent<Rigidbody>(out var rb))
            {
                rb.useGravity = true;
                rb.isKinematic = false;
            }
            _colliders.Remove(other);
        }
    }
}


