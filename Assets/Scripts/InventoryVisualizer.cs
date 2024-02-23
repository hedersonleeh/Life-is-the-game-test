using UnityEngine;
using UnityEngine.UI;

public class InventoryVisualizer : MonoBehaviour
{
    [SerializeField] private Button _inventoryBtnPrefab;
    [SerializeField] private RectTransform _content;
    private Inventory _inventory;
    private int currentCount;

    private void Awake()
    {
    }
    private void Start()
    {
        _inventory = FindObjectOfType<FpsController>().Inventory;
        _inventory.OnGunAdded += OnGunAdded;
        _inventory.OnGunSelected += OnGunSelected;
    }

    private void OnGunAdded(Gun obj)
    {
        var btn = Instantiate(_inventoryBtnPrefab, _content);
        btn.GetComponent<Image>().sprite = obj.Data.icon;
        btn.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = (btn.transform.GetSiblingIndex()+1).ToString();
    }
    private void OnGunSelected(int index)
    {
        Debug.Log(_content.transform.GetChild(index).name);
        for (int i = 0; i < _content.childCount; i++)
        {
            _content.transform.GetChild(i).Find("BackGround").GetComponent<Image>().color = index == i ? Color.yellow : Color.white;
        }
    }
}


