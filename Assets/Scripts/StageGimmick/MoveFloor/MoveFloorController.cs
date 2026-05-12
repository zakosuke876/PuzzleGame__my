using UnityEngine;
using UnityEngine.InputSystem;

public class MoveFloorController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [SerializeField] private float floorMoveSpeed = 0f;

    // 現在選択されているオブジェクト
    private GameObject selectedObject;

    // MpveFloor移動時に鳴らす効果音
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;
    [Header("SEの音量"), SerializeField] private float volume = 1f;


    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();

        Initialize();
    }

    private void Initialize()
    {
        // 選択オブジェクト変更イベント購読
        gridManager.OnSelectedObjectChanged += OnSelectedChanged;
    }

    private void OnDisable()
    {
        gridManager.OnSelectedObjectChanged -= OnSelectedChanged;
    }

    /// <summary>
    /// 選択オブジェクトが変わった時に呼ばれる
    /// </summary>
    public void OnSelectedChanged(GameObject obj)
    {
        selectedObject = obj;
    }

    void Update()
    {
        if (selectedObject == null) return;

        if (!selectedObject.CompareTag(Tags.MoveFloor)) return;

        // W = 上方向, S = 下方向
        Vector2 move = Vector2.zero;
        if (Keyboard.current.wKey.IsPressed()) move = Vector2.up;
        if (Keyboard.current.sKey.IsPressed()) move = Vector2.down;

        if (move != Vector2.zero)
        {
            Vector3 localMove = (Vector3)(move * floorMoveSpeed * Time.deltaTime);
            selectedObject.transform.position += localMove;

            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
