using UnityEngine;
using System.Collections.Generic;

public class ControlPanel : MonoBehaviour
{
    [SerializeField] private FrameController frameController;

    [SerializeField] private List<MonoBehaviour> controls = new List<MonoBehaviour>();
    private void OnEnable()
    {
        frameController.OnSelectedChanged += HandleSelectedChanged;
    }

    private void OnDisable()
    {
        frameController.OnSelectedChanged -= HandleSelectedChanged;
    }

    private void HandleSelectedChanged(int index)
    {
        for (int i = 0; i < controls.Count; i++)
        {
            // MonoBehaviour‚ðIControllable‚É•ÏŠ·‚·‚é
            (controls[i] as IControllable)?.SetSelected(i == index);
        }
    }
}
