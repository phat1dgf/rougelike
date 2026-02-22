using UnityEngine;
using System.Collections.Generic;

public class ObjectPool
{
    private class PrefabPool
    {
        public GameObject Prefab;
        public Queue<GameObject> Objects = new();
        public Transform Parent;

        public PrefabPool(GameObject prefab, Transform root)
        {
            Prefab = prefab;

            var go = new GameObject(prefab.name + "_Pool");
            Parent = go.transform;
            Parent.SetParent(root);
        }
    }

    private Dictionary<GameObject, PrefabPool> _prefabPools = new();
    private Dictionary<GameObject, PrefabPool> _instanceLookup = new();

    private Transform _root;

    public ObjectPool(string poolName)
    {
        var rootGO = new GameObject(poolName);
        _root = rootGO.transform;
    }

    public void Preload(GameObject prefab, int count)
    {
        var pool = GetOrCreatePool(prefab);

        for (int i = 0; i < count; i++)
        {
            var obj = CreateInstance(pool);
            pool.Objects.Enqueue(obj);
        }
    }

    public GameObject Spawn(GameObject prefab, Vector3 pos, Quaternion rot)
    {
        var pool = GetOrCreatePool(prefab);

        GameObject obj;

        if (pool.Objects.Count > 0)
            obj = pool.Objects.Dequeue();
        else
            obj = CreateInstance(pool);

        obj.transform.SetPositionAndRotation(pos, rot);
        obj.transform.SetParent(null);
        obj.SetActive(true);

        foreach (var poolable in obj.GetComponentsInChildren<IPoolable>())
            poolable.OnSpawned();

        return obj;
    }

    public void Return(GameObject obj)
    {
        if (!_instanceLookup.TryGetValue(obj, out var pool))
        {
            Debug.LogWarning("Object not managed by this pool.");
            return;
        }

        foreach (var poolable in obj.GetComponentsInChildren<IPoolable>())
            poolable.OnDespawned();

        obj.SetActive(false);
        obj.transform.SetParent(pool.Parent);

        pool.Objects.Enqueue(obj);
    }

    private PrefabPool GetOrCreatePool(GameObject prefab)
    {
        if (_prefabPools.TryGetValue(prefab, out var pool))
            return pool;

        pool = new PrefabPool(prefab, _root);
        _prefabPools[prefab] = pool;
        return pool;
    }

    private GameObject CreateInstance(PrefabPool pool)
    {
        var obj = GameObject.Instantiate(pool.Prefab);
        obj.SetActive(false);
        obj.transform.SetParent(pool.Parent);

        _instanceLookup[obj] = pool;
        return obj;
    }
}
