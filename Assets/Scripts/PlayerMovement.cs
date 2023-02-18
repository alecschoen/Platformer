using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool canMove = true;
    private int horizontalMovement = 0;
    private bool hasFinished = false;
    private float movementVelocity = 7f;
    public float jumpVelocity = 14f;
    private int groundType = 0; // 0 == normal terrain // 1 == ice // ...  
    private bool isSlippery = false;
    private bool hasMovementParticle = false;


    [SerializeField] private LayerMask[] jumpableGrounds;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private ParticleSystem snowPaticle;

    private bool canDoubleJump = true;

    // Start is called before the first frame update
    private void Start()
    {
        Debug.Log("Script intitialised");
        rb = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    private void Update()
    {

        CharachterMovent();

    }

    private void FixedUpdate()
    {

        CharacterAnimation();

    }


    private void CharachterMovent()
    {
        if (canMove)
        {
            SetMovementVelocity();
            //Debug.Log("inupt horizontal; " + Input.GetAxisRaw("Horizontal"));
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 7f, rb.velocity.y);
            Debug.Log("Velocity.x: "+rb.velocity.x);
            Debug.Log("Drag: "+ rb.drag);
            if (!isSlippery)
            {
                rb.velocity = new Vector2((horizontalMovement * movementVelocity), rb.velocity.y);
            }
            else
            {
                float movement = rb.velocity.x + (horizontalMovement * movementVelocity)* Time.deltaTime;
                if(movement < -11)
                {
                    movement = -11;
                }
                else if(movement > 11)
                {
                    movement = 11;
                }

                rb.velocity = new Vector2(movement, rb.velocity.y);
                if(movement > 0.5f || movement < -0.5f) 
                {
                    ParticleEmission();
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (IsGrounded())
            {
                canDoubleJump = true;
            }

            if (hasFinished && IsGrounded())
            {
                animator.SetTrigger("IsFinished");
                Invoke("CanNotMove", 0.04f);
            }
        }
    }

    private void ParticleEmission()
    {
        if (hasMovementParticle)
        {
            switch(groundType)
            {
                case 1: // Snow Particle Play
                    snowPaticle.Play();
                    break;
            }
        }
    }

    private void CharacterAnimation()
    {
        if (canMove)
        {
            //Debug.Log(rb.velocity.y);

            if (horizontalMovement != 0)
            {
                animator.SetBool("isMoving", true);

                switch(horizontalMovement)
                {
                    case -1:
                        spriteRenderer.flipX = true;
                        break;
                    case 1:
                        spriteRenderer.flipX = false;
                        break;

                }
            }
            else
            {
                animator.SetBool("isMoving", false);
            }
            if (rb.velocity.y > 0.01)
            {
                animator.SetBool("isFalling", false);
                animator.SetBool("isJumping", true);
            }
            else if (rb.velocity.y < -0.01)
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", true);
                animator.SetBool("isDoubleJumping", false);
            }
            else
            {
                animator.SetBool("isJumping", false);
                animator.SetBool("isFalling", false);
            }
        }
    }

    private bool IsGrounded()
    {
        //Debug.Log("isGrounded");
        //Debug.Log("Boxcollider -> x:" + boxCollider.bounds.size.x + "y:"+ boxCollider.size.y

        for(int i = 0; i < jumpableGrounds.Length; i++)
        {
            //Debug.Log(i);
            if (Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f, Vector2.down, .1f, jumpableGrounds[i]))
            {
                groundType = i;
                return true;
            }
        }
        return false;
    }

    //set movement velocity according to terrain type

    private void SetMovementVelocity()
    {
        if(IsGrounded())
        {
            switch (groundType)
            {
                case 0: //normal
                    movementVelocity = 7f;
                    jumpVelocity = 14f;
                    isSlippery = false;
                    hasMovementParticle = false;
                    rb.gravityScale = 3;
                    break;
                case 1: //ice
                    rb.gravityScale = 1;
                    movementVelocity = 20f;
                    jumpVelocity = 14f;
                    isSlippery = true;
                    hasMovementParticle = true;
                    break;
            }
        }
        else
        {
            rb.gravityScale = 3;
            movementVelocity = 7f;
            jumpVelocity = 14f;
            hasMovementParticle = false;
        }
    }

    private void CanMove()
    {
        canMove = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    public void CanNotMove()
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            jumpSound.Play();
            //Debug.Log("space");
        }
        else if (canDoubleJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpVelocity);
            canDoubleJump = false;
            animator.SetBool("isDoubleJumping", true);
            jumpSound.Play();
            dustParticle.Play();
        }
    }

    public void PressMoveLeft()
    {
        horizontalMovement = -1;
    }

    public void PressMoveRight()
    {
        horizontalMovement = 1;
    }

    public void DePressMove()
    {
        horizontalMovement = 0;
    }

    public void HasFinished()
    {
        hasFinished= true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            Debug.Log("Enter Ice");
            movementVelocity= 3.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Ice"))
        {
            Debug.Log("Exit Ice");
            movementVelocity = 7f;
        }
    }

}
