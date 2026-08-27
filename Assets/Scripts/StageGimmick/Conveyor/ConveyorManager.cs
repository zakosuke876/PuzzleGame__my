using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private List<Conveyor> conveyors = new List<Conveyor>();

    [SerializeField] private Sprite rightLever;

    [SerializeField] private Sprite leftLever;

    [SerializeField] private Image leverImage;
    void Start()
    {
        SetConveyor(true);
        LeverImageReset();
    }

    void Update()
    {
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            SetConveyor(true);
        }

        if (Keyboard.current.lKey.wasPressedThisFrame)
        {
            SetConveyor(false);
        }
    }

    private void LeverImageReset()
    {
        leverImage.sprite = rightLever;
    }

    private void SetConveyor(bool movingRight)
    {
        foreach(Conveyor c in conveyors)
        {
            c.IsMovingRight = movingRight;
        }

        leverImage.sprite = movingRight ? rightLever : leftLever;
        leverImage.enabled = true;
    }
}
