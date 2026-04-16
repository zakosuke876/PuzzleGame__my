using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // 使用するシーン名
    private enum SceneName
    {
        TitleScene,
        TutorialScene,
        Stage1
    }

    // インスペクターから遷移するシーンを選ぶ
    [SerializeField] private SceneName sceneName;

    #region ボタン用関数
    public void SceneChange()
    {
        // 設定したシーンに遷移する
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName.ToString());
    }

    public void GameRetry()
    {
        GameManager.Instance.Retry();
    }
    #endregion
}
