using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using static Gems;

public class GemUI : MonoBehaviour
{
    [SerializeField] private Image[] gemImages;

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
}
