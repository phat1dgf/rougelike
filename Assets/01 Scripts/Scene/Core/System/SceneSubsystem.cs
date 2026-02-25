using UnityEngine;

public abstract class SceneSubsystem
{
    protected GameMode GameMode;
    protected GameState GameState;

    public void Inject(GameMode gameMode, GameState gameState)
    {
        GameMode = gameMode;
        GameState = gameState;
    }

    public virtual void Initialize() { }
    public virtual void Tick(float deltaTime) { }
    public virtual void Dispose() { }
}

