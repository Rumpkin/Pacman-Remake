using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : Movement
{
    public enum state
    {
        Chase,
        Scatter,
        Frightened,
        Eaten
    }

    public state actionState = state.Chase;    

    [SerializeField] protected Transform _scatterPosition;
    [SerializeField] protected LayerMask _nodeLayerMask;

    [SerializeField] bool inGhostHouse;
    private protected bool _frightened;

    protected void Start()
    {
        base.Start();                     
    }


    public void SwitchState(state _newState, bool switchDirection = false)
    {
        if (actionState != state.Frightened && actionState != state.Eaten)
            actionState = _newState;
        else if (actionState == state.Frightened && _newState == state.Eaten)
            actionState = _newState;

        if (switchDirection && actionState != state.Eaten)
            SetDirection(-direction, true);

        if (actionState == state.Frightened && actionState != state.Eaten)
        {
            CancelInvoke("FrightenedReset");
            Invoke("FrightenedReset", 10);
        }
    }

    public void FrightenedReset()
    {
        if (GameManager.Instance.wave % 2 == 0)
            actionState = state.Scatter;
        else
            actionState = state.Chase;
    }
    

    public void LeaveGhostHouse()
    {
        transform.position = new Vector3(2, -2.5f, 0);
        GetComponent<Animator>().enabled = false;
    }

    protected void MoveTowardsPosition(Node _startNode, Vector2 _targetPosition)
    {
        if (actionState != state.Eaten)
        {
            SetDirection(Pathfind(direction, _startNode, _targetPosition));
        }
        else
        {
            SetDirection(Pathfind(direction, _startNode, new Vector2(2,-2)));
        }
        
    }

    public Vector2 Pathfind(Vector2 direction, Node startNode, Vector2 endPosition)
    {

        float closestDistance = Mathf.Infinity;
        Node closestNode = null;
        int numDirections = 0;

        List<Vector2> validDirections = new List<Vector2>(0);

        foreach (Node neighbor in startNode.neighborNodes)
        {
            if ((Vector2)(neighbor.transform.position - startNode.transform.position).normalized != -direction)
            {
                validDirections.Add((neighbor.transform.position - startNode.transform.position).normalized);
                if (Vector2.Distance(neighbor.transform.position, endPosition) < closestDistance)
                {
                    closestDistance = Vector2.Distance(neighbor.transform.position, endPosition);
                    closestNode = neighbor;
                }
                numDirections++;
            }
        }

        if (actionState != state.Frightened)
            return (closestNode.transform.position - startNode.transform.position).normalized;
        else
        {
            Debug.Log("-");

            foreach (Vector2 dir in validDirections)
            {
                Debug.DrawLine(transform.position, (Vector2)transform.position + dir, Color.red);
                Debug.Log(dir);
            }
            return validDirections[Random.Range(0, validDirections.Count)];
        }
    }

    public void Eaten()
    {
        SwitchState(state.Eaten, true);        
    }

}
