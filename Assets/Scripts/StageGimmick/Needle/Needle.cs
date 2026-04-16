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

    private bool isGame = false;

    // 初期化済みフラグ
    private bool isInitialized = false;

    public bool IsInitialized
    {
        get { return isInitialized; }
    }

    private Sequence verticalSeq;

    private Sequence horizontalSeq;


    public void Initialize()
    {
        isGame = true;

        // 初期化済み状態にする
        isInitialized = true;

        // 元のZ座標を保存
        originalZ = transform.position.z;
    }

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

    public void MoveNeedleVertical()
    {
        transform.position = new Vector3(verticalStartPos.x, verticalStartPos.y, originalZ);

        verticalSeq = DOTween.Sequence();

        verticalSeq.Append(transform.DOMove(verticalEndPos, moveDuration))
                         .AppendInterval(delayTime)
                         .Append(transform.DOMove(verticalStartPos, moveDuration))
                         .SetLoops(-1);
    }

    private void OnDestroy()
    {
        transform.DOKill();
    }

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
        isGame = true;
    }

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
        isGame = false;
    }

    public void ResetGimmick()
    {
        verticalSeq?.Kill();

        verticalSeq = null;

        horizontalSeq?.Kill();

        horizontalSeq = null;

        isInitialized = false;

        isGame = false;

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
