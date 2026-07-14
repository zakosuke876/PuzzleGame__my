using UnityEngine;
using DG.Tweening;
using UnityEngine.InputSystem;

public class Saw : MonoBehaviour
{
    [Header("スタート地点"), SerializeField]
    private Vector2 startPos;

    [Header("ゴール地点"), SerializeField]
    private Vector2 goalPos;

    [Header("移動時間"), SerializeField]
    private float duration = 3f;

    [Header("イージング"), SerializeField]
    private Ease easeType = Ease.Linear;

    [Header("待機時間"), SerializeField]
    private float waitTime = 1f;

    // Z座標を保持
    private float originalZ;

    // 初期化済みフラグ
    private bool isInitialized = false;

    private Sequence seq;

    private void Awake()
    {
        // 元のZ座標を保存
        originalZ = transform.position.z;
    }

    private void Initialize()
    {
        // 初期化済み状態にする
        isInitialized = true;

        // スタート地点に配置
        transform.position = new Vector3(startPos.x, startPos.y, originalZ);
        SawMove();
    }

    private void SawMove()
    {
        Vector3 start = new Vector3(startPos.x, startPos.y, originalZ);
        Vector3 goal = new Vector3(goalPos.x, goalPos.y, originalZ);

        seq = DOTween.Sequence();

        seq.Append(transform.DOMove(goal, duration).SetEase(easeType))
           .AppendInterval(waitTime)
           .Append(transform.DOMove(start, duration).SetEase(easeType))
           .AppendInterval(waitTime)
           .SetLoops(-1)
           .SetLink(gameObject);
    }

    /// <summary>
    /// オブジェクト破棄時にTweenを全停止
    /// </summary>
    void OnDestroy()
    {
        // 保険のKill
        seq?.Kill();
    }

    /// <summary>
    /// 一時停止中のシーケンスを再開する
    /// </summary>
    public void DoPlay()
    {
        if (!isInitialized)
        {
            Initialize();
        }
        else
        {
            seq?.Play();
        }
    }

    /// <summary>
    /// シーケンスを一時停止する
    /// </summary>
    public void DoPause()
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
