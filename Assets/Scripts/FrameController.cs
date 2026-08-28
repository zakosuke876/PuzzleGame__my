using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class FrameController : MonoBehaviour
{
    [SerializeField] private List<Image> frames = new List<Image>();

    private int selectedIndex = 0;

    private void Start()
    {
        selectedIndex = 0;

        UpdateFrame();
    }

    private void Update()
    {
        // 入力が無ければ処理しない
        if (Keyboard.current == null) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            selectedIndex--;

            // リストの先頭を越えた場合末尾に戻る
            if (selectedIndex < 0) selectedIndex = frames.Count - 1;
            UpdateFrame();
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            selectedIndex++;

            // リストの末尾を超えた場合先頭に戻る
            if (selectedIndex >= frames.Count) selectedIndex = 0;
            UpdateFrame();
        }
    }

    /// <summary>
    /// 選択中の枠だけを表示,他は非表示にする
    /// </summary>
    private void UpdateFrame()
    {
        for (int i = 0; i < frames.Count; i++)
        {
            // iが選択中インデックスと一致する枠だけ表示
            frames[i].enabled = (i == selectedIndex);
        }
    }
}
