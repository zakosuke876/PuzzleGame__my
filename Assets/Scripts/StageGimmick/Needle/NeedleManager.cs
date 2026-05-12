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

            // ˆê“x‚¾‚¯‰Šú‰»‚·‚é
            if (!needle.IsInitialized)
            {
                needle.Initialize();

                needle.Move();
            }
            else // 2‰ñ–ÚˆÈ~‚Íó‘Ô‚¾‚¯ÄŠJ‚µÄ¶¬‚ğ–h‚®
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
