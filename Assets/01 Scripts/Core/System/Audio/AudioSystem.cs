using UnityEngine;
using System.Collections.Generic;

public class AudioSystem
{
    private ObjectPool _pool;
    private Dictionary<SoundType, AudioClip> _clipMap = new();
    private GameObject _audioPrefab;

    public AudioSystem(AudioDatabase db, GameObject audioPrefab)
    {
        _pool = new ObjectPool("AudioPool");
        _audioPrefab = audioPrefab;

        foreach (var s in db.sounds)
            _clipMap[s.type] = s.clip;

        _pool.Preload(_audioPrefab, 10);
    }

    public void PlaySound(SoundType type, Vector3 position)
    {
        if (!_clipMap.TryGetValue(type, out var clip))
            return;

        var obj = _pool.Spawn(_audioPrefab, position, Quaternion.identity);

        var source = obj.GetComponent<AudioSource>();
        source.clip = clip;
        source.Play();

        GameRoot.Instance.StartCoroutine(
            ReturnWhenFinished(source, obj)
        );
    }

    private System.Collections.IEnumerator ReturnWhenFinished(AudioSource source, GameObject obj)
    {
        yield return new WaitForSeconds(source.clip.length);
        _pool.Return(obj);
    }
}
