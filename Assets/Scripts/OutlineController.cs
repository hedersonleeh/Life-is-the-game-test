using UnityEngine;

public class OutlineController : MonoBehaviour
{
    [SerializeField]private Material _outLineMaterial;
    private MeshRenderer _myRederer;
    private void Awake()
    {
        _myRederer = GetComponent<MeshRenderer>();
    }
    private void Start()
    {
        gameObject.SetActive(false);
    }
    public void Active(bool active, Color color = default)
    {
        gameObject.SetActive(active);
     
    }
}
