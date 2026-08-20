using NUnit.Framework.Internal.Execution;
using Unity.VisualScripting;
using UnityEngine;

using static UnityEngine.LightAnchor;

public class PlayerController : MonoBehaviour
{
    
    private Rigidbody2D rb;
    [Header("Player Speeds")]
    [SerializeField] float moveSpeed;
    [SerializeField] float climbSpeed;
    private float movementX;

    [Header("Jump Height")]
    [SerializeField] float jumpForce;

    [Header("Cround Check")]
    [SerializeField] Vector2 groundCheckSize;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] Transform groundCheckPos;
    private bool isGrounded;

    private bool climb;

    public bool facingRight;

    private Vector2 playerStart;


    [SerializeField] float playerHealth;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        playerStart = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        GroundCheck();   
        Direction();
        jump();
    }
    void FixedUpdate()
    {
        Movement();
        
    }

    private void Movement()
    {
        movementX = Input.GetAxisRaw("Horizontal");
        rb.linearVelocity = new Vector2(movementX * moveSpeed, rb.linearVelocity.y);


        if (climb)
        {
            float movementY = Input.GetAxisRaw("Vertical");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, movementY * climbSpeed);
        }


    }
    void Direction()
    {

        if (movementX > 0 && facingRight)
        {
            flip();
        }
        if (movementX < 0 && !facingRight)
        {
            flip();
        }

    }
    

    private void jump()
    {
        if (Input.GetKeyDown(KeyCode.W) || (Input.GetKeyDown(KeyCode.UpArrow) && isGrounded && !climb))
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            
        }
        
    }
    private void flip()
    {

        facingRight = !facingRight;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }




    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climb = true;
        }
        else
            return;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ladder"))
        {
            climb = false;
        }
        else
            return;
    }




    private void GroundCheck()
    {
        
        isGrounded = Physics2D.OverlapBox(groundCheckPos.transform.position, groundCheckSize, 0f, groundLayer);
        
        Debug.Log(isGrounded);

        
    }

    public void TakeDamage(float bulletDamage)
    {
        playerHealth -= bulletDamage;
        if (playerHealth <= 0) transform.position = playerStart;

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(groundCheckPos.transform.position, groundCheckSize);//visulises the gound check box for debuging 
    }


    
}
