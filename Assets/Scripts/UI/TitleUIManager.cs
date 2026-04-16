using UnityEngine;


public class TitleUIManager : MonoBehaviour
{
    [SerializeField] private GameObject titleMainPanel;

    [SerializeField] private GameObject manualPanel;

    [SerializeField] private GameObject stageSelectPanel;
    void Start()
    {
        Initialize();
    }

    void Initialize()
    {
        titleMainPanel.SetActive(true);
        manualPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
    }

    public void HandleStateChange(TitlePanel state)
    {
        switch (state)
        {
            case TitlePanel.Main:

                AllPanelReset();
                ShowMainPanel();
                break;

            case TitlePanel.Manual:

                AllPanelReset();
                ShowManualPanel();
                break;

            case TitlePanel.StageSelect:

                AllPanelReset();
                ShowStageSelectPanel();
                break;

            default:

                break;
        }
    }

    void AllPanelReset()
    {
        titleMainPanel.SetActive(false);
        manualPanel.SetActive(false);
        stageSelectPanel.SetActive(false);
    }

    void ShowMainPanel()
    {
        titleMainPanel.SetActive(true);
    }

    void ShowManualPanel()
    {
        manualPanel.SetActive(true);
    }

    void ShowStageSelectPanel()
    {
        stageSelectPanel.SetActive(true);
    }
}
