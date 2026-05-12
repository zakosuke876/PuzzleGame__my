using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collider2D col;

    [SerializeField] private Conveyor currentConveyor;

    [SerializeField] private BallManager ballManager;

    /// <summary>
    /// ゲームオーバー時に発火するイベント
    /// </summary>
    public event System.Action OnBallGameOver;

    [Header("この座標より下に落ちたらリセット"), SerializeField]
    private float resetTriggerPosY;

    [Header("破壊設定")]
    [SerializeField] private float destroyDuration = 0.3f;

    public void Initialize(BallManager manager)
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();

        if (col != null)
        {
            // 当たり判定無し
            col.enabled = false;
        }

        // BallManagerの参照を渡す
        ballManager = manager;
    }

    /// <summary>
    /// 生成アニメーション準備
    /// 物理演算を無効化
    /// </summary>
    public void PrepareSpawn()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        col.enabled = false;
        transform.localScale = Vector3.zero;
    }

    /// <summary>
    /// 物理演算を有効化
    /// </summary>
    public void EnablePhysics()
    {
        col.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }

    /// <summary>
    /// 物理演算を停止
    /// </summary>
    public void DisablePhysics()
    {
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void FixedUpdate()
    {
        if (currentConveyor != null)
        {
            Vector2 dir;

            // 取得したコンベアオブジェクトのflagによって右・左向きを判別する
            dir = (currentConveyor.IsMovingRight) ? Vector2.right : Vector2.left;

            // 速度を設定
            rb.linearVelocity = dir * currentConveyor.ConveyorSpeed;

            // Y方向の速度を0にする
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            // 重力を無効化
            rb.gravityScale = 0f;

            // 回転速度を0にする
            rb.angularVelocity = 0f;
        }
        else
        {
            // コンベアに乗っていない場合は重力を有効にする
            rb.gravityScale = 1f;
        }

        // 指定した座標よりも下に落ちたら
        if (this.transform.position.y < resetTriggerPosY)
        {
            PositionReset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // コンベアに接触した場合
        if (collision.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            // 上からコンベアに乗った場合
            if (transform.position.y > conveyor.transform.position.y + 0.2f)
            {
                // スクリプト取得
                currentConveyor = conveyor;
            }
        }

        // Trapに接触した場合はゲームオーバー
        if (collision.gameObject.CompareTag(Tags.Trap))
        {
            // ボールのゲームオーバーイベントを発火
            OnBallGameOver?.Invoke();
        }

        // 消えるブロックに接触した場合
        if (collision.gameObject.TryGetComponent(out DisappearingBlock block))
        {
            block.StartDisappearing();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // コンベアから離れた場合コンベア参照を解除
        if (collision.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            // 乗っていたコンベアと一致する場合のみ解除
            if (conveyor == currentConveyor)
            {
                currentConveyor = null;
            }
        }
    }

    /// <summary>
    /// ボールが落下時にゲームオーバーイベントを発火
    /// <see cref="BallManager.GameOver"/>
    /// </summary>
    private void PositionReset()
    {
        OnBallGameOver?.Invoke();
    }
}
