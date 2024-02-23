using System.Collections.Generic;
using UnityEngine;

public class Pool<T> where T : MonoBehaviour
{
    private Queue<T> _pool = new Queue<T>();
    private GameObject _parent;
    private T _prefab;
    public Pool(T prefab, int initialsize = 5)
    {
        _prefab = prefab;
        _parent = new GameObject($"Pool <{prefab.name}>");
        for (int i = 0; i < initialsize; i++)
        {
            var obj = GameObject.Instantiate(prefab, _parent.transform);
            obj.gameObject.SetActive(false);
            _pool.Enqueue(obj);

        }
    }

    public T Get()
    {
        T obj;

        if (_pool.Count < 1)
        {
            var nObj = GameObject.Instantiate(_prefab, _parent.transform);
            nObj.gameObject.SetActive(false);
            _pool.Enqueue(nObj);
            Debug.Log("Created on demand");

        }


        obj = _pool.Dequeue();

        return obj;
    }

    public void BackToPool(T obj)
    {
        obj.gameObject.SetActive(false);
        _pool.Enqueue(obj);
    }
}