using System.Xml.Serialization;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField] private GemUI gemUi;

    [SerializeField] private Gems[] gems;

    private int collectedGemCount = 0;

    /// <summary>
    /// Gemが取得された時に発火するイベント
    /// 引数：取得されたGemの種類
    /// </summary>
    public event System.Action<Gems.GemType> OnGemCollected;

    /// <summary>
    /// Gemのリセットが行われた時に発火するイベント
    /// </summary>
    public event System.Action OnGemReset;


    /// <summary>
    /// 初期化(Gemにイベント登録)
    /// </summary>
    public void Initialize()
    {
        // ゲームステート変更イベントを購読
        GameManager.Instance.OnStateChanged += HandleStateChange;

        foreach (var gem in gems)
        {
            if (gem == null) continue;

            // Gem取得時にカウント
            gem.OnCollected += CollectGem;
            gem.Initialize();
        }

        gemUi.Initialize(this);
    }

    private void OnDisable()
    {
        // ゲームステート変更の購読解除
        GameManager.Instance.OnStateChanged -= HandleStateChange;

        foreach (var gem in gems)
        {
            if (gem == null) continue;

            // アイテム取得の購読解除
            gem.OnCollected -= CollectGem;
        }
    }

    /// <summary>
    /// ゲームステートの変更処理
    /// </summary>
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Title:

                ResetAll();

                break;

            case GameState.Game:

                ResetAll();

                break;

            case GameState.Pause:

                break;

            case GameState.GameOver:

                ResetAll();

                gemUi.ResetGemColor();

                break;

            default:

                break;
        }
    }

    /// <summary>
    /// Gemを取得し、取得イベントを通知する
    /// </summary>
    private void CollectGem(Gems.GemType type)
    {
        collectedGemCount++;
        OnGemCollected?.Invoke(type);
    }

    /// <summary>
    /// Gemの情報・取得数をリセット
    /// </summary>
    private void ResetAll()
    {
        ResetAllGems();
        ResetGemCount();
        OnGemReset?.Invoke();
    }

    /// <summary>
    /// 全Gemの情報をリセット
    /// </summary>
    public void ResetAllGems()
    {
        foreach (var gem in gems)
        {
            if (gem == null) continue;

            gem.ResetGems();
        }
    }

    /// <summary>
    /// Gem取得数をリセット
    /// </summary>
    public void ResetGemCount()
    {
        collectedGemCount = 0;
    }

}
