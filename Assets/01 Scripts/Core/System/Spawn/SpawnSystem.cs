using UnityEngine;
using System.Collections.Generic;

public class SpawnSystem
{
    private SpawnDatabase _spawndb;
    private ObjectPool _pool;

    private Dictionary<EnemyType, GameObject> _enemyMap = new();
    private Dictionary<BulletType, GameObject> _bulletMap = new();

    public SpawnSystem(SpawnDatabase db)
    {
        _spawndb = db;
        _pool = new ObjectPool("EnemyPool");
        Initialize();
    }

    private void Initialize()
    {
        foreach (var e in _spawndb.enemies)
        {
            _enemyMap[e.type] = e.prefab;
            _pool.Preload(e.prefab, e.preloadCount);
        }

        foreach (var b in _spawndb.bullets)
        {
            _bulletMap[b.type] = b.prefab;
            _pool.Preload(b.prefab, b.preloadCount);
        }
    }

    public GameObject SpawnEnemy(EnemyType type, Vector3 pos)
    {
        return _pool.Spawn(_enemyMap[type], pos, Quaternion.identity);
    }

    public GameObject SpawnBullet(BulletType type, Vector3 pos, Quaternion rot)
    {
        return _pool.Spawn(_bulletMap[type], pos, rot);
    }

    public void Despawn(GameObject prefab, GameObject obj)
    {
        _pool.Return(obj);
    }
}
