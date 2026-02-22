using UnityEngine;

public static class GameEvents
{
    public readonly struct SceneBegin
    {
        public readonly string A;
        public SceneBegin(string a)
        {
            A = a;
        }
    }
    public readonly struct MatchStateChanged
    {
        public readonly MatchState State;
        public MatchStateChanged(MatchState state)
        {
            State = state;
        }
    }
}
