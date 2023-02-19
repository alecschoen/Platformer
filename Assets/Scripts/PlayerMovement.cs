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
    private float maxHorizontalVelocity = 0f;
    private bool isBouncy = false;
    private float previousVelocityY;
    private float bounceRetention = 0f;
    private bool hasLanded = true;


    [SerializeField] private LayerMask[] jumpableGrounds;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private ParticleSystem dustParticle;
    [SerializeField] private ParticleSystem snowPaticle;
    [SerializeField] private ParticleSystem sandPaticle;
    [SerializeField] private ParticleSystem slimePaticle;

    private bool canDoubleJump = true;

    // Start is called before the first frame update
    private void Start()
    {
        //Debug.Log("Script intitialised");
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
        Bounce();
        CharacterAnimation();

    }


    private void CharachterMovent()
    {
        if (canMove)
        {
            SetMovementVelocity();
            //Debug.Log("inupt horizontal; " + Input.GetAxisRaw("Horizontal"));
            //rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 7f, rb.velocity.y);
            //Debug.Log("Velocity.x: "+rb.velocity.x);
            //Debug.Log("groundType: "+ groundType);
            //Debug.Log("previous velocity y: " + previousVelocityY);
            float movement = rb.velocity.x + (horizontalMovement * movementVelocity) * Time.deltaTime;
            if (!isSlippery)
            {
                rb.velocity = new Vector2((horizontalMovement * movementVelocity), rb.velocity.y);
            }
            else
            {
                if (movement < maxHorizontalVelocity * -1)
                {
                    movement = maxHorizontalVelocity * -1;
                }
                else if (movement > maxHorizontalVelocity)
                {
                    movement = maxHorizontalVelocity;
                }

                rb.velocity = new Vector2(movement, rb.velocity.y);
            }
            if (movement > 0.5f || movement < -0.5f)
            {
                ParticleEmission();
            }

            if (Input.GetButtonDown("Jump"))
            {
                Jump();
            }
            if (IsGrounded())
            {
                canDoubleJump = true;
                LandingParticleEffect();
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
            switch (groundType)
            {
                case 1: // Snow Particle Play
                    snowPaticle.Play();
                    break;
                case 2: // Sand Particle Play
                    sandPaticle.Play();
                    break;
                case 3: // Slime Particle Play
                    slimePaticle.Play();
                    break;
            }
        }
    }

    private void LandingParticleEffect()
    {
        if (!hasLanded && IsGrounded())
        {
            hasLanded= true;
            ParticleEmission();
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
                hasLanded = false;
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

    private void Bounce()
    {
        if (IsGrounded() && isBouncy && previousVelocityY < -3f)
        {
            rb.velocity = new Vector2(rb.velocity.x ,previousVelocityY * -bounceRetention) ;
        }
        previousVelocityY = rb.velocity.y;
    }

    //set movement velocity according to terrain type

    private void SetMovementVelocity()
    {
        if(IsGrounded())
        {
            switch (groundType)
            {
                case 0: //Normal
                    movementVelocity = 7f;
                    jumpVelocity = 14f;
                    isSlippery = false;
                    hasMovementParticle = false;
                    rb.gravityScale = 3;
                    isBouncy = false;
                    break;
                case 1: //Ice
                    rb.gravityScale = 1;
                    movementVelocity = 20f;
                    jumpVelocity = 14f;
                    isSlippery = true;
                    hasMovementParticle = true;
                    maxHorizontalVelocity = 11f;
                    isBouncy = false;
                    break;
                case 2: //Sand
                    rb.gravityScale = 4;
                    movementVelocity = 18f;
                    jumpVelocity = 10f;
                    isSlippery = true;
                    hasMovementParticle = true;
                    maxHorizontalVelocity = 7f;
                    isBouncy = false;
                    break;
                case 3://Slime
                    rb.gravityScale = 3;
                    movementVelocity = 7f;
                    isSlippery = false;
                    hasMovementParticle = true;
                    jumpVelocity = 20f;
                    isBouncy = true;
                    bounceRetention = 0.75f;
                    break;
            }
        }
        else //Air
        {
            rb.gravityScale = 3;
            movementVelocity = 7f;
            jumpVelocity = 14f;
            hasMovementParticle = false;
            isSlippery = false;
            isBouncy = false;
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

}
