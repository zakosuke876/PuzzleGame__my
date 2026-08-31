using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class ConveyorControl : MonoBehaviour, IControllable
{
    [SerializeField] private List<Conveyor> conveyors = new List<Conveyor>();

    // コンベア向き変更時に鳴らす効果音
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    [Header("SEの音量"), SerializeField] private float volume = 1f;

    // 現在選択されているか
    private bool isSelected = false;

    /// <summary>
    /// カーソルが自分に合っているかを外部から設定する
    /// </summary>
    public void SetSelected(bool selected)
    {
        isSelected = selected;
    }

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // 選択されていない、または入力が無ければ処理しない
        if (!isSelected || Keyboard.current == null) return;

        if (Keyboard.current.aKey.wasPressedThisFrame)
        {
            // 左向きに設定
            SetDirection(false);

            // 効果音を鳴らす
            audioSource.PlayOneShot(audioClip, volume);
        }

        if (Keyboard.current.dKey.wasPressedThisFrame)
        {
            // 右向きに設定
            SetDirection(true);

            // 効果音を鳴らす
            audioSource.PlayOneShot(audioClip, volume);
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
