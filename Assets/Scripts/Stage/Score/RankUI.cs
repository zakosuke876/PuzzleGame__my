using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RankUI : MonoBehaviour
{
    [SerializeField] private RankManager rankManager;

    [SerializeField] private List<Image> stars = new List<Image>();

    [SerializeField] private List<Image> starOutlines = new List<Image>();

    [SerializeField] private TextMeshProUGUI rankSetText;

    // 各ランクで表示する星の数
    private const int rankACount = 3;
    private const int rankBCount = 2;
    private const int rankCCount = 1;

    private void OnEnable()
    {
        rankManager.OnRankDecided += ShowRank;
    }

    private void OnDisable()
    {
        rankManager.OnRankDecided -= ShowRank;
    }
    void Start()
    {
        ResetStars();
    }


    /// <summary>
    /// ゲーム開始時に星を非表示にする
    /// </summary>
    private void ResetStars()
    {
        foreach (var star in stars)
        {
            star.enabled = false;
        }
    }



    /// <summary>
    /// 確定ランクを受け取り、対応する数の星とランク文字を表示する
    /// </summary>
    private void ShowRank(Rank rank)
    {
        int starCount;

        switch (rank)
        {
            case Rank.A:

                starCount = rankACount;

                break;

            case Rank.B:

                starCount = rankBCount;

                break;

            case Rank.C:

                starCount = rankCCount;

                break;

            default:

                // 想定外のランクはランクCと同じとして扱う
                starCount = rankCCount;

                break;
        }

        rankSetText.text = $"Rank:{rank}";

        ShowStarOutlines();

        ShowStars(starCount);
    }


    /// <summary>
    /// 星の枠（アウトライン）を表示する
    /// </summary>
    private void ShowStarOutlines()
    {
        foreach (var outline in starOutlines)
        {
            outline.enabled = true;
        }
    }


    /// <summary>
    /// ランクに応じた数の星を表示する
    /// </summary>
    /// <param name="starCount">表示する星の数</param>
    private void ShowStars(int starCount)
    {
        for (int i = 0; i < starCount; i++)
        {
            stars[i].enabled = true;
        }
    }
}
