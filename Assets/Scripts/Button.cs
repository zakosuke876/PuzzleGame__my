using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    // 使用するシーン名
    private enum SceneName
    {
        TitleScene,
        GameScene
    }

    // インスペクターから遷移するシーンを選ぶ
    [SerializeField] private SceneName sceneName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SceneChange()
    {
        // 設定したシーンに遷移する
        SceneManager.LoadScene(sceneName.ToString());
        Debug.Log($"シーンチェンジ{sceneName}");
    }
}
