using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBase : MonoBehaviour, IGridContent
{
    protected BuildingSO _buildingSO;
    protected SpriteRenderer _spriteRenderer;

    public virtual void Initialize(BuildingSO buildingSO)
    {
        _buildingSO = buildingSO;

        _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        _spriteRenderer.sprite = _buildingSO.BuildingImage;
        _spriteRenderer.color = Color.white;
    }
    
    public bool IsOcuppyShareAvailable(IGridContent newGridContent)
    {
        return false;
    }
}