using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [Header("Trueなら右向き"), SerializeField]
    private bool isMovingRight = true;

    // 移動方向を取得・設定するプロパティ
    public bool IsMovingRight
    {
        get { return isMovingRight; }
        set { isMovingRight = value; }
    }

    [Header("コンベア移動速度"), SerializeField]
    private float conveyorSpeed = 0f;

    // コンベアの流れるスピードを取得するプロパティ
    public float ConveyorSpeed
    {
        get { return conveyorSpeed; }
    }

    [Header("ベルトコンベア画像格納配列"), SerializeField]
    private Sprite[] conveyorSprites = new Sprite[4];

    [Header("スプライト切り替え間隔"), SerializeField]
    private float conveyorSpriteInterval;

    // アニメーションのフレーム切り替えを管理するタイマー
    private float conveyorTimer = 0f;
    private int conveyorSpriteIndex = 0;

    private SpriteRenderer spriteRenderer;

    void Start()
    {
        Initialize();
    }

    /// <summary>
    /// 初期化用関数
    /// </summary>
    private void Initialize()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        ConveyorAnimation();
    }

    /// <summary>
    /// スプライトを変更してコンベアの流れる向きを表現する関数
    /// </summary>
    private void ConveyorAnimation()
    {
        // スプライトの配列チェック(要素数は4で固定)
        if (conveyorSprites == null || conveyorSprites.Length != 4) return;

        if (spriteRenderer == null) return;

        conveyorTimer += Time.deltaTime;

        // 指定時間を超えたらスプライトを切り替える
        if (conveyorTimer > conveyorSpriteInterval)
        {
            int baseIndex;

            // 配列の0～1は右向き,2～3は左向きのスプライト
            // 向きに応じて開始位置を切り替える
            if (isMovingRight)
            {
                baseIndex = 0;
            }
            else
            {
                baseIndex = 2;
            }

            
            conveyorSpriteIndex = (conveyorSpriteIndex + 1) % 2;

            // スプライトを切り替える
            spriteRenderer.sprite = conveyorSprites[baseIndex + conveyorSpriteIndex];

            // タイマーをリセット
            conveyorTimer = 0f;
        }
    }
}
