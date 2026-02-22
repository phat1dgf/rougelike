using System;
using UnityEngine;

public class GameState : MonoBehaviour
{
    public MatchState CurrentState { get; private set; }

    public float ElapsedTime { get; private set; }
    public int PlayerLevel { get; private set; } = 1;
    public int Exp { get; private set; }
    public bool IsGameOver => CurrentState == MatchState.GameOver;

    public void SetMatchState(MatchState newState)
    {
        if (CurrentState == newState)
            return;

        CurrentState = newState;
        GameRoot.Instance.GameEventBus.Publish(new GameEvents.MatchStateChanged(CurrentState));
    }

    public void AddTime(float delta)
    {
        if (CurrentState != MatchState.Playing)
            return;

        ElapsedTime += delta;
    }

    public void AddExp(int amount)
    {
        if (CurrentState != MatchState.Playing)
            return;

        Exp += amount;

        if (Exp >= PlayerLevel * 10)
        {
            PlayerLevel++;
            Exp = 0;
        }
    }
}
