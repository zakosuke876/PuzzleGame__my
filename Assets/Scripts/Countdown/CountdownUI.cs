using UnityEngine;
using TMPro;
using System;
using System.Collections;
using UnityEngine.UI;

public class CountdownUI : MonoBehaviour
{
    [SerializeField] private GameObject countdownPanel; 
    [SerializeField] private TextMeshProUGUI countdownText;

    [Header("カウントダウン終了後に表示する文字列"), SerializeField] private string startText = "START!!";

    [SerializeField] private int countdownTime = 3;

    public event Action OnCountDownFinished;

    private void Awake()
    {
        countdownPanel.gameObject.SetActive(false);
    }

    public void StartCountdown()
    {
        countdownPanel.gameObject.SetActive(true);
        StartCoroutine(PlayCountdown());
    }

    /// <summary>
    /// ゲーム開始までのカウントダウンを開始させる
    /// </summary>
    IEnumerator PlayCountdown()
    {
        // 3秒間のカウントダウンを開始
        for (int i = countdownTime; i > 0; i--)
        {
            countdownText.text = i.ToString();
            yield return new WaitForSeconds(1f);
        }

        countdownText.text = startText;

        yield return new WaitForSeconds(1f);

        countdownPanel.gameObject.SetActive(false);
        
        OnCountDownFinished?.Invoke();
    }
}
