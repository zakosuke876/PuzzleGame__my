using UnityEngine;
using System.Collections.Generic;

public class NeedleManager : MonoBehaviour, IGimmickManager, IResettable
{
    [SerializeField] private List<Needle> needles = new List<Needle>();

    public void OnGameStart()
    {
        foreach (var needle in needles)
        {
            if (needle == null) continue;

            if (!needle.IsInitialized)
            {
                needle.Initialize();

                needle.Move();
            }
            else
            {
                needle.DoPlay();
            }
        }
    }

    public void OnGameStop()
    {
        foreach (var needle in needles)
        {
            if (needle == null) continue;

            needle.DoStop();
        }
    }

    public void OnGameReset()
    {
        foreach (var needle in needles)
        {
            if (needle == null) continue;

            needle.ResetGimmick();
        }
    }
}
