using System;
using System.Collections.Generic;
public class Inventory
{
    List<Gun> _gunInventory;
    public Action<Gun> OnGunAdded;
    public Action<int> OnGunSelected;

    public Inventory()
    {
        _gunInventory = new List<Gun>();
    }
    public void AddGunToInventory(Gun toAdd)
    {

        if (!_gunInventory.Contains(toAdd))
        {
            _gunInventory.Add(toAdd);
            OnGunAdded?.Invoke(toAdd);
            toAdd.gameObject.SetActive(false);
        }
    }
    public Gun GetGun(int index)
    {
        if (index >= _gunInventory.Count) return null;
        OnGunSelected?.Invoke(index);
        return _gunInventory[index];
    }
}


