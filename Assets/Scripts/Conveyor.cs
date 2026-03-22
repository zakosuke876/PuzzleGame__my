using UnityEngine;

public class Conveyor : MonoBehaviour
{
    [Header("Trueなら右向き"), SerializeField]
    private bool moveRight = true;

    // プロパティ
    public bool MoveRight
    {
        get
        {
            return moveRight;
        }
        set
        {
            moveRight = value;
        }
    }

    [Header("ベルトコンベア画像格納配列"), SerializeField]
    private Sprite[] conveyorSprites;

    [Header("画像切り替え間隔"), SerializeField]
    private float conveyorSpriteInterval;

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

    private void ConveyorAnimation()
    {
        // スプライトの配列チェック
        if (conveyorSprites == null || conveyorSprites.Length < 4) return;

        if (spriteRenderer == null) return;

        conveyorTimer += Time.deltaTime;

        // 指定時間を超えたらスプライトヲ切り替える
        if (conveyorTimer > conveyorSpriteInterval)
        {
            // コンベアの初期状態の向きによってスタート位置を変える
            int baseIndex;

            if (moveRight)
            {
                baseIndex = 0;
            }
            else
            {
                baseIndex = 2;
            }

            // 
            conveyorSpriteIndex = (conveyorSpriteIndex + 1) % 2;

            //
            spriteRenderer.sprite = conveyorSprites[baseIndex + conveyorSpriteIndex];

            // タイマーをリセット
            conveyorTimer = 0f;
        }
    }
}
