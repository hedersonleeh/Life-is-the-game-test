using UnityEngine;
public class Bullet : MonoBehaviour
{
    [SerializeField] private Rigidbody _rb;

    public Rigidbody Rb => _rb;
    protected void Start()
    {
        Destroy(gameObject, GetLifeTime());
    }
    protected virtual float GetLifeTime() => 10f;
}
