using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Title,
    Game,
    Pause,
    Respawn,
    GameClear,
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

        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            ChangeState(GameState.Pause);
        }

        if (Keyboard.current.gKey.wasPressedThisFrame)
        {
            ChangeState(GameState.Game);
        }

        if (Keyboard.current.oKey.wasPressedThisFrame)
        {
            ChangeState(GameState.GameOver);
        }
    }

    private void Initialize()
    {
        try
        {
            itemManager.Initialize();
            ballManager.Initialize();
            gimmickManager.Initialize();
        }
        catch (Exception e)
        {
            Debug.LogError($"初期化失敗:{e.Message}");
        }

        ChangeState(currentState);
    }

    public void ChangeState(GameState state)
    {
        currentState = state;
        OnStateChanged?.Invoke(state);
    }

    public void Retry()
    {
        ChangeState(GameState.Game);

        ballManager.Retry();
    }
}
