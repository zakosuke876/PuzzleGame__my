using System;
using UnityEngine;
using UnityEngine.InputSystem;

public enum GameState
{
    Title,
    Game,
    Pause,
    GameClear,
    GameOver,
    Reset
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

    private void OnDestroy()
    {
        // 破棄時に自分がInstanceの場合片付ける
        if (Instance == this)
        {
            Instance = null;
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

    // 最初はタイトル状態
    [SerializeField] private GameState currentState = GameState.Title;

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

    private void Update()
    {
        // Pキーでポーズ
        if (Keyboard.current.pKey.wasPressedThisFrame)
        {
            TogglePose();
        }

        // エスケープキーでゲーム終了
        if (Keyboard.current.escapeKey.wasPressedThisFrame)
        {
            Application.Quit();
        }
    }

    /// <summary>
    /// Pause状態とGame状態を切り替える
    /// </summary>
    private void TogglePose()
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
        ApplyTimeScale(state);
        OnStateChanged?.Invoke(state);
    }


    /// <summary>
    /// 状態に応じて時間の流れを切り替える（ポーズ中のみ停止）
    /// </summary>
    private void ApplyTimeScale(GameState state)
    {
        Time.timeScale = (state == GameState.Pause) ? 0f : 1f;
    }

    /// <summary>
    /// ゲームをリトライ(カウントダウンを挟む)
    /// </summary>
    public void Retry()
    {
        ChangeState(GameState.Reset);

        StartCountDownOnce(ballManager.Retry);
    }
}
