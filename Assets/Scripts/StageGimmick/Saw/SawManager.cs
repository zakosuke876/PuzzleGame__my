using UnityEngine;
using System.Collections.Generic;

public class SawManager : MonoBehaviour, IGimmickManager, IResettable
{
    [SerializeField] private List<Saw> saws = new List<Saw>();

    public void OnGameStart()
    {
        foreach (var saw in saws)
        {
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
        foreach (var saw in saws)
        {
            if (saw == null) continue;

            saw.DoStop();
        }
    }

    public void OnGameReset()
    {
        foreach (var saw in saws)
        {
            if (saw == null) continue;

            saw.ResetGimmick();
        }
    }
}
