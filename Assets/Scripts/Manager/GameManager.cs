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
    GameOver,
    Reset
}

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private bool isPause = false;

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

    [SerializeField] private CountdownUI countdownUI;

    /// <summary>
    /// ゲームステート変更時に発火するイベント
    /// </summary>
    public event Action<GameState> OnStateChanged;

    [SerializeField] private GameState currentState = GameState.Title;

    private void OnBallRespawn() => ChangeState(GameState.Respawn);

    private void OnBallDead() => ChangeState(GameState.GameOver);

    private void OnBallSpawned() => ChangeState(GameState.Game);
    private void Start()
    {
        // カウントダウン完了後にInitializeを呼ぶ
        countdownUI.OnCountDownFinished += Initialize;

        countdownUI.StartCountdown();

        // ボールの状態変化イベントを購読
        ballManager.OnBallRespawn += OnBallRespawn;
        ballManager.OnBallDead += OnBallDead;
        ballManager.OnBallSpawned += OnBallSpawned;
    }

    private void OnDisable()
    {
        // イベント購読解除
        countdownUI.OnCountDownFinished -= Initialize;

        ballManager.OnBallRespawn -= OnBallRespawn;

        ballManager.OnBallDead -= OnBallDead;

        ballManager.OnBallSpawned -= OnBallSpawned;
    }

    private void Update()
    {
        // Rキーでリセット
        if (Keyboard.current.rKey.wasPressedThisFrame) ballManager.RespawnBall();

        // Pキーでポーズ
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            if (currentState == GameState.Pause)
            {
                ChangeState(GameState.Game);
            }
            else if (currentState == GameState.Game)
            {
                ChangeState(GameState.Pause);
            }
        }

        // エスケープキーでゲーム終了
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// カウントダウン完了後に各マネージャーを初期化してゲーム開始
    /// </summary>
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

        countdownUI.OnCountDownFinished -= Initialize;

    }

    /// <summary>
    /// ゲームステートを変更してイベントを通知
    /// </summary>
    /// <param name="state"></param>
    public void ChangeState(GameState state)
    {
        currentState = state;
        OnStateChanged?.Invoke(state);
    }

    /// <summary>
    /// ゲームをリトライ(カウントダウンを挟む)
    /// </summary>
    public void Retry()
    {
        ChangeState(GameState.Reset);

        countdownUI.OnCountDownFinished += RetryStart;

        countdownUI.StartCountdown();
    }

    /// <summary>
    /// カウントダウン完了後にリトライ
    /// </summary>
    public void RetryStart()
    {
        countdownUI.OnCountDownFinished -= RetryStart;

        ballManager.Retry();
    }
}
