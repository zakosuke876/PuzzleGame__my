using UnityEngine;
using DG.Tweening;

public class SpikeHead : MonoBehaviour, IGimmick
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

    // 初期化済みフラグ
    private bool isInitialized = false;

    private Sequence seq;

    /// <summary>
    /// 初期化処理(位置リセット・状態設定)
    /// ※1度だけ実行される
    /// </summary>
    private void Initialize()
    {
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
    private void SpikeHeadMove()
    {
        seq = DOTween.Sequence();

        seq.Append(transform.DOMove(endPos, forwardTime).SetEase(forwardEase))
                .AppendInterval(waitTime)
                .Append(transform.DOMove(startPos, backTime).SetEase(backEase))
                .AppendInterval(waitTime)
                .SetLoops(-1)
                .SetLink(gameObject); // このgameObjectが破棄された時にTweenも自動でKillされるようにする
    }

    /// <summary>
    /// 一時停止中のシーケンスを再開する
    /// </summary>
    public void Play()
    {
        if (!isInitialized)
        {
            Initialize();
            SpikeHeadMove();
        }
        else
        {
            seq?.Play();
        }
    }

    /// <summary>
    /// シーケンスを一時停止する
    /// </summary>
    public void Stop()
    {
        seq?.Pause();
    }

    /// <summary>
    /// ギミックをリセットする（Tweenを破棄し、初期位置に戻す）
    /// </summary>
    public void ResetGimmick()
    {
        seq?.Kill();

        seq = null;

        isInitialized = false;

        transform.position = new Vector3(startPos.x, startPos.y, originalZ);
    }
}
