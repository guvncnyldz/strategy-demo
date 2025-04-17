using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour, IGridContent, IInteractable
{
    protected BuildingSO _buildingSO;
    protected SpriteRenderer _spriteRenderer;

    public (string, Sprite) GetInformation()
    {
        return (_buildingSO.BuildingName, _buildingSO.BuildingImage);
    }

    public virtual void Initialize(BuildingSO buildingSO)
    {
        _buildingSO = buildingSO;

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = _buildingSO.BuildingImage;
    }

    public bool IsOcuppyShareAvailable(IGridContent newGridContent)
    {
        return false;
    }
}