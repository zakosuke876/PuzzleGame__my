using UnityEngine;

public class ResultUIManager : MonoBehaviour
{
    [SerializeField] private GameObject gameClearPanel;

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

                HideAllPanels();

                break;

            case GameState.Pause:

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
        }
    }

    private void HideAllPanels()
    {
        gameClearPanel.SetActive(false);
        gameOverPanel.SetActive(false);
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
