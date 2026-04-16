using UnityEngine;
using UnityEngine.InputSystem;

public class ConveyorController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    private GameObject selectedObject;


    /// <summary>
    /// Startより先に実行されるAwakeでイベントを購読する
    /// </summary>
    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        gridManager.OnSelectedObjectChanged += OnSelectedChanged;
    }

    private void OnSelectedChanged(GameObject obj)
    {
        selectedObject = obj;
    }

    private void Update()
    {
        if (selectedObject == null) return;

        if (!selectedObject.CompareTag(Tags.ConveyorBelt)) return;

        if (Keyboard.current.aKey.wasPressedThisFrame) ChangeDirection(false);
        if (Keyboard.current.dKey.wasPressedThisFrame) ChangeDirection(true);
    }

    private void ChangeDirection(bool moveRight)
    {
        if (selectedObject == null) return;

        if (selectedObject.TryGetComponent<Conveyor>(out var conveyor))
        {
            conveyor.IsMovingRight = moveRight;
        }
    }
}
