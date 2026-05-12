using UnityEngine;
using System.Collections.Generic;

public class DisappearingBlockManager : MonoBehaviour,IResettable
{
    [SerializeField] private List<GameObject> blockObjects = new List<GameObject>();


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
