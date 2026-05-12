using DG.Tweening;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class BallManager : MonoBehaviour
{
    [SerializeField] private Ball ballPrefab;

    [Header("ボールの生成")]
    [SerializeField, Tooltip("ボールの生成位置")] private Vector2 spawnStartPosition; // 開始位置
    [SerializeField] private Vector2 spawnEndPosition;   // 最終位置

    [Header("アニメーション設定")]
    [SerializeField] private float spawnDuration = 1f;
    [SerializeField] private Ease spawnEase = Ease.OutBounce;

    // 現在生成されているボール
    private Ball currentBall;

    [Header("破壊設定")]
    [SerializeField] private float destroyDuration = 0.3f;

    [Header("衝突設定")]
    [SerializeField] private float collisionEnableDelay = 0.5f; // コライダー有効化までの時間

    // 再生成中かどうか
    private bool isRespawning = false;

    /// <summary>
    /// ゲームオーバー時に発火するイベント
    /// </summary>
    public event System.Action OnBallDead;


    /// <summary>
    /// リトライ時に発火するイベント
    /// </summary>
    public event System.Action OnBallRespawn;

    public event System.Action OnBallSpawned;


    public void Initialize()
    {
        // ステート変更を購読
        GameManager.Instance.OnStateChanged += HandleStateChange;

        isRespawning = false;

        SpawnBall();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        // ステート変更の購読解除
        GameManager.Instance.OnStateChanged -= HandleStateChange;

        if (currentBall != null )
        {
            currentBall.OnBallGameOver -= GameOver;
        }
    }

    /// <summary>
    /// ゲームステートの変更処理
    /// </summary>
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            

            case GameState.Game:

                break;

            case GameState.Pause:

                break;

            case GameState.Respawn:

                break;

            case GameState.GameOver:

                break;

            case GameState.Reset:

                if (currentBall != null)
                {
                    DestroyAnimation(() =>
                    {
                        isRespawning = false;
                    });
                }
                else
                {
                    isRespawning = false;
                }

                break;

            default:

                break;
        }
    }

    /// <summary>
    /// ボールを生成
    /// </summary>
    public void SpawnBall()
    {
        // ボールを生成
        currentBall = Instantiate(ballPrefab, spawnStartPosition, Quaternion.identity);

        isRespawning = true;

        // リスポーン中はアイテムを取得できないようにする
        currentBall.gameObject.layer = LayerMask.NameToLayer(Layers.NoItemCollision);

        currentBall.OnBallGameOver += GameOver;

        currentBall.Initialize(this);

        currentBall.PrepareSpawn();

        Sequence spawnSequence = DOTween.Sequence();

        spawnSequence.Append(currentBall.transform.DOMove(spawnEndPosition, spawnDuration).SetEase(spawnEase))
                     .Join(currentBall.transform.DOScale(new Vector3(0.7f, 0.7f, 1f), spawnDuration).SetEase(Ease.OutBack))
                     .AppendInterval(collisionEnableDelay)
                     .OnComplete(() =>
                     {
                         // アイテムを取得できる状態に変更
                         currentBall.gameObject.layer = LayerMask.NameToLayer(Layers.Player);
                         currentBall.EnablePhysics();
                         isRespawning = false;
                         OnBallSpawned?.Invoke();
                     });
    }

    public void DestroyAnimation(Action onComplete = null)
    {
        if (currentBall == null) return;

        currentBall.transform.DOKill();

        currentBall.DisablePhysics();

        currentBall.OnBallGameOver -= GameOver;

        // 破壊アニメーション開始
        currentBall.transform.DOScale(Vector3.zero, destroyDuration)
                   .SetEase(Ease.InBack)
                   .OnComplete(() =>
                   {
                       Destroy(currentBall.gameObject);
                       onComplete?.Invoke();
                   });
    }

    /// <summary>
    /// ゲーム状態をリスポーンに変更
    /// ボールを再生成
    /// </summary>
    public void RespawnBall()
    {
        if (currentBall == null) return;

        if (isRespawning) return;

        isRespawning = true;

        currentBall.gameObject.layer = LayerMask.NameToLayer(Layers.NoItemCollision);

        OnBallRespawn?.Invoke();

        DestroyAnimation(SpawnBall);
    }

    public void GameOver()
    {
        if (currentBall == null) return;

        if (isRespawning) return;

        isRespawning = true;

        DestroyAnimation(() =>
        {
            OnBallDead?.Invoke();
        });
    }

    public void Retry()
    {
        if (currentBall != null)
        {
            DestroyAnimation(SpawnBall);
        }
        else
        {
            SpawnBall();
        }
    }
}
