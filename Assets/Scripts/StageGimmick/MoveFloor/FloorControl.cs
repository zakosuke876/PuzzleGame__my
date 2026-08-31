using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class FloorControl : MonoBehaviour, IControllable
{
    [SerializeField] private List<MoveFloor> floors = new List<MoveFloor>();

    // MoveFloor移動時に鳴らす効果音
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
        audioSource.clip = audioClip;
        audioSource.loop = true;
        audioSource.volume = volume;
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        // 選択されていない、または入力が無ければ効果音を止めて終了
        if (!isSelected || Keyboard.current == null)
        {
            StopSe();
            return;
        }

        float direction = 0;

        if (Keyboard.current.wKey.isPressed)
        {
            direction = 1;
        }

        if (Keyboard.current.sKey.isPressed)
        {
            direction = -1;
        }

        // 移動方向が0の場合効果音を止めて終了
        if (direction == 0)
        {
            StopSe();
            return;
        }

        // リフトの移動方向をまとめて設定(移動はMoveFloorで行う)
        foreach (MoveFloor moveFloor in floors)
        {
            moveFloor.Move(direction);
        }

        PlaySe();
    }

    /// <summary>
    /// 操作時に効果音を鳴らす
    /// </summary>
    private void PlaySe()
    {
        // 効果音がなっていない場合に鳴らす
        if (!audioSource.isPlaying)
        {
            audioSource.Play();
        }
    }

    /// <summary>
    /// 効果音を止める
    /// </summary>
    private void StopSe()
    {
        // 効果音がなっている場合に止める
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
}
