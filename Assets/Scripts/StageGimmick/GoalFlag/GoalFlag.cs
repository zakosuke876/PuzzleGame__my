using UnityEngine;

public class GoalFlag : MonoBehaviour
{
    // ƒNƒŠƒAŽž‚É–Â‚ç‚·Œø‰Ê‰¹
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    [Header("SE‚Ì‰¹—Ê"), SerializeField] private float volume = 1f;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag(Tags.Ball))
        {
            audioSource.PlayOneShot(audioClip, volume);
            GameManager.Instance.ChangeState(GameState.GameClear);
        }
    }
}
