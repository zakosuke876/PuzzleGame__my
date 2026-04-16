using UnityEngine;
using UnityEngine.InputSystem;

public class MoveFloorController : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;

    [SerializeField] private float floorMoveSpeed = 0f;

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

    public void OnSelectedChanged(GameObject obj)
    {
        selectedObject = obj;
    }

    void Update()
    {
        if (selectedObject == null) return;

        if (!selectedObject.CompareTag(Tags.MoveFloor)) return;

        Vector2 move = Vector2.zero;
        if (Keyboard.current.wKey.IsPressed()) move = Vector2.up;
        if (Keyboard.current.sKey.IsPressed()) move = Vector2.down;

        if (move != Vector2.zero)
        {
            Vector3 localMove = (Vector3)(move * floorMoveSpeed * Time.deltaTime);
            selectedObject.transform.position += localMove;
        }
    }
}
