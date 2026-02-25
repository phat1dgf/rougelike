using UnityEngine;

public class GameMode : MonoBehaviour
{
    [SerializeField] private GameState gameState;
    [SerializeField] private float matchDuration = 900f;
    [SerializeField] private float countdownTime = 3f;

    private float countdownTimer;

    private void Start()
    {
        StartCountdown();
    }

    private void Update()
    {
        switch (gameState.CurrentState)
        {
            case MatchState.Countdown:
                UpdateCountdown();
                break;

            case MatchState.Playing:
                UpdateMatch();
                break;
        }
    }

    private void StartCountdown()
    {
        countdownTimer = countdownTime;
        gameState.SetMatchState(MatchState.Countdown);
    }

    private void UpdateCountdown()
    {
        countdownTimer -= Time.deltaTime;

        if (countdownTimer <= 0f)
        {
            gameState.SetMatchState(MatchState.Playing);
        }
    }

    private void UpdateMatch()
    {
        if (gameState.ElapsedTime >= matchDuration)
        {
            EndGame();
        }
    }

    public void OnEnemyKilled(int expReward)
    {
        gameState.AddExp(expReward);
    }

    public void PauseGame()
    {
        gameState.SetMatchState(MatchState.Paused);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        gameState.SetMatchState(MatchState.Playing);
    }

    private void EndGame()
    {
        gameState.SetMatchState(MatchState.GameOver);
        Time.timeScale = 0f;
    }
}
