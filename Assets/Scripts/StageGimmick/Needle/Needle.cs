using UnityEngine;
using DG.Tweening;
using UnityEngine.Rendering.UI;

public class Needle : MonoBehaviour
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

    /// <summary>
    /// 初期化済みかどうかを外から参照する
    /// </summary>
    public bool IsInitialized
    {
        get { return isInitialized; }
    }

    // DOTweenのシーケンス(縦・横それぞれ保持して外部から制御する)
    private Sequence verticalSeq;

    private Sequence horizontalSeq;

    /// <summary>
    /// 針の初期化処理
    /// </summary>
    public void Initialize()
    {
        // 初期化済み状態にする
        isInitialized = true;

        // 元のZ座標を保存
        originalZ = transform.position.z;
    }

    /// <summary>
    /// isVerticalの設定に応じて縦または横移動する
    /// </summary>
    public void Move()
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
    public void MoveNeedleHorizontal()
    {
        transform.position = new Vector3(horizontalStartPos.x, horizontalStartPos.y, originalZ);

        horizontalSeq = DOTween.Sequence();

        horizontalSeq.Append(transform.DOMove(visiblePos, showTime))
                           .Append(transform.DOMove(horizontalEndPos, moveDuration))
                           .Append(transform.DOMove(hiddenPos, showTime))
                           .Append(transform.DOMove(horizontalStartPos, moveDuration))
                           .SetLoops(-1);
    }

    /// <summary>
    /// 縦移動のTweenシーケンスを作成してループ再生する
    /// </summary>
    public void MoveNeedleVertical()
    {
        transform.position = new Vector3(verticalStartPos.x, verticalStartPos.y, originalZ);

        verticalSeq = DOTween.Sequence();

        verticalSeq.Append(transform.DOMove(verticalEndPos, moveDuration))
                         .AppendInterval(delayTime)
                         .Append(transform.DOMove(verticalStartPos, moveDuration))
                         .SetLoops(-1);
    }

    /// <summary>
    /// オブジェクト破棄時にTweenを全停止してメモリリークを防ぐ
    /// </summary>
    private void OnDestroy()
    {
        transform.DOKill();
    }

    /// <summary>
    /// 一時停止中のシーケンスを再開する
    /// </summary>
    public void DoPlay()
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

    /// <summary>
    /// シーケンスを一時停止する
    /// </summary>
    public void DoStop()
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
