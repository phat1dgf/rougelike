using UnityEngine;

public class SpawnSubsystem : SceneSubsystem
{
    private float spawnTimer;
    private float spawnInterval = 2f;

    public override void Tick(float deltaTime)
    {
        if (GameState.IsGameOver)
            return;

        spawnTimer += deltaTime;

        if (spawnTimer >= spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemy();
        }
    }

    private void SpawnEnemy()
    {
        
    }
}
