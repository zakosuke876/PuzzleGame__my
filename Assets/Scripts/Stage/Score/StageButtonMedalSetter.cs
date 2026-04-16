using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class StageButtonMedalSetter : MonoBehaviour
{
    [SerializeField] private RankSaveSystem rankSaveSystem;

    [SerializeField] private Sprite goldMedal;

    [SerializeField] private Sprite silverMedal;

    [SerializeField] private Sprite bronzeMedal;

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

    private void ApplySavedRanks()
    {
        ApplyMedal(stage1Medal, 1);

        ApplyMedal(stage2Medal, 2);
    }

    private void ApplyMedal(Image medalImage, int stageNumber)
    {
        if (!rankSaveSystem.IsStageClear(stageNumber))
        {
            Debug.Log($"クリアできてません:{stageNumber}");
            SetMedal(medalImage, bronzeMedal, Color.black, 0.5f);
            return;
        }

        Rank rank = rankSaveSystem.LoadRank(stageNumber);
        Sprite sprite = GetMedalSprite(rank);
        SetMedal(medalImage, sprite, Color.white, 1f);
    }

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
    /// スプライト変更
    /// </summary>
    private void SetMedalSprite(Image medalImage, Sprite medalSprite)
    {
        medalImage.sprite = medalSprite;
    }

    /// <summary>
    /// メダルの設定
    /// </summary>
    private void SetMedal(Image medalImage, Sprite medalSprite, Color color, float alpha)
    {
        SetMedalSprite(medalImage, medalSprite);
        color.a = alpha;
        medalImage.color = color;
    }
}
