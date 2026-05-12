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

            // ˆê“x‚¾‚¯‰Šú‰»‚·‚é
            if (!saw.IsInitialized)
            {
                saw.Initialize();
                saw.SawMove();
            }
            else // 2‰ñ–ÚˆÈ~‚Íó‘Ô‚¾‚¯ÄŠJ‚µÄ¶¬‚ğ–h‚®
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
