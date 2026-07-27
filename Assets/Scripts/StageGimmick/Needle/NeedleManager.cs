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

            needle.Play();
        }
    }

    public void OnGameStop()
    {
        foreach (var needle in needles)
        {
            if (needle == null) continue;

            needle.Stop();
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
