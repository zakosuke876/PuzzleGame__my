using UnityEngine;
using DG.Tweening;
using Unity.VisualScripting;

public class SpikeHead : MonoBehaviour
{
    [Header("スタート位置"), SerializeField]
    private Vector2 startPos;

    [Header("ゴール位置"), SerializeField]
    private Vector2 endPos;

    [Header("前進にかかる時間"), SerializeField]
    private float forwardTime = 1f;

    [Header("後退にかかる時間"), SerializeField]
    private float backTime = 1f;

    [Header("待機時間"), SerializeField]
    private float waitTime = 1f;

    [Header("イージング(前進)"), SerializeField]
    private Ease forwardEase = Ease.OutQuad;

    [Header("イージング(後退)"), SerializeField]
    private Ease backEase = Ease.InBack;

    // Z座標を保持
    private float originalZ;

    private bool isGame = false;

    // 初期化済みフラグ
    private bool isInitialized = false;

    public bool IsInitialized
    {
        get { return isInitialized; }
    }

    private DG.Tweening.Sequence seq;

    /// <summary>
    /// 初期化処理(位置リセット・状態設定)
    /// ※1度だけ実行される
    /// </summary>
    public void Initialize()
    {
        isGame = true;

        // 初期化済み状態にする
        isInitialized = true;

        // 元のZ座標を保存
        originalZ = transform.position.z;

        // スタート地点に配置
        transform.position = new Vector3(startPos.x, startPos.y, originalZ);
    }

    /// <summary>
    /// SpikeHeadギミックを作動
    /// </summary>
    public void SpikeHeadMove()
    {
        seq = DOTween.Sequence();

        seq.Append(transform.DOMove(endPos, forwardTime).SetEase(forwardEase))
                .AppendInterval(waitTime)
                .Append(transform.DOMove(startPos, backTime).SetEase(backEase))
                .AppendInterval(waitTime)
                .SetLoops(-1);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }

    /// <summary>
    /// ギミック動作を再開
    /// </summary>
    public void DoPlay()
    {
        seq?.Play();
        isGame = true;
    }

    /// <summary>
    /// ギミック動作を一時停止
    /// </summary>
    public void DoStop()
    {
        seq?.Pause();
        isGame = false;
    }

    public void ResetGimmick()
    {
        seq?.Kill();

        seq = null;

        isInitialized = false;

        isGame = false;

        transform.position = new Vector3(startPos.x, startPos.y, originalZ);
    }
}
