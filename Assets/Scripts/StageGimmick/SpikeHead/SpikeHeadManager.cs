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

            // ˆê“x‚¾‚¯‰Šú‰»‚·‚é
            if (!spikeHead.IsInitialized)
            {
                spikeHead.Initialize();
                spikeHead.SpikeHeadMove();
            }
            else // 2‰ñ–ÚˆÈ~‚Íó‘Ô‚¾‚¯ÄŠJ‚µÄ¶¬‚ğ–h‚®
            {
                spikeHead.DoPlay();
            }
        }
    }

    public void OnGameStop()
    {
        foreach (var spikeHead in spikeHeads)
        {
            if (spikeHead == null) continue;

            spikeHead.DoStop();
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
