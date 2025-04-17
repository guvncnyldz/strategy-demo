using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGrid
{
   public List<IGridNode> GetNeighbours(IGridNode node, bool isCross = false);
   public Vector2 GetWorldPositionByGridNode(IGridNode gridNode);
   public IGridNode GetGridNodeByWorldPosition(Vector2 worldPos,bool closest = true);
   public bool TryGetGrid(int x, int y, out IGridNode node);
   public float GetWorldDistance(IGridNode a, IGridNode b);
   public int GetGridDistance(IGridNode a, IGridNode b);
}
