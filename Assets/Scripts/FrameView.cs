using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FrameView : MonoBehaviour
{
    [SerializeField] private List<Image> frames = new List<Image>();

    [SerializeField] private FrameController frameController;

    private void OnEnable()
    {
        frameController.OnSelectedChanged += UpdateFrame;
    }

    private void OnDisable()
    {
        frameController.OnSelectedChanged -= UpdateFrame;
    }

    /// <summary>
    /// 使用するフレーム数をFrameControllerに渡す
    /// </summary>
    private void Awake()
    {
        frameController.SetFrameCount(frames.Count);
    }
    
    /// <summary>
    /// 選択中の枠だけを表示、他は非表示にする
    /// </summary>
    private void UpdateFrame(int selectedIndex)
    {
        for (int i = 0; i < frames.Count; i++)
        {
            // iが選択中インデックスと一致する枠だけ表示
            frames[i].enabled = (i == selectedIndex);
        }
    }
}
