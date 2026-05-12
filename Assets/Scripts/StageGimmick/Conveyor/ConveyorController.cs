using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    // 現在選択されているオブジェクト
    private GameObject selectedObject;

    // コンベア向き変更時に鳴らす効果音
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
        gridManager.OnSelectedObjectChanged += OnSelectedChanged;
    }

    /// <summary>
    ///  選択オブジェクトが変わった時に呼ばれる
    /// </summary>
    /// <param name="obj"></param>
    private void OnSelectedChanged(GameObject obj)
    {
        selectedObject = obj;
    }

    private void Update()
    {
        if (selectedObject == null) return;

        if (!selectedObject.CompareTag(Tags.ConveyorBelt)) return;

        // A,Dキーでコンベアの向きを左右に切り替える
        if (Keyboard.current.aKey.wasPressedThisFrame) ChangeDirection(false);
        if (Keyboard.current.dKey.wasPressedThisFrame) ChangeDirection(true);
    }

    /// <summary>
    /// 選択中のコンベアの移動方向を変更する
    /// </summary>
    private void ChangeDirection(bool moveRight)
    {
        if (selectedObject == null) return;

        if (selectedObject.TryGetComponent<Conveyor>(out var conveyor))
        {
            if (conveyor.IsMovingRight == moveRight) return;
            conveyor.IsMovingRight = moveRight;

            if (audioSource.isPlaying) return;
            audioSource.PlayOneShot(audioClip, volume);
        }
    }
}
