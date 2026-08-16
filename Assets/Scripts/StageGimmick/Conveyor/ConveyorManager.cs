using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ConveyorManager : MonoBehaviour
{
    [SerializeField] private List<Conveyor> conveyors = new List<Conveyor>();
    void Start()
    {
        foreach(Conveyor c in conveyors)
        {
            c.IsMovingRight = true;
        }
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

    private void SetConveyor(bool movingRight)
    {
        foreach(Conveyor c in conveyors)
        {
            c.IsMovingRight = movingRight;
        }
    }
}
