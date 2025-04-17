using System.Collections.Generic;
using UnityEngine;

public class Pool
{
    private LinkedList<MonoBehaviour> _poolObjectList;
    private MonoBehaviour _prefab;
    private Transform _parent;

    public Pool(MonoBehaviour prefab, Transform parent)
    {
        _prefab = prefab;
        _parent = parent;

        _poolObjectList = new LinkedList<MonoBehaviour>();
    }

    public MonoBehaviour Instantiate()
    {
        if (_poolObjectList.First == null)
        {
            Create();
        }

        MonoBehaviour obj = _poolObjectList.Last.Value;
        obj.gameObject.SetActive(_prefab.gameObject.activeSelf);
        _poolObjectList.RemoveLast();
        obj.transform.SetParent(null);

        return obj;
    }

    public MonoBehaviour Instantiate(Transform parent)
    {
        if (_poolObjectList.First == null)
        {
            Create();
        }

        MonoBehaviour obj = _poolObjectList.Last.Value;
        obj.gameObject.SetActive(_prefab.gameObject.activeSelf);
        _poolObjectList.RemoveLast();
        obj.transform.SetParent(parent);

        return obj;
    }
    
    public void Destroy(MonoBehaviour obj)
    {
        obj.transform.position = new Vector3(100, 100, 100);
        obj.transform.SetParent(_parent);
        obj.gameObject.SetActive(false);
        _poolObjectList.AddLast(obj);
    }

    void Create()
    {
        MonoBehaviour obj = GameObject.Instantiate(_prefab, new Vector3(100, 100, 100), Quaternion.identity);
        obj.gameObject.SetActive(false);
        obj.transform.SetParent(_parent);
        obj.name = _prefab.name;
        _poolObjectList.AddLast(obj);
    }
}
