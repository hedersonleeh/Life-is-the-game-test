using UnityEngine;

public class DacingCharacterController : MonoBehaviour
{
    [SerializeField] private Camera _characterCam;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);
    }
    private void Start()
    {
        _characterCam.gameObject.SetActive(false);
    }

    public void ActiveCamera()
    {
        _characterCam.gameObject.SetActive(true);
        //this is for hiding the charcter on scene
        transform.position = -Vector3.one * 0xffff;
    }

}
