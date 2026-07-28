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
    [SerializeField] private ItemManager itemManager;

    [SerializeField] private RankSaveSystem rankSaveSystem;

    [SerializeField] private int currentStageNumber = 0;

    // Gem取得数がこの値以上でランク〇
    private const int judgmentRankA = 2;
    private const int judgmentRankB = 1;

    /// <summary>
    /// ランクが確定した時に発火するイベント
    /// </summary>
    public event Action<Rank> OnRankDecided;

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
        Rank rank = GetRank();

        rankSaveSystem.SaveRank(rank, currentStageNumber);

        OnRankDecided?.Invoke(rank);
    }


    /// <summary>
    /// ItemManagerから取得済みGem数を取得する
    /// </summary>
    private int GetCollectedGemCount()
    {
        return itemManager.CollectedGemCount;
    }
}
