using DG.Tweening;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private Rigidbody2D rb;

    private Collider2D col;

    [SerializeField] private Conveyor currentConveyor;

    [SerializeField] private BallManager ballManager;

    /// <summary>
    /// ボールリスポーン時に発火するイベント
    /// </summary>
    public event System.Action OnBallRespawn;

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

        ballManager = manager;
    }

    /// <summary>
    /// 生成アニメーション準備
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

            dir = (currentConveyor.IsMovingRight) ? Vector2.right : Vector2.left;

            rb.linearVelocity = dir * currentConveyor.ConveyorSpeed;

            rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0f);

            rb.gravityScale = 0f;

            rb.angularVelocity = 0f;
        }
        else
        {
            // ベルトに乗っていない場合は重力を有効にする
            rb.gravityScale = 1f;
        }

        if (this.transform.position.y < resetTriggerPosY)
        {
            PositionReset();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            if (transform.position.y > conveyor.transform.position.y + 0.2f)
            {
                currentConveyor = conveyor;
            }
        }

        if (collision.gameObject.CompareTag(Tags.Trap))
        {
            // ボールのゲームオーバーイベントを発火
            OnBallGameOver?.Invoke();
        }

        if (collision.gameObject.TryGetComponent(out DisappearingBlock block))
        {
            block.StartDisappearing();
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Conveyor conveyor))
        {
            if (conveyor == currentConveyor)
            {
                currentConveyor = null;
            }
        }
    }

    private void PositionReset()
    {
        OnBallGameOver?.Invoke();
    }
}
