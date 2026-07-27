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

            saw.Play();
        }
    }

    public void OnGameStop()
    {
        foreach (var saw in saws)
        {
            if (saw == null) continue;

            saw.Stop();
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
