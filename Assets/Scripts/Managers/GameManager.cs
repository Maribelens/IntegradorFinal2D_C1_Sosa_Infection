using System;
using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    //[SerializeField] private GameManager gameManager;
    public enum GameState { Playing, Paused, GameOver, Victory }
    [SerializeField] private GameState currentState;

    [Header ("Infection values")]
    public GameState CurrentState => currentState;

    [Range(0,1)]
    [SerializeField] private float infection01 =0f; //visible en Inspector

    [SerializeField] private float infectionStep = 0.05f;   //cuando sube cada tick
    [SerializeField] private float infectionInterval = 2f;    //cada cuantos segundos sube 

    public float Infection01 => infection01;
    public float InfectionPercent => infection01 * 100f;

    [Header("Infection Curve")]
    [SerializeField] private AnimationCurve infectionCurve = AnimationCurve.Linear(0, 0, 1, 1);

    public float InfectionEvaluated => infectionCurve.Evaluate(infection01);

    public event Action<GameState> OnStateChanged;
    public event Action<float> OnInfectionChanged;
    public event Action OnGameOver;
    public event Action OnVictory;

    private void Start()
    {
        SetGameState(GameState.Playing);
        StartCoroutine(InfectionRoutine());
    }

    private IEnumerator InfectionRoutine()
    {
        OnInfectionChanged?.Invoke(infection01);

        while (infection01 < 1f)
        {
            yield return new WaitForSeconds(infectionInterval);

            infection01 += infectionStep;
            infection01 = Mathf.Clamp01(infection01);

            OnInfectionChanged?.Invoke(infection01);
        }
    }

    public void SetGameState(GameState newState)
    {
        if (currentState == newState) return;
        currentState = newState;
        Debug.Log("Nuevo estado: " + currentState);

        OnStateChanged?.Invoke(currentState);

        switch (currentState)
        {
            case GameState.Playing:
                Time.timeScale = 1;
                break;

            case GameState.Paused:
                Time.timeScale = 0;
                break;

            case GameState.GameOver:
                Time.timeScale = 0;
                //PlayMusic(gameOverMusic);
                OnGameOver?.Invoke();
                break;

            case GameState.Victory:
                Time.timeScale = 0;
                //PlayMusic(victoryMusic);
                OnVictory?.Invoke();
                break;
        }

    }

    public void GameOver()
    {
        SetGameState(GameState.GameOver);
    }

    public void Victory()
    {
        SetGameState(GameState.Victory);
    }

    public void Pause()
    {
        SetGameState(GameState.Paused);
    }

    public void Resume()
    {
        SetGameState(GameState.Playing);
    }
}
