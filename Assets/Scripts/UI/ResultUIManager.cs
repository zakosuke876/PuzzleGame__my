using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameClearPanel;

    [SerializeField] private GameObject pausePanel;

    [SerializeField] private GameObject gameOverPanel;

    private void Start()
    {
        HideAllPanels();

        GameManager.Instance.OnStateChanged += HandleStateChange;
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

                Time.timeScale = 1;
                HideAllPanels();

                break;

            case GameState.Pause:

                Time.timeScale = 0;
                ShowPanel(pausePanel);

                break;

            case GameState.Respawn:

                HideAllPanels();

                break;

            case GameState.GameClear:

                ShowPanel(gameClearPanel);

                break;

            case GameState.GameOver:

                ShowPanel(gameOverPanel);

                break;

            case GameState.Reset:

                HideAllPanels();

                break;
        }
    }

    /// <summary>
    /// 指定したパネルだけを表示する
    /// </summary>
    private void ShowPanel(GameObject panel)
    {
        HideAllPanels();
        panel.SetActive(true);
    }

    private void HideAllPanels()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }
}
