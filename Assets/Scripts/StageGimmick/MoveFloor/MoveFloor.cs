using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float moveSpeed = 0f;

    // 開始位置から上に動ける距離
    [SerializeField] private float upRange;

    // 開始位置から下に動ける距離
    [SerializeField] private float downRange;

    private float startY;

    // 移動方向を受け取る
    private float moveDir;

    private void Awake()
    {
        startY = rb.position.y;
    }

    private void FixedUpdate()
    {
        // 0の場合処理しない
        if (moveDir == 0) return;

        // 移動先を計算
        Vector2 target = rb.position + Vector2.up * (moveDir * moveSpeed * Time.fixedDeltaTime);

        // 移動できる範囲を制限
        target.y = Mathf.Clamp(target.y, startY - downRange, startY + upRange);

        // 物理と同期して移動
        rb.MovePosition(target);

        // 方向をリセット
        moveDir = 0f;
    }

    /// <summary>
    /// 移動方向を受け取る
    /// </summary>
    public void Move(float direction)
    {
        moveDir = direction;
    }
}
