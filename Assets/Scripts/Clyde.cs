using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clyde : GhostMovement
{            
    bool _scared;

    private void Start()
    {
        base.Start();
        GameManager.Clyde = gameObject;
        _scared = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) {
            
            switch (actionState)
            {
                case (state.Scatter):
                    _scared = false;
                    MoveTowardsPosition(collision.GetComponent<Node>(), _scatterPosition.position);

                    break;

                case (state.Chase):
                    if (!_scared)
                    {
                        MoveTowardsPosition(collision.GetComponent<Node>(), GameManager.Pacman.transform.position);

                        if (Vector2.Distance(transform.position, GameManager.Pacman.transform.position) <= 9)
                        {
                            _scared = true;
                        }

                    }
                    else
                    {
                        MoveTowardsPosition(collision.GetComponent<Node>(), _scatterPosition.position);

                        if (Vector2.Distance(transform.position, GameManager.Pacman.transform.position) > 9)
                            _scared = false;

                    }
                    break;

                case (state.Frightened):
                    _scared = false;
                    MoveTowardsPosition(collision.GetComponent<Node>(), new Vector2(0,0));
                    break;
            }
                        
        }
    }
    /*
     * private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) {            
            switch (actionState) {
                case (state.Scatter):
                    _scared = false;
                    targetPosition = _scatterNode.transform.position;
                    break;

                case (state.Chase):
                    if (!_scared)
                    {
                        targetPosition = GameManager.Pacman.transform.position;

                        if (Vector2.Distance(transform.position, GameManager.Pacman.transform.position) <= 9)
                        {                                                        
                            _scared = true;                            
                        }

                    }
                    else
                    {
                        targetPosition = _scatterNode.transform.position;

                        if (Vector2.Distance(transform.position, GameManager.Pacman.transform.position) > 9)
                            _scared = false;
                        
                    }
                    break;

                case (state.Frightened):
                    _scared = false;
                    break;
            }
        }
    }*/


}
