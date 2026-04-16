using UnityEngine;
using UnityEngine.InputSystem;

public class RankSaveSystem : MonoBehaviour
{
    private const string saveKey = "Rank_Stage";

    public void SaveRank(Rank rank, int stageNumber)
    {
        if(!PlayerPrefs.HasKey(saveKey + stageNumber))
        {
            // ‰‰ñ‚Í•K‚¸•Û‘¶‚·‚é
            PlayerPrefs.SetInt(saveKey + stageNumber, (int)rank);
            PlayerPrefs.Save();
            return;
        }

        Rank currentrank = LoadRank(stageNumber);

        if (rank > currentrank)
        {
            PlayerPrefs.SetInt(saveKey + stageNumber, (int)rank);
            PlayerPrefs.Save();
        }
    }

    public Rank LoadRank(int stageNumber)
    {
        return (Rank)PlayerPrefs.GetInt(saveKey + stageNumber);
    }

    public bool IsStageClear(int stageNumber)
    {
        return PlayerPrefs.HasKey(saveKey + stageNumber);
    }
}
