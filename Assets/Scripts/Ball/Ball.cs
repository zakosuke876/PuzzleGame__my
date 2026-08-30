using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collider2D col;

    private Conveyor currentConveyor;

    [SerializeField] private BallManager ballManager;

    // ゲームオーバーイベント発火済みフラグ
    bool isDead = false;

    // 物理が有効状態かどうかを表すフラグ
    bool physicsEnabled = false;

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
        isDead = false;
        physicsEnabled = false;

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
        physicsEnabled = true;
        col.enabled = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.gravityScale = 1f;
    }

    /// <summary>
    /// 物理演算を停止
    /// </summary>
    public void DisablePhysics()
    {
        physicsEnabled = false;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = 0f;
    }

    private void FixedUpdate()
    {
        if (!physicsEnabled) return;

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
        if (!isDead && this.transform.position.y < resetTriggerPosY)
        {
            isDead = true;

            /// ボールが落下時にゲームオーバーイベントを発火
            OnBallGameOver?.Invoke();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 死亡後は処理しない
        if (isDead) return; 

        ContactPoint2D contact = collision.GetContact(0);

        // 上側から触れた場合
        bool hitFromUp = contact.normal.y > 0.7f;

        if (collision.collider.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            if (hitFromUp)
            {
                currentConveyor = conveyor;
            }
        }

        // Trapに接触した場合はゲームオーバー
        if (collision.collider.gameObject.CompareTag(Tags.Trap))
        {
            // イベント発火済みに変更
            isDead = true;

            // ボールのゲームオーバーイベントを発火
            OnBallGameOver?.Invoke();
        }

        // 消えるブロックに接触した場合
        if (collision.collider.gameObject.TryGetComponent(out DisappearingBlock block))
        {
            if (hitFromUp)
            {
                block.StartDisappearing();
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        // コンベアから離れた場合コンベア参照を解除
        if (collision.collider.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            // 乗っていたコンベアと一致する場合のみ解除
            if (conveyor == currentConveyor)
            {
                currentConveyor = null;
            }
        }
    }
}
