using UnityEngine;

public class PooledAudio : MonoBehaviour, IPoolable
{
    private AudioSource _source;

    private void Awake()
    {
        _source = GetComponent<AudioSource>();
    }

    public void OnSpawned()
    {
        _source.volume = 1f;
    }

    public void OnDespawned()
    {
        _source.Stop();
        _source.clip = null;
    }
}
