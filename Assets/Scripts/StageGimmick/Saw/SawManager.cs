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

            saw.DoPlay();
        }
    }

    public void OnGameStop()
    {
        foreach (var saw in saws)
        {
            if (saw == null) continue;

            saw.DoPause();
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
