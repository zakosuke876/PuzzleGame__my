using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Title,
    Game,
    Pause,
    GameOver
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    [SerializeField] private GimmickManager gimmickManager;

    [SerializeField] private ItemManager itemManager;

    [SerializeField] private BallManager ballManager;

    /// <summary>
    /// ゲームステート変更時に発火するイベント
    /// </summary>
    public event Action<GameState> OnStateChanged;

    [SerializeField] private GameState currentState;
    private void Start()
    {
        Initialize();
    }

    private void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            ballManager.RespawnBall();
        }
    }

    private void Initialize()
    {
        itemManager.Initialize();
        ballManager.Initialize();
        gimmickManager.Initialize();
        ballManager.OnBallDead += HandleBallDead;
        ChangeState(currentState);
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
        OnStateChanged?.Invoke(state);
    }


    /// <summary>
    /// ゲームステート(GameOverに変更)
    /// </summary>
    private void HandleBallDead()
    {
        ChangeState(GameState.GameOver);
    }

    private void OnDestroy()
    {
        ballManager.OnBallDead -= HandleBallDead;
    }
}
