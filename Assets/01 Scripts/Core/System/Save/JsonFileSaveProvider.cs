using UnityEngine;
using System.IO;

public class JsonFileSaveProvider : ISaveProvider
{
    private string _basePath;

    public JsonFileSaveProvider()
    {
        _basePath = Application.persistentDataPath + "/saves/";
        if (!Directory.Exists(_basePath))
            Directory.CreateDirectory(_basePath);
    }

    private string GetPath(string slotId)
        => _basePath + $"save_{slotId}.json";

    public void Save(string slotId, SaveData data)
    {
        string json = JsonUtility.ToJson(data, true);
        File.WriteAllText(GetPath(slotId), json);
    }

    public SaveData Load(string slotId)
    {
        string path = GetPath(slotId);

        if (!File.Exists(path))
            return new SaveData();

        string json = File.ReadAllText(path);
        return JsonUtility.FromJson<SaveData>(json);
    }

    public bool Exists(string slotId)
        => File.Exists(GetPath(slotId));

    public void Delete(string slotId)
    {
        string path = GetPath(slotId);
        if (File.Exists(path))
            File.Delete(path);
    }
}
