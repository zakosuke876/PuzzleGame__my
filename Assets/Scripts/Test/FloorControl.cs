using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class FloorControl : MonoBehaviour, IControllable
{
    [SerializeField] private List<MoveFloor> floors = new List<MoveFloor>();

    [SerializeField] private FrameController frameController;

    [SerializeField] private int myIndex = 1;

    private void OnEnable()
    {
        //frameController.OnSelectedChanged += HandleChanged;
    }

    private void OnDisable()
    {
        //frameController.OnSelectedChanged -= HandleChanged;
    }

    private void HandleChanged(int index)
    {
        isSelected = (myIndex == index);
    }

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
        // 選択されていない、または入力が無ければ処理しない
        if (!isSelected || Keyboard.current == null) return;

        float direction = 0;

        if (Keyboard.current.wKey.isPressed)
        {
            direction = 1;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            direction = -1;
        }

        // リフトの移動方向をまとめて設定(移動はMoveFloorで行う)
        foreach (MoveFloor moveFloor in floors)
        {
            moveFloor.Move(direction);
        }
    }
}
