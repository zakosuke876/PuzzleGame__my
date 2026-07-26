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

        GameObject near = null;
        Vector2Int nearPos = currentPos;
        int bestScore = int.MaxValue;

        foreach (var kvp in grid)
        {
            Vector2Int pos = kvp.Key;
            if (pos == currentPos) continue;

            int dRow = pos.x - currentPos.x;
            int dCol = pos.y - currentPos.y;

            // 移動方向と逆方向は除外
            if (rowPower != 0 && dRow * rowPower <= 0) continue;
            if (colPower != 0 && dCol * colPower <= 0) continue;

            // 主軸距離 * 重み + 副幅距離
            int primary = Mathf.Abs(rowPower != 0 ? dRow : dCol);
            int secondary = Mathf.Abs(rowPower != 0 ? dCol : dRow);
            int score = primary * 1000 + secondary;

            if (score < bestScore)
            {
                bestScore = score;
                nearPos = pos;
                near = kvp.Value;
            }
        }

        if (near != null) currentPos = nearPos;

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
    /// 選択しているオブジェクトの色を変更
    /// </summary>
    private void HighlightSelected()
    {
        GameObject obj = GetSelectObject();
        if (obj == null) return;

        if (obj.TryGetComponent<Renderer>(out var renderer))
        {
            renderer.material.color = Color.blue;
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
