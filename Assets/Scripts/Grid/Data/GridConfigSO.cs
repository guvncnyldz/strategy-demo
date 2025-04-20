using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GridConfig", menuName = "ScriptableObjects/Config/GridConfig")]
public class GridConfigSO : ScriptableObject
{
    //Only the visual grid is resizable; buildings and units have fixed sizes due to limited case time. So, that's why SIZE is constant.
    public const float SIZE_X = 0.32f;
    public const float SIZE_Y = 0.32f;

    [Tooltip("If using a visual grid, the limit is 1024 due to GPU Instancing.")]
    public Vector2Int Count;
    public GridFactory GridFactory;

    public Vector2 GetOrigin()
    {
        float yOrigin = 0 - (Count.y * 0.5f) * SIZE_Y;
        float xOrigin = 0 - (Count.x * 0.5f) * SIZE_X;

        return new Vector2(xOrigin, yOrigin);
    }
}
