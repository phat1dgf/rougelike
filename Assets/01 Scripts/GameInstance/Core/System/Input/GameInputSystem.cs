using UnityEngine;

public class GameInputSystem
{
    private InputActions _input;

    public InputActions Input => _input;

    public GameInputSystem()
    {
        _input = new InputActions();
    }

    public void EnableGameplay()
    {
        DisableAllMaps();
        _input.Gameplay.Enable();
    }

    public void EnableUI()
    {
        DisableAllMaps();
        _input.UI.Enable();
    }

    public void DisableAll()
    {
        _input.Disable();
    }
    public void DisableAllMaps()
    {
        foreach (var map in _input.asset.actionMaps)
        {
            map.Disable();
        }
    }
}
