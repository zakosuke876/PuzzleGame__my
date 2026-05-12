using UnityEngine;

public class Gems : MonoBehaviour
{
    public enum GemType
    {
        Gray,
        Orange,
        Red,
        Blue,
        Yellow
    }

    // Gemの種類
    [SerializeField] private GemType gemType;

    // Gemの表示/非表示を制御するRenderer
    [SerializeField] private Renderer gemRenderer;

    // リセット時に使う初期位置
    private Vector3 initialPosition;

    // 取得済みかどうか
    private bool isCollected = false;

    /// <summary>
    /// Gem取得時に発火するイベント
    /// 引数：取得されたGemの種類
    /// </summary>
    public event System.Action<GemType> OnCollected;

    // アイテム取得時に鳴らす効果音
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    [Header("SEの音量"), SerializeField] private float volume = 1f;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    /// <summary>
    /// 初期化(初期位置の保存・Gemの状態をリセット)
    /// </summary>
    public void Initialize()
    {
        // 初期位置を保存
        initialPosition = transform.position;
        ResetGems();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Tags.Ball) && !isCollected)
        {
            CollectItem();
        }
    }

    /// <summary>
    /// Gemを取得した際の処理
    /// (非表示化・イベント通知)
    /// </summary>
    private void CollectItem()
    {
        isCollected = true;

        audioSource.PlayOneShot(audioClip, volume);

        // 非表示状態にする
        gemRenderer.enabled = false;

        // アイテム取得イベントを発火
        OnCollected?.Invoke(gemType);
    }

    /// <summary>
    /// Gemの状態を初期化する(表示・位置)
    /// </summary>
    public void ResetGems()
    {
        if (gemRenderer == null) return;

        // 表示状態にする
        gemRenderer.enabled = true;

        isCollected = false;

        // 初期位置に戻す
        transform.position = initialPosition;
    }
}
