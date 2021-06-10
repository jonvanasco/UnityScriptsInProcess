using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D theRB;

    public float moveSpeed, jumpForce;

    public Transform groundCheckPoint;
    public LayerMask whatisGround;

    private bool isGrounded;

    public Animator anim;
	
	public Transform wallGrabPoint;
	private bool canGrab, isGrabbing;
	private float gravityStore;
	public float wallJumpTime = .1f;
	private float wallJumpCounter;
	public LayerMask whatIsGrabbable;

    // Start is called before the first frame update
    void Start()
    {
        gravityStore = theRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        if(wallJumpCounter <=0)
		{
        theRB.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * moveSpeed, theRB.velocity.y);

        isGrounded = Physics2D.OverlapCircle(groundCheckPoint.position, .2f, whatisGround);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            theRB.velocity = new Vector2(theRB.velocity.x, jumpForce);
        }

        //flip direction
        if (theRB.velocity.x > 0)
        {
            transform.localScale = Vector3.one;
        }
        else if (theRB.velocity.x < 0)
        {
            transform.localScale = new Vector3(-1f, 1, 1f);
        }
        
		// handle wall jumping
		
		canGrab = Physics2D.OverlapCircle(wallGrabPoint.position, .2f, whatisGround);
		isGrabbing = false;
		
		if(canGrab && !isGrounded)
		{
			if((transform.localScale.x == 1f && Input.GetAxisRaw("Horizontal") > 0) || (transform.localScale.x == -1f && Input.GetAxisRaw("Horizontal") < 0))
			
			{
				isGrabbing = true;

			}
					
		}
		
		if(isGrabbing)
		{
			theRB.gravityScale = 0f;
			theRB.velocity = vector2.zero; //x and y set to 0
			
			if(Input.GetButtonDown("Jump"))
			{
				
			wallJumpCounter = wallJumpTime;
			theRB.velocity = new Vector2(-Input.GetAxisRaw("Horizontal") * moveSpeed, jumpForce);
			theRB.gravityScale  = gravityStore;
			isGrabbing = false;
			}	
			
			
		} else
			
		{ 
		
		theRB.gravityScale = gravityStore;
		
		} 
		
		}
		else 
		{ 
		wallJumpCounter -= Time.deltaTime;
		}
        anim.SetFloat("speed", Mathf.Abs(theRB.velocity.x));
        anim.SetBool("isGrounded", isGrounded);
		anime.SetBool("isGrabbing", isGrabbing);
    }
}
