using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering;

public class DisappearingBlockManager : MonoBehaviour,IResettable
{
    [SerializeField] private List<DisappearingBlock> blocks = new List<DisappearingBlock>();

    /// <summary>
    /// GameOverやRespawn時にブロックをリセットする
    /// </summary>
    public void OnGameReset()
    {
        foreach (var block in blocks)
        {
            if (block == null) continue;

            block.ResetBlock();
        }
    }
}
