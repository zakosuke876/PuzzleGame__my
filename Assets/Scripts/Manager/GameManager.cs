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

#if UNITY_EDITOR
    [Header("--- Debug ---")]
    [SerializeField] private GameState debugState;

    [ContextMenu("Force Change State")]
    private void DebugChangeState()
    {
        ChangeState(debugState);
    }
#endif

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

    [SerializeField] private GimmickManger gimmickManager;

    [SerializeField] private ItemManager itemManager;

    [SerializeField] private BallManager ballManager;

    [SerializeField] private CountdownUI countdownUI;

    /// <summary>
    /// ゲームステート変更時に発火するイベント
    /// </summary>
    public event Action<GameState> OnStateChanged;

    // 最初はタイトル状態
    [SerializeField] private GameState currentState = GameState.Title;

    /// <summary>
    /// ボールがリスポーン要求された時
    /// </summary>
    /// <see cref="BallManager.OnBallRespawn"/>
    private void HandleBallRespawned() => ChangeState(GameState.Respawn);

    /// <summary>
    /// ボールが死亡した時
    /// </summary>
    /// <see cref="BallManager.OnBallDead"/>
    private void HandleBallDead() => ChangeState(GameState.GameOver);

    /// <summary>
    /// ボール生成完了時
    /// </summary>
    /// <see cref="BallManager.OnBallSpawned"/>
    private void HandleBallSpawned() => ChangeState(GameState.Game);
    private void Start()
    {
        if (countdownUI != null)
        {
            StartCountDownOnce(Initialize);
        }
    }

    /// <summary>
    /// カウントダウンを開始し、終了後に1度onFinishedを呼ぶ
    /// </summary>
    private void StartCountDownOnce(Action onFinished)
    {
        void Handler()
        {
            countdownUI.OnCountDownFinished -= Handler;
            onFinished();
        }

        countdownUI.OnCountDownFinished += Handler;
        countdownUI.StartCountdown();
    }

    private void OnEnable()
    {
        // 重複インスタンス(Awakeで破棄予定)は購読しない
        if (Instance != this) return;

        // ボールの状態変化イベントを購読
        ballManager.OnBallRespawn += HandleBallRespawned;
        ballManager.OnBallDead += HandleBallDead;
        ballManager.OnBallSpawned += HandleBallSpawned;
    }

    private void OnDisable()
    {
        // 購読していない場合は処理しない
        if (Instance != this) return;

        // イベント購読解除
        ballManager.OnBallRespawn -= HandleBallRespawned;
        ballManager.OnBallDead -= HandleBallDead;
        ballManager.OnBallSpawned -= HandleBallSpawned;


        //countdownUI.OnCountDownFinished -= Initialize;
        //countdownUI.OnCountDownFinished -= RetryStart;
    }

    private void Update()
    {
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
        if (ballManager != null)
        {
            ballManager.Initialize();
        }

        if (itemManager != null)
        {
            itemManager.Initialize();
        }

        if (gimmickManager != null)
        {
            gimmickManager.Initialize();
        }

        // 状態変更
        ChangeState(currentState);
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

        StartCountDownOnce(ballManager.Retry);
    }

    /// <summary>
    /// カウントダウン完了後にリトライ
    /// </summary>
    /*public void RetryStart()
    {
        countdownUI.OnCountDownFinished -= RetryStart;

        ballManager.Retry();
    }*/
}
