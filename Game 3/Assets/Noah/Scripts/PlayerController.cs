using UnityEngine;
using static UnityEngine.LightAnchor;

public class PlayerController : MonoBehaviour
{

    private Rigidbody2D rb;

    [SerializeField] float moveSpeed;
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    private bool isGrounded;
    [SerializeField] Transform groundCheckPos;

    [SerializeField] float jumpForce;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();
        jump();
        
    }
    void FixedUpdate()
    {
        Movement();
    }

    private void Movement()
    {
        float movementx = Input.GetAxis("Horizontal");
        rb.linearVelocity = new Vector2(movementx * moveSpeed, rb.linearVelocity.y);
    }

    private void jump()
    {
        if(Input.GetKeyDown(KeyCode.W) && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
        
    }

    private void GroundCheck()
    {
        isGrounded = Physics2D.OverlapBox(transform.position, groundCheckSize, 0f, groundLayer);
        
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.position, groundCheckSize);//visulises the gound check box for debuging 
    }
}
