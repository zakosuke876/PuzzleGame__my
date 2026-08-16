using System.Collections.Generic;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    // IGimmickManagerを実装したコンポーネントを子から集めて保持するリスト
    private List<IGimmickManager> managers = new List<IGimmickManager>();

    // IResettableを実装したコンポーネントを子から集めて保持するリスト
    private List<IResettable> resetManagers = new List<IResettable>();

    /// <summary>
    /// ゲームステートのイベントを購読する
    /// </summary>
    public void Initialize()
    {
        // 二重登録防止
        managers.Clear();
        resetManagers.Clear();

        // 子オブジェクト内のIGimmickManager実装を全て取得して登録
        managers.AddRange(
            GetComponentsInChildren<IGimmickManager>());

        // 子オブジェクト内のIResettable実装を全て取得して登録
        resetManagers.AddRange(
            GetComponentsInChildren<IResettable>());

        // 二重購読防止
        GameManager.Instance.OnStateChanged -= HandleStateChange;
        GameManager.Instance.OnStateChanged += HandleStateChange;
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

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
        foreach (IResettable resettable in resetManagers)
        {
            resettable.OnGameReset();
        }
    }
}