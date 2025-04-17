using System;
using System.Collections.Generic;
using UnityEngine;

public class PoolingService : ServiceBase
{
    private HashSet<string> _uniqueTypes;
    private Dictionary<string, Pool> _poolDatabase;

    protected override void Awake()
    {
        base.Awake();

        _uniqueTypes = new HashSet<string>();
        _poolDatabase = new Dictionary<string, Pool>();
    }

    public bool IsPoolExist(string name)
    {
        return _uniqueTypes.Contains(name);
    }

    public void Register<T>(T prefab, string name) where T : MonoBehaviour
    {
        if (_uniqueTypes.Add(name))
        {
            CreatePool(prefab);
        }
    }

        public void Register<T>(T prefab) where T : MonoBehaviour
    {
        if (_uniqueTypes.Add(prefab.name))
        {
            CreatePool(prefab);
        }
    }

    public new T Instantiate<T>(T prefab) where T : MonoBehaviour
    {
        if (_uniqueTypes.Add(prefab.name))
        {
            CreatePool(prefab);
        }

        return _poolDatabase[prefab.name].Instantiate() as T;
    }

    public T Instantiate<T>(string name) where T : MonoBehaviour
    {
        return _poolDatabase[name].Instantiate() as T;
    }

    public new T Instantiate<T>(T prefab, Transform parent) where T : MonoBehaviour
    {
        if (_uniqueTypes.Add(prefab.name))
        {
            CreatePool(prefab);
        }

        return _poolDatabase[prefab.name].Instantiate(parent) as T;
    }

    public void Destroy<T>(T prefab) where T : MonoBehaviour
    {
        _poolDatabase[prefab.name].Destroy(prefab);
    }

    void CreatePool<T>(T prefab) where T : MonoBehaviour
    {
        _poolDatabase[prefab.name] = new Pool(prefab, transform);
    }
}
