using UnityEngine;

public interface IGimmick
{
    void Play();  // 開始/再開
    void Stop();  // 一時停止
    void ResetGimmick();  // リセット(Tween破棄)
}
