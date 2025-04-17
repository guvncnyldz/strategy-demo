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

    public List<IGridNode> FindPath(IGridNode startNode, IGridNode targetNode)
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
                if (neighbor.IsOccupied(_gridContent) || closedList.Contains(neighbor))
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

        return null;
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
