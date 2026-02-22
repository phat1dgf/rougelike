using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "Game/SpawnDatabase")]
public class SpawnDatabase : ScriptableObject
{
    public List<EnemyEntry> enemies;
    public List<BulletEntry> bullets;

    [System.Serializable]
    public class EnemyEntry
    {
        public EnemyType type;
        public GameObject prefab;
        public int preloadCount = 10;
    }

    [System.Serializable]
    public class BulletEntry
    {
        public BulletType type;
        public GameObject prefab;
        public int preloadCount = 20;
    }
}
public enum EnemyType
{
    Skeleton,
    Zombie
}

public enum BulletType
{
    Normal,
    Fire
}
