using System.Collections;
using UnityEngine;

public class Hook : Bullet
{
    [SerializeField] private LineRenderer _line;
    [SerializeField] private Transform _hookTail;
    private Transform _startPointTranform;
    System.Action<Hook> _callback;

    private void OnDisable()
    {
        _startPointTranform = null;
        _callback = null;
        Rb.isKinematic = false;

    }
    public void InitializeHook(Transform startPointTranform,System.Action<Hook> callback)
    {
        _line.useWorldSpace = true;
        _startPointTranform = startPointTranform;
        _callback = callback;
    }
    private void FixedUpdate()
    {
        _line.SetPosition(0, _startPointTranform.position);
        _line.SetPosition(1, _hookTail.transform.position);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HookTarget>(out var target))
        {
            _callback.Invoke(this);
            Rb.isKinematic = true;
        }
    }
    protected override float GetLifeTime()
    {
        return 1f;
    }

}
