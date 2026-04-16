using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GimmickManager : MonoBehaviour
{
    private List<IGimmickManager> managers = new List<IGimmickManager>();

    public void Initialize()
    {
        managers.AddRange(GetComponentsInChildren<IGimmickManager>());

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
        }
    }

    public void OnGameStart()
    {
        foreach (IGimmickManager manager in managers)
        {
            manager.OnGameStart();
        }
    }

    public void OnGameStop()
    {
        foreach (IGimmickManager manager in managers)
        {
            manager.OnGameStop();
        }
    }

    public void OnGameReset()
    {
        foreach (IResettable resettable in managers.OfType<IResettable>())
        {
            resettable.OnGameReset();
        }
    }
}