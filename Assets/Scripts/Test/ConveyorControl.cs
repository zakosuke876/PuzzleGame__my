using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ConveyorControl : MonoBehaviour, IControllable
{
    [SerializeField] private List<Conveyor> conveyors = new List<Conveyor>();

    /*[SerializeField] private FrameController frameController;

    [SerializeField] private int myIndex = 0;

    private void OnEnable()
    {
        frameController.OnSelectedChanged += HandleChanged;
    }

    private void OnDisable()
    {
        frameController.OnSelectedChanged -= HandleChanged;
    }

    private void HandleChanged(int index)
    {
        isSelected = (myIndex == index);
    }*/


    // 現在選択されているか
    private bool isSelected = false;

    /// <summary>
    /// カーソルが自分に合っているかを外部から設定する
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    void Update()
    {
        // 選択されていない、または入力が無ければ処理しない
        if (!isSelected || Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            // 左向きに設定
            SetDirection(false);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            // 右向きに設定
            SetDirection(true);
        }
    }

    /// <summary>
    /// 全ベルトコンベアの向きをまとめて設定する
    /// </summary>
    private void SetDirection(bool movingRight)
    {
        foreach (Conveyor conveyor in conveyors)
        {
            conveyor.IsMovingRight = movingRight;
        }
    }
}
