using UnityEngine;
using System;

[CreateAssetMenu(menuName = "Game/Audio Database")]
public class AudioDatabase : ScriptableObject
{
    [Serializable]
    public struct SoundEntry
    {
        public SoundType type;
        public AudioClip clip;
    }

    public SoundEntry[] sounds;
}
public enum SoundType
{
    None,
    EnemyHit,
    EnemyDeath,
    PlayerShoot,
    Explosion
}
