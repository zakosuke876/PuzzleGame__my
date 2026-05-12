using UnityEngine;

public class DisappearingBlock : MonoBehaviour
{
    [Header("消えるまでの時間"), SerializeField]
    private float disappearTIme = 0.8f;

    // 消滅処理中かどうかを管理するフラグ
    private bool disappearing = false;

    // 消滅までの時間を管理するタイマー
    private float timer = 0f;

    // 見た目の表示・非表示を制御
    [SerializeField] private SpriteRenderer spriteRenderer;

    // 当たり判定の有効・無効を制御
    [SerializeField] private Collider2D col;

    // ブロックが消えた(壊れた)時に鳴らす効果音
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    [Header("SEの音量"), SerializeField] private float volume = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        col = GetComponent<Collider2D>();
    }

    private void Update()
    {
        if (disappearing)
        {
            timer += Time.deltaTime;

            // 指定時間を超えた場合
            if (timer > disappearTIme)
            {
                HideBlock();
            }
        }
    }

    /// <summary>
    /// ブロックの消滅処理を開始する
    /// </summary>
    public void StartDisappearing()
    {
        disappearing = true;
    }

    /// <summary>
    /// 一定時間経過後にブロックを非表示にし、消滅状態を終了する(タイマー・フラグをリセット)
    /// </summary>
    private void HideBlock()
    {
        if (col != null)
        {
            // 当たり判定を無くす
            col.enabled = false;
        }

        if (spriteRenderer != null)
        {
            // 非表示にする
            spriteRenderer.enabled = false;
        }

        audioSource.PlayOneShot(audioClip, volume);
        disappearing = false;
        timer = 0f;
    }

    /// <summary>
    /// ゲームリトライ時などで呼ばれ、ブロックの状態(フラグ・表示・当たり判定)を初期状態にする
    /// </summary>
    public void ResetBlock()
    {
        disappearing = false;
        timer = 0f;

        if (spriteRenderer != null)
        {
            spriteRenderer.enabled = true;
        }

        if (col != null)
        {
            col.enabled = true;
        }
    }
}
