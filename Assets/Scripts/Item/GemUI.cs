using UnityEngine;
using UnityEngine.UI;
using static Gems;

public class GemUI : MonoBehaviour
{
    [SerializeField] private Image[] gemImages;

    [SerializeField] private ItemManager itemManager;

    private void Start()
    {
        // GemColorをリセット
        InitializeGemColor();
    }

    private void OnEnable()
    {
        if (itemManager == null) return;

        itemManager.OnGemCollected += UpdateGemColor;
        itemManager.OnGemReset += ResetGemColor;
    }

    private void OnDisable()
    {
        if (itemManager == null) return;

        // イベント登録解除
        itemManager.OnGemCollected -= UpdateGemColor;
        itemManager.OnGemReset -= ResetGemColor;
    }


    /// <summary>
    /// GemColorを初期化する
    /// </summary>
    private void InitializeGemColor()
    {
        Debug.Log("終了");
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

                // 色を付けて表示状態にする
                gemImages[0].color = Color.white;

                break;

            case GemType.Yellow:

                gemImages[1].color = Color.white;

                break;

            default:

                break;
        }
    }
}
