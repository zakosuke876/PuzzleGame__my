using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneNameState : MonoBehaviour
{
    private void /*Initialize*/Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        Debug.Log(SceneManager.GetActiveScene().name);

        switch (sceneName)
        {
            case SceneNames.Title:

                GameManager.Instance.ChangeState(GameState.Title);

                break;

            case SceneNames.Game:

                GameManager.Instance.ChangeState(GameState.Game);

                break;

            default:

                Debug.Log("•sˆê’v");

                break;
        }
    }
}
