using UnityEngine;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using System;

public class GridManager : MonoBehaviour
{
    [Header("グリッド設定")]
    [SerializeField] private int rows = 2; // 行
    [SerializeField] private int cols = 3; // 列

    // グリッド座標(行、列)をキーにGameObjectを管理するDictionary
    private Dictionary<Vector2Int, GameObject> grid;

    [Header("オブジェクト"), SerializeField]
    private List<GameObject> objects;

    //現在選択中の座標
    private Vector2Int currentPos = Vector2Int.zero;

    // 選択中のオブジェクトが変わった時に発火するイベント
    public event Action<GameObject> OnSelectedObjectChanged;

    private void Start()
    {
        InitializeGrid();
    }

    private void Update()
    {
        HandleCursor();
    }

    /// <summary>
    /// グリッドを初期化する(座標とオブジェクトを対応させる)
    /// </summary>
    private void InitializeGrid()
    {
        grid = new Dictionary<Vector2Int, GameObject>();

        int index = 0;
        for (int r = 0; r < rows; r++) // 行ループ
        {
            for (int c = 0; c < cols; c++) // 列ループ
            {
                // 配列の範囲内でかつnullでない場合登録
                if (index < objects.Count && objects[index] != null)
                {
                    grid[new Vector2Int(r, c)] = objects[index];
                }
                index++;
            }
        }

        HighlightSelected();

        // 選択オブジェクト変更を通知
        OnSelectedObjectChanged?.Invoke(GetSelectObject());
    }


    
    /// <summary>
    /// カーソルを移動し、選択オブジェクトの変更を通知する
    /// </summary>
    private void MoveCursor(int rowPower, int colPower)
    {
        ResetHighlight();

        Vector2Int direction = new Vector2Int(rowPower, colPower);
        Vector2Int newPos = currentPos + direction;

        // 登録されているオブジェクトが見つかるまで進む
        while (IsInBounds(newPos))
        {
            if (grid.ContainsKey(newPos))
            {
                currentPos = newPos;
                break;
            }
            newPos += direction;
        }

        HighlightSelected();

        OnSelectedObjectChanged?.Invoke(GetSelectObject());
    }

    /// <summary>
    /// 矢印キーでカーソルを移動する
    /// </summary>
    private void HandleCursor()
    {
        if (Keyboard.current.upArrowKey.wasPressedThisFrame) MoveCursor(-1, 0);
        if (Keyboard.current.downArrowKey.wasPressedThisFrame) MoveCursor(1, 0);
        if (Keyboard.current.leftArrowKey.wasPressedThisFrame) MoveCursor(0, -1);
        if (Keyboard.current.rightArrowKey.wasPressedThisFrame) MoveCursor(0, 1);
    }

    /// <summary>
    /// 選択中のオブジェクトを取得する
    /// </summary>
    /// <returns></returns>
    public GameObject GetSelectObject()
    {
        if (grid.TryGetValue(currentPos, out GameObject obj))
        {
            return obj;
        }

        return null;
    }

    /// <summary>
    /// グリッドの範囲内かどうかを判定する
    /// </summary>
    private bool IsInBounds(Vector2Int pos)
    {
        return pos.x >= 0 && pos.x < rows && pos.y >= 0 && pos.y < cols;
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
            renderer.material.color = Color.red;
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
