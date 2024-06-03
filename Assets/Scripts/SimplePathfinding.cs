using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimplePathfinding
{
    
    private Node startNode;
    private Vector2 endPosition;

    public SimplePathfinding()
    {
        startNode = null;
        endPosition = new Vector2(0, 0);
    }

    public SimplePathfinding(Node _startNode, Vector2 _endPosition)
    {
        startNode = _startNode;
        endPosition = _endPosition;
    }

    public void SetStartNode(Node _startNode)
    {
        this.startNode = _startNode;
    }

    public void SetEndPosition(Vector2 endPosition)
    {
        this.endPosition = endPosition;
    }

    /*public Node Pathfind(Vector2 direction)
    {
        float closestDistance = Mathf.Infinity;
        Node closestNode = null;
        foreach (Node neighbor in startNode.neighborNodes)
        {
            if ((Vector2)(neighbor.transform.position - startNode.transform.position).normalized != -direction)
            {
                if (Vector2.Distance(neighbor.transform.position, endPosition) < closestDistance)
                {
                    closestDistance = Vector2.Distance(neighbor.transform.position, endPosition);
                    closestNode = neighbor;
                }
            }
        }

        return closestNode;
    }*/

    public Vector2 Pathfind(Vector2 direction)
    {
        
        float closestDistance = Mathf.Infinity;
        Node closestNode = null;
        int numDirections = 0;

        List<Vector2> validDirections = new List<Vector2>(0);

        foreach (Node neighbor in startNode.neighborNodes)
        {
            if ((Vector2)(neighbor.transform.position - startNode.transform.position).normalized != -direction)
            {
                validDirections.Add(neighbor.transform.position);
                if (Vector2.Distance(neighbor.transform.position, endPosition) < closestDistance)
                {
                    closestDistance = Vector2.Distance(neighbor.transform.position, endPosition);
                    closestNode = neighbor;
                }
                numDirections++;
            }
        }

        return (closestNode.transform.position - startNode.transform.position).normalized;
    }


}
