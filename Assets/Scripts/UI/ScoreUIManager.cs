using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;


public class ScoreUIManager : MonoBehaviour
{
    [SerializeField] private List<Image> stars = new List<Image>();

    [SerializeField] private List<Image> starOutlines = new List<Image>();

    [SerializeField] private RankManager scoreManager;

    // ランクを表示する
    [SerializeField] private TextMeshProUGUI rankSetText;

    // 各ランクで表示する星の数
    private const int rankACount = 3;
    private const int rankBCount = 2;
    private const int rankCCount = 1;

    private void Start()
    {
        if (GameManager.Instance == null)
        {
            return;
        }

        GameManager.Instance.OnStateChanged += HandleStateChange;
        ResetStars();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;
        GameManager.Instance.OnStateChanged -= HandleStateChange;
    }

    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Game:


                break;

            case GameState.Pause:

                break;

            case GameState.GameClear:

                InitShowOutlines();

                ResetStars();

                break;

            case GameState.GameOver:


                break;
        }
    }

    /*private void ShowResult()
    {
        int count = ItemManager.
    }*/

    /// <summary>
    /// 星の枠を表示する
    /// </summary>
    private void InitShowOutlines()
    {
        foreach (var outline in starOutlines)
        {
            outline.enabled = true;
        }
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
}
