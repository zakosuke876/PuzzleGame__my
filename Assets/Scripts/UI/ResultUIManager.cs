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
        GameManager.Instance.OnStateChanged -= HandleStateChange;
    }


    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Game:

                Time.timeScale = 1;
                HideAllPanels();
                pausePanel.SetActive(false);

                break;

            case GameState.Pause:

                Time.timeScale = 0;
                pausePanel.SetActive(true);

                break;

            case GameState.Respawn:

                HideAllPanels();

                break;

            case GameState.GameClear:

                showGameClearPanel();

                break;

            case GameState.GameOver:

                showGameOverPanel();

                break;

            case GameState.Reset:

                HideAllPanels();

                break;
        }
    }

    private void HideAllPanels()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    private void showGameClearPanel()
    {
        gameClearPanel.SetActive(true);
        gameOverPanel.SetActive(false);
    }

    private void showGameOverPanel()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(true);
    }
}
