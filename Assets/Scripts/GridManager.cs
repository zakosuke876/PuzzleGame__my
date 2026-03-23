using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;

public class GridManager : MonoBehaviour
{
    [Header("グリッド設定")]
    [SerializeField] private int rows = 2; // 行
    [SerializeField] private int cols = 3; // 列

    [Header("オブジェクト"), SerializeField]
    private List<GameObject> objects;

    [Header("床オブジェクトの移動速度"), SerializeField]
    private float floorMoveSpeed = 0f;

    // グリッド座標(行、列)をキーにGameObjectを管理するDictionary
    private Dictionary<Vector2Int, GameObject> grid;

    //現在選択中の座標
    private Vector2Int currentPos = Vector2Int.zero;
    void Start()
    {
        InitializeGrid();
    }

    void Update()
    {
        HandleConveyor();
        HandleMoveFloor();
    }

    /// <summary>
    /// objectsリストからグリッド用Dictionaryを生成する
    /// null要素はスキップする
    /// </summary>
    private void InitializeGrid()
    {
        grid = new Dictionary<Vector2Int, GameObject>();

        int index = 0;
        for (int r = 0; r < rows; r++) // 行ループ
        {
            for (int c = 0; c < cols; c++) // 列ループ
            {
                if (index < objects.Count && objects[index] != null) // 配列の範囲内でかつnullでない場合登録
                {
                    grid[new Vector2Int(r, c)] = objects[index];
                }
                index++;
            }
        }

        HighlightSelected(); // 最初の位置をハイライト
    }

    /// <summary>
    /// 矢印キーでカーソル移動、A/Dキーでベルトコンベアの向きを変更
    /// </summary>
    private void HandleConveyor()
    {
        // 矢印キーでの操作
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) MoveCursor(-1, 0);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame)  MoveCursor(-1, 0);
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) MoveCursor(0, -1);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) MoveCursor(0, 1);

        GameObject obj = GetSelectObject();
        if (obj != null && obj.CompareTag(Tags.ConveyorBelt))  // A/Dキーはベルトコンベアの時だけ有効にする
        {
            // A/Dキーでコンベアの向きを変更
            //if (Keyboard.current.aKey.wasPressedThisFrame)
        }
    }

    /// <summary>
    /// W/Sキーで選択中の床を上下に移動
    /// </summary>
    private void HandleMoveFloor()
    {
        GameObject obj = GetSelectObject();

        Vector2 move = Vector2.zero;

        if (obj != null && obj.CompareTag(Tags.MoveFloor))
        {
            if (Keyboard.current.wKey.wasPressedThisFrame)
            {
                move = Vector2.up;
            }
            else if (Keyboard.current.sKey.wasPressedThisFrame)
            {
                move = Vector2.down;
            }

            if (move != Vector2.zero)
            {
                obj.transform.position += (Vector3)(move * floorMoveSpeed * Time.deltaTime);
            }
        }
    }

    /// <summary>
    /// 選択中のオブジェクトを取得
    /// </summary>
    GameObject GetSelectObject()
    {
        if (grid.TryGetValue(currentPos, out GameObject obj))
        {
            return obj;
        }
        return null;
    }

    /// <summary>
    /// カーソル移動
    /// </summary>
    private void MoveCursor(int rowPower, int colPower)
    {
        ResetHighlight();

        Vector2Int direction = new Vector2Int(rowPower, colPower);
        Vector2Int newpos = currentPos + direction;

        while (IsInBounds(newpos))
        {
            if (grid.ContainsKey(newpos))
            {
                currentPos = newpos;
                break;
            }
            newpos += direction;
        }

        HighlightSelected();
    }

    bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
    }

    /// <summary>
    /// コンベアの向きを変更 (trueなら右向き、falseなら左向き)
    /// </summary>
    /// 
    private void ChangeConveyorDirection(bool moveRight)
    {
        GameObject obj = GetSelectObject();
        if (obj == null) return;

        if (obj.TryGetComponent<Conveyor>(out var conveyor))
        {
            conveyor.MoveRight = moveRight;
        }
    }



    /// <summary>
    /// 選択しているオブジェクトの色を変更
    /// </summary>
    private void HighlightSelected()
    {
        GameObject obj = GetSelectObject();
        if (obj == null) return;

        if (obj.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = Color.yellow;
        }
    }

    /// <summary>
    /// 色をリセットする
    /// </summary>
    void ResetHighlight()
    {
        GameObject obj = GetSelectObject();
        if (obj == null) return;

        if (obj.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = Color.white;
        }
    }
}
