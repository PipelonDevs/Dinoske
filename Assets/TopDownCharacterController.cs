using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float speed;

    private Rigidbody2D rb;
    float horizontalInput;
    float verticalInput;
    Vector3 movementVector;
    Animator animator;
    public bool canMove = true;
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        movementVector = new Vector3();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        movementVector.x = Input.GetAxis("Horizontal");
        movementVector.y = Input.GetAxis("Vertical");

        FlipCharacter();
    }

    private void FixedUpdate()
    {
        if (movementVector != Vector3.zero && canMove)
        {
            animator.SetBool("Walk", true);
            rb.MovePosition(transform.position + movementVector * speed * Time.fixedDeltaTime);
        }
        else
        {
            animator.SetBool("Walk", false);
            rb.velocity = Vector2.zero;
        }
    }

    private void FlipCharacter()
    {
        if (movementVector.x > 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if (movementVector.x < 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }
}