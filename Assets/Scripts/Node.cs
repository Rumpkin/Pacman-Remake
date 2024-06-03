using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    [SerializeField] LayerMask nodeLayerMask;
    [SerializeField] LayerMask objectLayerMask;

    [SerializeField] public List<Node> neighborNodes { get; private set; }

    public Node parentNode { get; private set; }    

    public float fCost { get; private set; } //total estimated cost of path through node
    public float gCost { get; private set; } //cost so far to reach node 
    private float hCost; //estimated cost from n to goal

    
    void Start()
    {
        UpdateNeighborNodes();
    }

    public void UpdateNeighborNodes()
    {
        neighborNodes = new List<Node>(0);

        Collider2D[] hits = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.5f, 1.5f), 0, nodeLayerMask);
        foreach (Collider2D hit in hits)
        {
            if (hit.gameObject != gameObject)
                if (hit.transform.position.x == transform.position.x || hit.transform.position.y == transform.position.y)
                    neighborNodes.Add(hit.GetComponent<Node>());
        }
    }

    public void CacluateCosts(Node currentNode, Node targetNode)
    {
        SetParent(currentNode);

        // calcutes distance from original node to new node 
        gCost = CalculateGCost(currentNode);

        // uses manhattan distance equation to calculate distance from this node to the target node
        hCost = Mathf.Abs(transform.position.x - targetNode.transform.position.x) + Mathf.Abs(transform.position.y - targetNode.transform.position.y);

        // calculates gCost
        fCost = gCost + hCost;
    }

    public float CalculateGCost(Node currentNode)
    {
        return Vector2.Distance(currentNode.transform.position, transform.position) + currentNode.gCost;
    }

    public void SetParent(Node parentNode)
    {
        this.parentNode = parentNode;
    }
}
