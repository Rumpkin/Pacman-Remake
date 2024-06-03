using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Movement : MonoBehaviour
{
    public Vector2 direction { get; private set; }
    [SerializeField] protected float speed;
    [SerializeField] float speedMultiplier = 1f;
    [SerializeField] protected bool frozen;
    [SerializeField] Vector2 initialDirection;    
    [SerializeField] Vector2 nextDirection;
    [SerializeField] Vector3 startingPosition;

    private protected Vector2 targetPosition;

    [SerializeField] public LayerMask obstacle;

    private protected Rigidbody2D rb;

    private void Awake()
    {
        frozen = true;
        rb = GetComponent<Rigidbody2D>();
        startingPosition = transform.position;
    }

    protected void Start()
    {
        ResetState();
    }

    public void ResetState()
    {
        speedMultiplier = 1f;
        direction = initialDirection;
        nextDirection = Vector2.zero;
        transform.position = startingPosition;
        rb.isKinematic = false;
        enabled = true;
    }

    protected void Update()
    {
        if (nextDirection != Vector2.zero)
        {
            SetDirection(nextDirection);
        }
    }

    private protected void FixedUpdate()
    {
        if (!frozen)
        {
            Vector2 position = rb.position;
            Vector2 translation = speed * speedMultiplier * direction * Time.fixedDeltaTime;

            rb.MovePosition(position + translation);
        }
    }

    public void SetPosition(Vector2 position)
    {
        Debug.Log("Moving to " + position);        
        transform.position = position;
    }

    public void SetDirection(Vector2 direction, bool forced = false)
    {
        if (direction != new Vector2(0, 0))
        {
            if (forced || !Occupied(direction))
            {
                this.direction = direction;
                nextDirection = Vector2.zero;
            }
            else
            {
                nextDirection = direction;
            }
        }
        
    }

    public void SetFrozen(bool frozen)
    {
        this.frozen = frozen;
    }

    public bool Occupied(Vector2 direction)
    {
        RaycastHit2D hit = Physics2D.BoxCast(transform.position, Vector2.one * 0.85f, 0, direction, 2, obstacle);
        return hit.collider != null;
    }
}
