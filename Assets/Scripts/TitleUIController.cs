using UnityEngine;

public enum TitlePanel
{
    Main,
    Manual,
    StageSelect
}

public class TitleUIController : MonoBehaviour
{
    [SerializeField] TitleUIManager TitleUIManager;

    public void OnMainPanel()
    {
        TitleUIManager.HandleStateChange(TitlePanel.Main);
    }

    public void OnManualPanel()
    {
        TitleUIManager.HandleStateChange(TitlePanel.Manual);
    }
    public void OnStageSelect()
    {
        TitleUIManager.HandleStateChange(TitlePanel.StageSelect);
    }
}