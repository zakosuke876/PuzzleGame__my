using UnityEngine;
using System.Collections.Generic;

public class DisappearingBlockManager : MonoBehaviour, IGimmickManager, IResettable
{
    [SerializeField] private List<GameObject> blockObjects = new List<GameObject>();

    public void OnGameStart()
    {
        foreach (GameObject obj in blockObjects)
        {
            if (obj == null) continue;

            DisappearingBlock block = obj.GetComponent<DisappearingBlock>();

            if (block == null) continue;
        }        
    }

    public void OnGameStop()
    {
        foreach (GameObject obj in blockObjects)
        {
            if (obj == null) continue;

            DisappearingBlock block = obj.GetComponent<DisappearingBlock>();

            if (block == null) continue;
        }
    }

    /// <summary>
    /// GameOverやRespawn時にブロックをリセットする
    /// </summary>
    public void OnGameReset()
    {
        foreach (GameObject obj in blockObjects)
        {
            if (obj == null) continue;

            DisappearingBlock block = obj.GetComponent<DisappearingBlock>();

            if (block == null) continue;

            block.ResetBlock();
        }
    }
}
