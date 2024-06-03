using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inky : GhostMovement
{
    private void Start()
    {
        base.Start();
        GameManager.Inky = gameObject;
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
                    Vector2 pacmanPos = GameManager.Pacman.transform.position;
                    pacmanPos += GameManager.Pacman.GetComponent<Movement>().direction * 4;
                    Vector2 localVector = (pacmanPos - (Vector2)GameManager.Blinky.transform.position) * 2;
                    Debug.DrawRay(GameManager.Blinky.transform.position, localVector);

                    MoveTowardsPosition(collision.GetComponent<Node>(), (Vector2)GameManager.Blinky.transform.position + localVector);
                    break;

                case (state.Frightened):
                    MoveTowardsPosition(collision.GetComponent<Node>(), new Vector2(0, 0));
                    break;
            }
            
        }
    }
}
