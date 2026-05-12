using UnityEngine;
using UnityEngine.UI;

public class StageButtonMedalSetter : MonoBehaviour
{
    [SerializeField] private RankSaveSystem rankSaveSystem;

    // 各ランクに対応するメダルスプライト
    [SerializeField] private Sprite goldMedal;
    [SerializeField] private Sprite silverMedal;
    [SerializeField] private Sprite bronzeMedal;

    // 各ステージのメダル表示用Imageコンポーネント
    [SerializeField] private Image stage1Medal;
    [SerializeField] private Image stage2Medal;
    void Start()
    {
        Initialize();
        ApplySavedRanks();
    }

    private void Initialize()
    {
        SetMedal(stage1Medal, bronzeMedal, Color.black, 0.5f);
        SetMedal(stage2Medal, bronzeMedal, Color.black, 0.5f);
    }

    /// <summary>
    /// 保存されているランクデータを全ステージに反映する
    /// </summary>
    private void ApplySavedRanks()
    {
        ApplyMedal(stage1Medal, 1);

        ApplyMedal(stage2Medal, 2);
    }

    /// <summary>
    /// 指定ステージのクリア状況に応じてメダルを設定する
    /// </summary>
    private void ApplyMedal(Image medalImage, int stageNumber)
    {
        // 未クリアの場合は暗転表示
        if (!rankSaveSystem.IsStageClear(stageNumber))
        {
            SetMedal(medalImage, bronzeMedal, Color.black, 0.5f);
            return;
        }

        // クリア済みの場合はランクに対応したメダルを表示
        Rank rank = rankSaveSystem.LoadRank(stageNumber);
        Sprite sprite = GetMedalSprite(rank);
        SetMedal(medalImage, sprite, Color.white, 1f);
    }

    /// <summary>
    /// ランクに対応するメダルスプライトを返す
    /// </summary>
    private Sprite GetMedalSprite(Rank rank)
    {
        switch (rank)
        {
            case Rank.A:

                return goldMedal;


            case Rank.B:

                return silverMedal;

            case Rank.C:

                return bronzeMedal;


            default:

                return bronzeMedal;
        }
    }

    /// <summary>
    /// メダルのスプライト変更
    /// </summary>
    private void SetMedalSprite(Image medalImage, Sprite medalSprite)
    {
        medalImage.sprite = medalSprite;
    }


    /// <summary>
    /// メダルのスプライト・色・透明度を纏めて設定する
    /// </summary>
    /// <param name="medalImage">対象のメダルImage</param>
    /// <param name="medalSprite">設定するスプライト</param>
    /// <param name="color">設定する色</param>
    /// <param name="alpha">透明度0 = 透明、1 = 不透明</param>
    private void SetMedal(Image medalImage, Sprite medalSprite, Color color, float alpha)
    {
        SetMedalSprite(medalImage, medalSprite);
        color.a = alpha;
        medalImage.color = color;
    }
}
