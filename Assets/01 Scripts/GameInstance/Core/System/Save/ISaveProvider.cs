using UnityEngine;

public interface ISaveProvider
{
    void Save(string slotId, SaveData data);
    SaveData Load(string slotId);
    bool Exists(string slotId);
    void Delete(string slotId);
}
