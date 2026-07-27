using UnityEngine;
using System.Collections.Generic;

public class SpikeHeadManager : MonoBehaviour, IGimmickManager, IResettable
{
    [SerializeField] private List<SpikeHead> spikeHeads = new List<SpikeHead>();
    
    public void  OnGameStart()
    {
        foreach (var spikeHead in spikeHeads)
        {
            if (spikeHead == null) continue;

            spikeHead.Play();
        }
    }

    public void OnGameStop()
    {
        foreach (var spikeHead in spikeHeads)
        {
            if (spikeHead == null) continue;

            spikeHead.Stop();
        }
    }

    public void OnGameReset()
    {
        foreach (var spikeHead in spikeHeads)
        {
            if (spikeHead == null) continue;

            spikeHead.ResetGimmick();
        }
    }
}
