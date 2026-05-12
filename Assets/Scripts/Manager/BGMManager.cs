using UnityEngine;

public class BGMManager : MonoBehaviour
{
    [SerializeField] private AudioClip gameOberClip;
    [SerializeField] private AudioClip bgmClip;
    [SerializeField] private float volume = 1f;
    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        // ゲームステート変更イベントを購読
        GameManager.Instance.OnStateChanged += HandleStateChange;

        PlayBGM();
    }

    private void OnDisable()
    {
        if (GameManager.Instance == null) return;

        // ゲームステート変更の購読解除
        GameManager.Instance.OnStateChanged -= HandleStateChange;
    }

    private void PlayBGM()
    {
        if (bgmClip == null) return;

        audioSource.clip = bgmClip;
        audioSource.volume = volume;
        audioSource.loop = true;
        audioSource.Play();
    }

    /// <summary>
    /// ゲームステートの変更処理
    /// </summary>
    private void HandleStateChange(GameState state)
    {
        switch (state)
        {
            case GameState.Title:

                break;

            case GameState.Game:

                if (!audioSource.isPlaying)
                {
                    audioSource.Play();
                }

                break;

            case GameState.Pause:

                audioSource.Pause();

                break;

            case GameState.Respawn:

                audioSource.Stop();

                break;

             case  GameState.GameClear:

                audioSource.Stop();

                break;

            case GameState.GameOver:

                audioSource.Stop();

                audioSource.PlayOneShot(gameOberClip, volume);

                break;

            case GameState.Reset:

                audioSource.Play();

                break;

            default:

                break;
        }
    }
}
