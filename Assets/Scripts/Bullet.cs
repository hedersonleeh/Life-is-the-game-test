using UnityEngine;
public class Hook : Bullet
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Transform _hookTail;

    private void OnEnable()
    {

        _line.useWorldSpace = true;
        _line.SetPositions(new Vector3[]
        {
            transform.position,
            _hookTail.transform.position,
        });


    }
    private void FixedUpdate()
    {
        _line.SetPosition(1, _hookTail.transform.position);
    }
}
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
