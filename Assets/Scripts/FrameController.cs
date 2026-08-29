using UnityEngine;
using UnityEngine.InputSystem;

public class FrameController : MonoBehaviour
{
    // 現在選択中のインデックス
    private int selectedIndex = 0;
    
    // 線タックできる数(範囲チェック用。FrameViewから受け取る)
    private int frameCount = 0;

    /// <summary>
    /// 選択が変わった時に発火するイベント
    /// </summary>
    public event System.Action<int> OnSelectedChanged;

    /// <summary>
    /// 範囲内チェックに使うフレーム数を外部から設定する
    /// </summary>
    public void SetFrameCount(int count)
    {
        frameCount = count;
    }

    private void Start()
    {
        selectedIndex = 0;
        OnSelectedChanged?.Invoke(selectedIndex);
    }

    private void Update()
    {
        // 入力が無ければ処理しない
        if (Keyboard.current == null) return;

        if (Keyboard.current.leftArrowKey.wasPressedThisFrame)
        {
            selectedIndex--;

            // 先頭を越えたら末尾へ戻る
            if (selectedIndex < 0) selectedIndex = frameCount - 1;
            OnSelectedChanged?.Invoke(selectedIndex);
        }

        if (Keyboard.current.rightArrowKey.wasPressedThisFrame)
        {
            selectedIndex++;

            // 末尾を越えたら先頭へ戻る
            if (selectedIndex >= frameCount) selectedIndex = 0;
            OnSelectedChanged?.Invoke(selectedIndex);
        }
    }
}
