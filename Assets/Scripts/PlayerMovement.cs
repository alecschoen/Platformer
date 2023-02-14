using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool canMove = true;

    [SerializeField] private LayerMask jumpableGround;
    [SerializeField] private AudioSource jumpSound;
    [SerializeField] private ParticleSystem dust;

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
            rb.velocity = new Vector2(Input.GetAxisRaw("Horizontal") * 7f, rb.velocity.y);
            if (Input.GetButtonDown("Jump"))
            {
                if (isGrounded())
                {
                    rb.velocity = new Vector2(rb.velocity.x, 14);
                    jumpSound.Play();
                    //Debug.Log("space");
                }
                else if (canDoubleJump)
                {
                    rb.velocity = new Vector2(rb.velocity.x, 14);
                    canDoubleJump = false;
                    animator.SetBool("isDoubleJumping", true);
                    jumpSound.Play();
                    dust.Play();
                }
            }
            if (isGrounded())
            {
                canDoubleJump = true;
            }
        }
    }

    private void CharacterAnimation()
    {
        //Debug.Log(rb.velocity.y);
        if (rb.velocity.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0)
        {
            spriteRenderer.flipX = false;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            animator.SetBool("isMoving", true);
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

    private bool isGrounded()
    {
        //Debug.Log("isGrounded");
        return Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0f , Vector2.down, .1f, jumpableGround);
    }

    private void CanMove()
    {
        canMove = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
    }

    private void CanNotMove()
    {
        canMove = false;
        rb.bodyType = RigidbodyType2D.Static;
    }

}
