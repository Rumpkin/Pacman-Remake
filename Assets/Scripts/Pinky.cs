using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pinky : GhostMovement
{
    private void Start()
    {
        base.Start();
        GameManager.Pinky = gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {

            switch (actionState)
            {
                case (state.Scatter):
                    MoveTowardsPosition(collision.GetComponent<Node>(), _scatterPosition.position);
                    break;

                case (state.Chase):
                    Vector2 pacmanPos = GameManager.Pacman.transform.position;
                    pacmanPos += GameManager.Pacman.GetComponent<Movement>().direction * 4;

                    MoveTowardsPosition(collision.GetComponent<Node>(), pacmanPos);

                    break;

                case (state.Frightened):
                    MoveTowardsPosition(collision.GetComponent<Node>(), new Vector2(0, 0));
                    break;
            }
 
        }
    }
}
