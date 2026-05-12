using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Gems;

public class GemUI : MonoBehaviour
{
    [SerializeField] private Image[] gemImages;

    private ItemManager ItemManager;

    private void Start()
    {
        // GemColorをリセット
        InitializeGemColor();
    }

    /// <summary>
    /// イベントの登録・UI表示の初期化
    /// </summary>
    public void Initialize(ItemManager itemManager)
    {
        // Gem取得時にUI更新を行う
        itemManager.OnGemCollected += UpdateGemColor;

        // UI表示をリセットする
        itemManager.OnGemReset += ResetGemColor;

        InitializeGemColor();
    }

    /// <summary>
    /// GemColorを初期化する
    /// </summary>
    private void InitializeGemColor()
    {
        foreach (var gemImage in gemImages)
        {
            gemImage.color = Color.black;
        }
    }

    /// <summary>
    /// UIのGemの色をリセットする
    /// </summary>
    public void ResetGemColor()
    {
        foreach (var gemImage in gemImages)
        {
            gemImage.color = Color.black;
        }
    }

    /// <summary>
    /// Gemが取得された際にGemColorを更新する
    /// </summary>
    public void UpdateGemColor(Gems.GemType gemType)
    {
        switch (gemType)
        {
            case GemType.Red:

                gemImages[0].color = Color.white;

                break;

            case GemType.Yellow:

                gemImages[1].color = Color.white;

                break;

            default:

                break;
        }
    }

    private void OnDisable()
    {
        if (ItemManager == null) return;

        // イベント登録解除
        ItemManager.OnGemCollected -= UpdateGemColor;
        ItemManager.OnGemReset -= ResetGemColor;
    }
}
