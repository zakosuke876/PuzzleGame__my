using UnityEngine;
using UnityEngine.SceneManagement;

public class Button : MonoBehaviour
{
    private enum SceneName
    {
        TitleScene,
        GameScene
    }

    [SerializeField] private SceneName sceneName;

    void Start()
    {
        
    }

    void Update()
    {
        
    }

    public void SceneChange()
    {
        SceneManager.LoadScene(sceneName.ToString());
        Debug.Log($"シーンチェンジ{sceneName}");
    }
}
