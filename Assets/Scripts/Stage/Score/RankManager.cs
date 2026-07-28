using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum Rank
{
    C,  // 0
    B,  // 1
    A   // 2
}

public class RankManager : MonoBehaviour
{
    //[SerializeField] private List<Image> stars = new List<Image>();

    //[SerializeField] private List<Image> starOutlines = new List<Image>();

    [SerializeField] private ItemManager itemManager;

    [SerializeField] private RankSaveSystem rankSaveSystem;

    [SerializeField] private int currentStageNumber = 0;

    // Gem取得数がこの値以上でランク〇
    private const int judgmentRankA = 2;
    private const int judgmentRankB = 1;

    /*// 各ランクで表示する星の数
    private const int rankACount = 3;
    private const int rankBCount = 2;
    private const int rankCCount = 1;*/

    /// <summary>
    /// ランクが確定した時に発火するイベント
    /// </summary>
    public event Action<Rank> OnRankDecided;

    // ランクを表示する
    //[SerializeField] private TextMeshProUGUI rankSetText;
    void Start()
    {
        GameManager.Instance.OnStateChanged += HandleStateChange;

        //ResetStars();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        GameManager.Instance.OnStateChanged -= HandleStateChange;
    }


    /// <summary>
    /// ゲーム状態を受け取り、クリア時にランク判定を行う
    /// </summary>
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.GameClear:

                DecideRank();

                //InitShowOutlines();

                break;
        }
    }

    /*/// <summary>
    /// ゲーム開始時に星を非表示にする
    /// </summary>
    private void ResetStars()
    {
        foreach (var star in stars)
        {
            star.enabled = false;
        }
    }*/

    /*/// <summary>
    /// 星の枠を表示する
    /// </summary>
    private void InitShowOutlines()
    {
        foreach (var outline in starOutlines)
        {
            outline.enabled = true;
        }
    }*/

    /*/// <summary>
    /// ランクに応じた数の星を表示する
    /// </summary>
    /// <param name="count">表示する星の数</param>
    private void ShowStars(int count)
    {
        for (int i = 0; i < count; i++)
        {
            stars[i].enabled = true;
        }
    }*/

    /// <summary>
    /// 取得したGem数からランクを判定して返す
    /// </summary>
    private Rank GetRank()
    {
        int count;

        count = GetCollectedGemCount();

        if (count >= judgmentRankA) return Rank.A;
        if (count >= judgmentRankB) return Rank.B;
        return Rank.C;
    }

    /// <summary>
    /// クリア時のランク判定・保存・通知を行う
    /// </summary>
    private void DecideRank()
    {
        //int starCount;

        Rank rank = GetRank();

        /*switch (rank)
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
        }*/

        //rankSetText.text = $"Rank:{rank}";

        rankSaveSystem.SaveRank(rank, currentStageNumber);

        OnRankDecided?.Invoke(rank);

        //ShowStars(starCount);
    }


    /// <summary>
    /// ItemManagerから取得済みGem数を取得する
    /// </summary>
    private int GetCollectedGemCount()
    {
        return itemManager.CollectedGemCount;
    }
}
