using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public float moveSpeed;
    public float jumpForce;

    private bool isJumping;
    private bool isGrounded;
    
    public Transform groundCheckLeft;
    public Transform groundCheckRight;

    public Rigidbody2D rb;
    public Animator animator;
    public SpriteRenderer spriteRenderer;


    private Vector3 velocity = Vector3.zero;

    void FixedUpdate()
    {

        isGrounded = Physics2D.OverlapArea(groundCheckLeft.position, groundCheckRight.position);        


        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;

        if (Input.GetButtonDown("Jump") && isGrounded == true)
        {
            isJumping = true;
        }

        MovePlayer(horizontalMovement);

        Flip(rb.velocity.x);

        float characterVelocity = Mathf.Abs(rb.velocity.x);
        animator.SetFloat("Speed", rb.velocity.x);
    }

    void MovePlayer(float _horizontalMouvement) 
    {
        Vector3 targetVelocity = new Vector2(_horizontalMouvement, rb.velocity.y);
        rb.velocity = Vector3.SmoothDamp(rb.velocity, targetVelocity, ref velocity, .05f);

        if(isJumping == true)
        {
            rb.AddForce(new Vector2(0f, jumpForce));
            isJumping = false;
        }
    }

    void Flip(float _velocity)
    {
        if (_velocity > 01f)
        {
            spriteRenderer.flipX = false;
        }
        else if (_velocity < -01f)
        {
            spriteRenderer.flipX = true;
        }
    }
}
