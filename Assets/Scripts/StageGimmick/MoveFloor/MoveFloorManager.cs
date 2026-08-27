using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MoveFloorManager : MonoBehaviour
{
    [SerializeField] private List<MoveFloor> floors = new List<MoveFloor>();

    [SerializeField] private List<Image> frames = new List<Image>();

    // フレーム表示を制御する番号
    private int frameIndex = 0;

    void Start()
    {
        frameIndex = 0;

        // 最初はフレーム非表示状態からスタート
        foreach(Image i in frames)
        {
            i.enabled = false;
        }
    }

    void Update()
    {
        if (Keyboard.current.uKey.isPressed)
        {
            Move(3f);
        }

        if (Keyboard.current.dKey.isPressed)
        {
            Move(-3f);
        }
    }

    private void Move(float power)
    {
        foreach(MoveFloor m in floors)
        {
            Vector3 pos = m.transform.position;
            pos.y += power * Time.deltaTime;
            m.transform.position = pos;
        }
    }
}
