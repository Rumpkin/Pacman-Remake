using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleporter : MonoBehaviour
{
    [SerializeField] Vector2 TargetPosition;

    private void OnTriggerEnter2D(Collider2D collision)
    {        
        if (collision.gameObject.CompareTag("Pacman") || collision.gameObject.CompareTag("Ghost"))
        {            
            collision.GetComponent<Movement>().SetPosition(TargetPosition);
        }
    }
}
