using UnityEngine;
using UnityEngine.InputSystem;

public class RankSaveSystem : MonoBehaviour
{
    private const string saveKey = "Rank_Stage";

    /// <summary>
    /// ランクを保存する(既存より高い場合のみ上書き)
    /// </summary>
    public void SaveRank(Rank rank, int stageNumber)
    {
        // 保存されていなければ処理する
        if(!PlayerPrefs.HasKey(saveKey + stageNumber))
        {
            // 初回は必ず保存する
            PlayerPrefs.SetInt(saveKey + stageNumber, (int)rank);
            PlayerPrefs.Save();
            return;
        }

        // 既存のランクを読み込んで比較
        Rank currentrank = LoadRank(stageNumber);

        // 新しいランクが現在より高い場合に上書き保存
        if (rank > currentrank)
        {
            PlayerPrefs.SetInt(saveKey + stageNumber, (int)rank);
            PlayerPrefs.Save();
        }
    }

    /// <summary>
    /// 指定ステージのランクを読み込む
    /// </summary>
    public Rank LoadRank(int stageNumber)
    {
        return (Rank)PlayerPrefs.GetInt(saveKey + stageNumber);
    }

    /// <summary>
    /// 指定ステージがクリア済みかどうかを返す
    /// </summary>
    public bool IsStageClear(int stageNumber)
    {
        return PlayerPrefs.HasKey(saveKey + stageNumber);
    }
}
