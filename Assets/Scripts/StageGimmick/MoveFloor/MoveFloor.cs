using UnityEngine;

public class MoveFloor : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;

    [SerializeField] private float maxPosY = 0;

    [SerializeField] private float minPosY = 0;

    [SerializeField] private float moveSpeed = 0f;

    // 移動方向を受け取る
    private float moveDir;
    
    private void FixedUpdate()
    {
        // 0の場合処理しない
        if (moveDir == 0) return;

        // 移動先を計算
        Vector2 target = rb.position + Vector2.up * (moveDir * moveSpeed * Time.fixedDeltaTime);

        // 移動できる範囲を制限
        target.y = Mathf.Clamp(target.y, minPosY, maxPosY);

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
