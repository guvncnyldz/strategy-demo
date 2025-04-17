using System;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour, IGridContent
{
    private SpriteRenderer _spriteRenderer;
    private IGridNode _spawnNode;
    private bool _isConstructable;
    private ProductionBuildingSO _productionBuildingSO;

    private enum SpawnPointStatus
    {
        constructed,
        constructable,
        blocked
    }

    void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void Initialize(ProductionBuildingSO productionBuildingSO)
    {
        _productionBuildingSO = productionBuildingSO;
        _spriteRenderer.sprite = productionBuildingSO.SpawnPointImage;
    }

    public bool CheckSpawnArea(IGridNode originNode, Vector2Int size)
    {
        IGridNode selectedNode = GetEmptyNode(originNode, size);

        _isConstructable = selectedNode != null && !selectedNode.IsOccupied(this);

        if (_isConstructable)
        {
            _spawnNode = selectedNode;
        }

        UpdateVisual(_isConstructable ? SpawnPointStatus.constructable : SpawnPointStatus.blocked);

        if (selectedNode != null)
            transform.position = GridManager.Instance.GetWorldPositionByGridNode(selectedNode);

        gameObject.SetActive(selectedNode != null);

        return _isConstructable;
    }

    IGridNode GetEmptyNode(IGridNode originNode, Vector2Int size)
    {
        Vector2Int origin = originNode.GridPosition;
        IGridNode selectedNode = null;

        for (int x = origin.x - 1; x < origin.x + size.x + _productionBuildingSO.XSize; x++)
        {
            for (int y = origin.y - 1; y < origin.y + size.y + _productionBuildingSO.YSize; y++)
            {
                int xSize = origin.x + _productionBuildingSO.XSize;
                int ySize = origin.y + _productionBuildingSO.YSize;

                bool isInside = x >= origin.x && x < xSize && y >= origin.y && y < ySize;

                if (isInside) continue;

                if (GridManager.Instance.TryGetGrid(x, y, out IGridNode node))
                {
                    selectedNode = node;

                    if (!node.IsOccupied(this))
                    {
                        return node;
                    }
                }
            }
        }

        return selectedNode;
    }

    public void OccupySpawnPoint(bool isOccupied)
    {
        if (_spawnNode != null)
        {
            if (isOccupied)
                _spawnNode.Occupy(this);

            transform.position = GridManager.Instance.GetWorldPositionByGridNode(_spawnNode);
            UpdateVisual(SpawnPointStatus.constructed);
        }

        if (!isOccupied)
            _spawnNode.Release(this);
    }

    void UpdateVisual(SpawnPointStatus spawnPointStatus)
    {
        Color color = Color.white;

        switch (spawnPointStatus)
        {
            case SpawnPointStatus.constructed:
                color.a = 1;
                break;
            case SpawnPointStatus.constructable:
                color.a = 0.7f;
                break;
            case SpawnPointStatus.blocked:
                color = Color.red;
                color.a = 0.7f;
                break;
        }

        _spriteRenderer.color = color;
    }

    public bool IsOcuppyShareAvailable(IGridContent newGridContent)
    {
        if (newGridContent is UnitController)
            return true;

        return false;
    }

    public IGridNode GetSpawnPoint(IGridContent gridContent)
    {
        if (!_spawnNode.IsOccupied(gridContent))
            return _spawnNode;

        foreach (IGridNode gridNode in GridManager.Instance.GetNeighbours(_spawnNode, true))
        {
            if (!gridNode.IsOccupied(gridContent))
                return gridNode;
        }

        return null;
    }
}