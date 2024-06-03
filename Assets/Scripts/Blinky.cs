using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blinky : GhostMovement
{
    private void Start()
    {
        base.Start();
        GameManager.Blinky = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6) {
            
            
            switch (actionState)
            {
                case (state.Scatter):
                    MoveTowardsPosition(collision.GetComponent<Node>(), _scatterPosition.position);
                    break;

                case (state.Chase):
                    MoveTowardsPosition(collision.GetComponent<Node>(), GameManager.Pacman.transform.position);
                    break;

                case (state.Frightened):
                    MoveTowardsPosition(collision.GetComponent<Node>(), new Vector2(0, 0));
                    break;
            }

        }
    }
}
