using System;
using System.Collections.Generic;
using UnityEngine;

public class SceneRoot : MonoBehaviour
{
    [SerializeField] private GameMode _gameMode;
    [SerializeField] private GameState _gameState;

    private List<SceneSubsystem> subsystems = new();

    private void Awake()
    {
        // Create subsystems
        AddSubsystems();

        foreach (var sys in subsystems)
        {
            sys.Inject(_gameMode, _gameState);
            sys.Initialize();
        }
    }

    private void AddSubsystems()
    {
        
    }

    private void Update()
    {
        foreach (var sys in subsystems)
            sys.Tick(Time.deltaTime);
    }

    private void OnDestroy()
    {
        foreach (var sys in subsystems)
            sys.Dispose();
    }
}
