using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Movement))]
public class pacmanMovementController : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private Collider2D collider;
    private Movement movement;
    private Animator animator;
    private Rigidbody2D rb;
    public bool started;

    private void Awake()
    {
        GameManager.Pacman = gameObject;
        animator = GetComponent<Animator>();
        animator.speed = 0;
        started = false;

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        collider = GetComponent<Collider2D>();
        movement = GetComponent<Movement>();
    }

    private void Update()
    {
        if (started)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
            {
                movement.SetDirection(Vector2.up);
            }
            else if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
            {
                movement.SetDirection(Vector2.down);
            }
            else if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
            {
                movement.SetDirection(Vector2.left);
            }
            else if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
            {
                movement.SetDirection(Vector2.right);
            }

            // rotates pacman to direction it is moving
            float angle = Mathf.Atan2(movement.direction.y, movement.direction.x);
            if (animator.GetCurrentAnimatorStateInfo(0).IsName("Chomp"))
            {
                transform.rotation = Quaternion.AngleAxis(angle * Mathf.Rad2Deg, Vector3.forward);
            }

        
            RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.85f, 0, movement.direction, 0.1f, movement.obstacle);
            if (hit.collider != null && animator.GetCurrentAnimatorStateInfo(0).IsName("Chomp"))
            {
                GetComponent<Animator>().speed = 0;
            }
            else
            {
                GetComponent<Animator>().speed = 1;
            }
        }
    }    

    public void ResetState()
    {
        enabled = true;
        collider.enabled = true;
        movement.ResetState();
        gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ghost"))
        {
            if (!GameManager.Instance.pacmanScaryMode)
            {
                GameManager.Instance.WipeOut();

                animator.SetTrigger("Dead");
            }
            else
            {
                collision.gameObject.GetComponentInParent<GhostMovement>().Eaten();
            }
        }
    }       
}
