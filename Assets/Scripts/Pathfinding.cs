using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding
{
    private Node startNode;
    private Node endNode;    

    public Pathfinding()
    {
        startNode = null;
        endNode = null;
    }

    public Pathfinding(Node _startNode, Node _endNode)
    {
        startNode = _startNode;
        endNode = _endNode;        
    }

    public void SetStartNode(Node startNode)
    {
        this.startNode = startNode;
    }

    public void SetEndNode(Node endNode)
    {
        this.endNode = endNode;
    }

    public List<Node> ReconstructPath(Node endNode)
    {        
        List<Node> path = new List<Node>();

        Node cNode = endNode;

        while (cNode != null)
        {            
            path.Add(cNode);
            cNode = cNode.parentNode;
        }

        path.Reverse();

        return path;
    }

    public List<Node> Pathfind(Vector2 direction)
    {
        List<Node> openList = new List<Node>() { startNode };
        List<Node> closedList = new List<Node>();

        while (openList.Count > 0)
        {            
            Node cNode = GetLowestFCostNode(openList);            
            if (cNode == endNode)
            {
                return ReconstructPath(cNode);
            }
            else
            {                
                openList.Remove(cNode);
                closedList.Add(cNode);

                foreach (Node neighbor in cNode.neighborNodes)
                {
                    if ((Vector2)(neighbor.transform.position - cNode.transform.position).normalized != -direction || cNode != startNode)
                    {
                        if (neighbor.CalculateGCost(cNode) < cNode.gCost && closedList.Contains(neighbor))
                        {
                            neighbor.CacluateCosts(cNode, endNode);
                        }
                        else if (neighbor.CalculateGCost(cNode) < cNode.gCost && openList.Contains(neighbor))
                        {
                            neighbor.CacluateCosts(cNode, endNode);
                        }
                        else if (!openList.Contains(neighbor) && !closedList.Contains(neighbor))
                        {
                            neighbor.CacluateCosts(cNode, endNode);
                            openList.Add(neighbor);
                        }
                    }
                }
            }
        }

        return null;
    }

    public Node GetLowestFCostNode(List<Node> nodes)
    {
        Node lowestCostNode = null;

        foreach (Node node in nodes)
        {
            if (lowestCostNode != null && node.fCost < lowestCostNode.fCost)
                lowestCostNode = node;
            else if (lowestCostNode == null)
                lowestCostNode = node;
        }

        return lowestCostNode;
    }
}
