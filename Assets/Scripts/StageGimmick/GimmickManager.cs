using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    // IGimmickManagerを実装したコンポーネントを子から集めて保持するリスト
    private List<IGimmickManager> managers = new List<IGimmickManager>();

    /// <summary>
    /// ゲームステートのイベントを購読する
    /// </summary>
    public void Initialize()
    {
        managers.AddRange(GetComponentsInChildren<IGimmickManager>());

        GameManager.Instance.OnStateChanged += HandleStateChange;
    }

    private void OnDisable()
    {
        GameManager.Instance.OnStateChanged -= HandleStateChange;
    }

    // ゲームステートの変化を受け取る
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Game:

                OnGameStart();

                break;

            case GameState.Pause:

                OnGameStop();

                break;

            case GameState.Respawn:

                OnGameReset();

                break;

            case GameState.GameOver:

                OnGameReset();

                break;

            case GameState.Reset:

                OnGameReset();

                break;
        }
    }

    /// <summary>
    /// ギミックのゲーム開始処理を呼びだす
    /// </summary>
    public void OnGameStart()
    {
        foreach (IGimmickManager manager in managers)
        {
            manager.OnGameStart();
        }
    }

    /// <summary>
    /// ギミックのゲーム停止処理を呼びだす
    /// </summary>
    public void OnGameStop()
    {
        foreach (IGimmickManager manager in managers)
        {
            manager.OnGameStop();
        }
    }

    /// <summary>
    /// IResettableを実装しているギミックのみリセット処理を呼びだす
    /// </summary>
    public void OnGameReset()
    {
        foreach (IResettable resettable in managers.OfType<IResettable>())
        {
            resettable.OnGameReset();
        }
    }
}