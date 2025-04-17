using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PathFinder
{
    private readonly IGrid _grid;
    private readonly IGridContent _gridContent;

    public bool IsPathSuccessful { get; private set; }
    public List<IGridNode> Path { get; private set; }

    public PathFinder(IGrid grid, IGridContent gridContent)
    {
        Path = new List<IGridNode>();
        _gridContent = gridContent;
        _grid = grid;
    }

    public class NodeRecord
    {
        public IGridNode Node;
        public NodeRecord CameFrom;
        public int GCost;
        public int HCost;
        public int FCost => GCost + HCost;

        public NodeRecord(IGridNode node)
        {
            Node = node;
        }
    }

    public void FindPath(IGridNode startNode, IGridNode targetNode)
    {
        IsPathSuccessful = false;

        var openList = new List<NodeRecord>();
        var closedList = new HashSet<IGridNode>();

        NodeRecord startRecord = new NodeRecord(startNode)
        {
            GCost = 0,
            HCost = _grid.GetGridDistance(startNode, targetNode)
        };

        openList.Add(startRecord);

        while (openList.Count > 0)
        {
            NodeRecord current = openList.OrderBy(n => n.FCost).First();

            if (current.Node == targetNode)
            {
                IsPathSuccessful = true;
                Path = ReconstructPath(current);
            }

            openList.Remove(current);
            closedList.Add(current.Node);

            foreach (IGridNode neighbor in _grid.GetNeighbours(current.Node))
            {
                if (neighbor.IsOccupiedFor(_gridContent) || closedList.Contains(neighbor))
                    continue;

                int tempGCost = current.GCost + 1;

                NodeRecord neighborRecord = openList.FirstOrDefault(n => n.Node == neighbor);
                if (neighborRecord == null)
                {
                    neighborRecord = new NodeRecord(neighbor)
                    {
                        GCost = tempGCost,
                        HCost = _grid.GetGridDistance(neighbor, targetNode),
                        CameFrom = current
                    };
                    openList.Add(neighborRecord);
                }
                else if (tempGCost < neighborRecord.GCost)
                {
                    neighborRecord.GCost = tempGCost;
                    neighborRecord.CameFrom = current;
                }
            }
        }
    }

    public static bool CheckPath(IGrid grid, IGridContent content, IGridNode startNode, IGridNode targetNode)
    {
        if (startNode == targetNode)
            return true;

        var openList = new List<IGridNode> { startNode };
        var closedList = new HashSet<IGridNode>();

        while (openList.Count > 0)
        {
            IGridNode current = openList[0];
            openList.RemoveAt(0);
            closedList.Add(current);

            foreach (IGridNode neighbor in grid.GetNeighbours(current))
            {
                if (closedList.Contains(neighbor) || neighbor.IsOccupiedFor(content))
                    continue;

                if (neighbor == targetNode)
                    return true;

                if (!openList.Contains(neighbor))
                    openList.Add(neighbor);
            }
        }

        return false;
    }

    public static IGridNode GetValidAttackPosition(
    IGridNode targetNode,
    IGridNode myNode,
    float attackRange,
    IGridContent attackerContent,
    IGrid grid
)
    {
        IGridNode bestNode = null;
        float closestToMe = float.MaxValue;

        foreach (var node in GridManager.Instance.GetAllNodes)
        {
            float distanceToTarget = GridManager.Instance.GetWorldDistance(node, targetNode);

            if (distanceToTarget > attackRange)
                continue;

            if (node != myNode && node.IsOccupiedFor(attackerContent))
                continue;

            if (node != myNode && !CheckPath(grid, attackerContent, myNode, node))
                continue;

            float distanceToMe = GridManager.Instance.GetWorldDistance(node, myNode);

            if (distanceToMe < closestToMe)
            {
                closestToMe = distanceToMe;
                bestNode = node;
            }
        }

        return bestNode;
    }

    private List<IGridNode> ReconstructPath(NodeRecord endNode)
    {
        List<IGridNode> path = new List<IGridNode>();
        NodeRecord current = endNode;

        while (current != null)
        {
            path.Add(current.Node);
            current = current.CameFrom;
        }

        path.Reverse();
        return path;
    }

}
