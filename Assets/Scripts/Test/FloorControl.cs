using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class FloorControl : MonoBehaviour, IControllable
{
    [SerializeField] private List<MoveFloor> floors = new List<MoveFloor>();

    // 現在選択されているか
    private bool isSelected = false;

    /// <summary>
    /// カーソルが自分に合っているかを外部から設定する
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    private void Update()
    {
        Debug.Log("認識");
        // 選択されていない、または入力が無ければ処理しない
        if (!isSelected || Keyboard.current == null) return;
        Debug.Log($"{name} 選択中 floors={floors.Count}");

        float direction = 0;

        if (Keyboard.current.wKey.isPressed)
        {
            Debug.Log("上");
            direction = 1;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            Debug.Log("下");
            direction = -1;
        }

        // リフトの移動方向をまとめて設定(移動はMoveFloorで行う)
        foreach (MoveFloor moveFloor in floors)
        {
            moveFloor.Move(direction);
        }
    }
}
