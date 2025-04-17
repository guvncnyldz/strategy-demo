using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

public class GridGenerator
{
    private GridConfigSO _configSO;

    public GridGenerator(GridConfigSO configSO)
    {
        _configSO = configSO;
    }

    public IGridNode[,] Generate()
    {
        IGridNode[,] gridNodeArray = new IGridNode[_configSO.Count.x, _configSO.Count.y];

        for (int xCounter = 0; xCounter < _configSO.Count.x; xCounter++)
        {
            for (int yCounter = 0; yCounter < _configSO.Count.y; yCounter++)
            {
                Vector2Int gridPosition = new Vector2Int(xCounter, yCounter);

                IGridNode gridNode = _configSO.GridFactory.GetGridNode();
                gridNode.Initialize(gridPosition, _configSO);

                gridNodeArray[xCounter, yCounter] = gridNode;
            }
        }

        return gridNodeArray;
    }
}
