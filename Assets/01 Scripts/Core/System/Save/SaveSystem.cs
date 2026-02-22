using UnityEngine;
using System.IO;


public class SaveSystem
{
    private ISaveProvider _provider;
    private SaveData _currentData;
    private string _currentSlot;

    public SaveSystem(ISaveProvider provider)
    {
        this._provider = provider;
    }

    public void LoadSlot(string slotId)
    {
        _currentSlot = slotId;
        _currentData = _provider.Load(slotId);
    }

    public void Save()
    {
        if (string.IsNullOrEmpty(_currentSlot))
            return;

        _provider.Save(_currentSlot, _currentData);
    }

    public void CreateNewSlot(string slotId)
    {
        _currentSlot = slotId;
        _currentData = new SaveData();
        Save();
    }

    public SaveData GetData()
        => _currentData;

    public void DeleteSlot(string slotId)
        => _provider.Delete(slotId);
}

