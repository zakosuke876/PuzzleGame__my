using DG.Tweening;
using UnityEngine;

public class Needle : MonoBehaviour, IGimmick
{
    [Header("横移動設定")]

    [Header("スタート位置"), SerializeField]
    private Vector2 horizontalStartPos;

    [Header("ゴール位置"), SerializeField]
    private Vector2 horizontalEndPos;

    [Header("表示位置"), SerializeField]
    private Vector2 visiblePos;

    [Header("隠す位置"), SerializeField]
    private Vector2 hiddenPos;

    [Header("表示時間"), SerializeField]
    private float showTime;

    [Space]

    [Header("縦移動設定")]

    [Header("スタート位置"), SerializeField]
    private Vector2 verticalStartPos;

    [Header("ゴール位置"), SerializeField]
    private Vector2 verticalEndPos;

    [Header("移動時間"), SerializeField]
    private float moveDuration;

    [Header("待機時間"), SerializeField]
    private float delayTime;

    [Space]

    [Header("Trueなら縦移動"), SerializeField]
    private bool isVertical;

    // Z座標を保持
    private float originalZ;

    // 初期化済みフラグ
    private bool isInitialized = false;

    // DOTweenのシーケンス(縦・横それぞれ保持して外部から制御する)
    private Sequence verticalSeq;

    private Sequence horizontalSeq;

    private void Initialize()
    {
        // 初期化済み状態にする
        isInitialized = true;

        // 元のZ座標を保存
        originalZ = transform.position.z;
    }

    /// <summary>
    /// isVerticalの設定に応じて縦または横移動する
    /// </summary>
    private void Move()
    {
        if (isVertical)
        {
            MoveNeedleVertical();
        }
        else
        {
            MoveNeedleHorizontal();
        }
    }

    /// <summary>
    /// 横移動のTweenシーケンスを作成してループ再生する
    /// </summary>
    private void MoveNeedleHorizontal()
    {
        transform.position = new Vector3(horizontalStartPos.x, horizontalStartPos.y, originalZ);

        horizontalSeq = DOTween.Sequence();

        horizontalSeq.Append(transform.DOMove(visiblePos, showTime))
                           .Append(transform.DOMove(horizontalEndPos, moveDuration))
                           .Append(transform.DOMove(hiddenPos, showTime))
                           .Append(transform.DOMove(horizontalStartPos, moveDuration))
                           .SetLoops(-1)
                           .SetLink(gameObject); // このgameObjectが破棄された時にTweenも自動でKillされるようにする
    }

    /// <summary>
    /// 縦移動のTweenシーケンスを作成してループ再生する
    /// </summary>
    private void MoveNeedleVertical()
    {
        transform.position = new Vector3(verticalStartPos.x, verticalStartPos.y, originalZ);

        verticalSeq = DOTween.Sequence();

        verticalSeq.Append(transform.DOMove(verticalEndPos, moveDuration))
                         .AppendInterval(delayTime)
                         .Append(transform.DOMove(verticalStartPos, moveDuration))
                         .SetLoops(-1)
                         .SetLink(gameObject);
    }

    public void Play()
    {
        if (!isInitialized)
        {
            Initialize();
            Move();
        }
        else
        {
            if (isVertical)
            {
                verticalSeq?.Play();
            }
            else
            {
                horizontalSeq?.Play();
            }
        }
    }

    /// <summary>
    /// シーケンスを一時停止する
    /// </summary>
    public void Stop()
    {
        if (isVertical)
        {
            verticalSeq?.Pause();
        }
        else
        {
            horizontalSeq?.Pause();
        }
    }

    /// <summary>
    /// ギミックをリセットする（Tweenを破棄し、初期位置に戻す）
    /// </summary>
    public void ResetGimmick()
    {
        // シーケンスを破棄
        verticalSeq?.Kill();
        verticalSeq = null;
        horizontalSeq?.Kill();
        horizontalSeq = null;

        isInitialized = false;

        if (isVertical)
        {
            transform.position = new Vector3(verticalStartPos.x, verticalStartPos.y, originalZ);
        }
        else
        {
            transform.position = new Vector3(horizontalStartPos.x, horizontalStartPos.y, originalZ);
        }
    }
}
