using System.Collections;
using UnityEngine;
[RequireComponent(typeof(CharacterController))]
public class FpsController : MonoBehaviour
{
    [SerializeField] private float _speed = 10;
    [SerializeField] private float _mouseSenvility = 10;
    [SerializeField] private Transform _gunArm;
    [SerializeField] private Animator _animator;
    private Camera _camera;
    private CharacterController _controller;
    private Vector2 _input;
    private float _xRotacion;
    private float _yRotacion;

    private Gun _gunSelected;
    private Gun _currentGun;
    public Inventory Inventory { get; private set; }

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _camera = GetComponentInChildren<Camera>();
        Inventory = new Inventory();
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        _input.x = Input.GetAxisRaw("Horizontal");
        _input.y = Input.GetAxisRaw("Vertical");


        float mouseX = Input.GetAxis("Mouse X") * _mouseSenvility * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSenvility * Time.deltaTime;

        _xRotacion -= mouseY;
        _xRotacion = Mathf.Clamp(_xRotacion, -90, 90);
        _yRotacion += mouseX;

        transform.localRotation = Quaternion.Euler(_xRotacion, _yRotacion, 0);

        if (_gunSelected != null)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Inventory.AddGunToInventory(_gunSelected);
                _gunSelected.GetComponent<Collider>().enabled = false;
                _gunSelected = null;
            }
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) EquipGun(0);
        if (Input.GetKeyDown(KeyCode.Alpha2)) EquipGun(1);
        if (Input.GetKeyDown(KeyCode.Alpha3)) EquipGun(2);

        if (_currentGun == null) return;


        if (Input.GetMouseButtonDown(0))
        {
            if (_currentGun.Shoot())
            {
                _animator.Play("Shoot");
            }

        }
    }

    private void EquipGun(int index)
    {
        if (_currentGun != null)
        {
            _currentGun.transform.parent = null;
            _currentGun.gameObject.SetActive(false);
        }

        _currentGun = Inventory.GetGun(index);
        if (_currentGun != null)
        {
            _currentGun.gameObject.SetActive(true);
            _currentGun.transform.parent = _gunArm;
            _currentGun.transform.localPosition = Vector3.zero;
            _currentGun.transform.localRotation = Quaternion.identity;
            _currentGun.outline.Active(false);
        }
    }

    private void FixedUpdate()
    {
        var right = _camera.transform.right;
        var forward = _camera.transform.forward;
        forward.y = 0;
        _input.Normalize();
        var movement = _input.x * right + _input.y * forward;
        Debug.Log(movement);
        _controller.Move(movement * _speed * Time.fixedDeltaTime);

        if (Physics.Raycast(_camera.transform.position, _camera.transform.forward, out var hit, float.PositiveInfinity))
        {
            if (hit.collider.TryGetComponent<Gun>(out var gun))
            {

                if (_gunSelected != null && _gunSelected != gun)
                {
                    _gunSelected.outline.Active(false);

                }
                gun.outline.Active(true);
                _gunSelected = gun;
            }
        }
        Debug.DrawRay(_camera.transform.position, _camera.transform.forward);

    }
}
