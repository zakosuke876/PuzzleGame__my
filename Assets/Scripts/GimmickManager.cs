using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using DG.Tweening;

public class GimmickManager : MonoBehaviour
{
    [SerializeField] public List<GameObject> gimmickObject = new List<GameObject>();

    public void Initialize()
    {
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
            case GameState.Title:

                //Debug.Log("GimmickManager.State.Title");

                break;

            case GameState.Game:

                //Debug.Log("GimmickManager.State.Game");

                OnGameStart();

                break;

            case GameState.Pause:

                //Debug.Log("GimmickManager.State.Pause");

                OnGameStop();

                break;

            case GameState.GameOver:

                //Debug.Log("GimmickManager.State.GameOver");

                break;

            default:

                break;
        }
    }

    /// <summary>
    /// ÉMÉ~ÉbÉNÇìÆçÏÇ≥ÇπÇÈä÷êî
    /// </summary>
    public void OnGameStart()
    {
        foreach (var obj in gimmickObject)
        {
            if (obj == null) continue;

            Saw saw = obj.GetComponent<Saw>();

            if (saw == null) continue;

            if (!saw.IsInitialized)
            {
                saw.Initialize();
                saw.SawMove();
            }
            else
            {
                saw.DoPlay();
            }
        }
    }

    public void OnGameStop()
    {
        foreach (var obj in gimmickObject)
        {
            if (obj == null) continue;

            Saw saw = obj.GetComponent<Saw>();

            if (saw == null) continue;

            saw.DoStop();
        }
    }
}