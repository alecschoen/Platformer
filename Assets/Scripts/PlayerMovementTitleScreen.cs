using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovementTitleScreen : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject saw;
    [SerializeField] private AudioSource jumpSound;

    private Rigidbody2D rb;
    private bool isRunning = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool hasRespawned = true;
    private float previousSawPosX;

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        
    }

    private void Update()
    {
        if (isRunning)
        {
            Run();
        }
        if (rb.position.x > 11.7)
        {
            rb.position =new Vector2(-12, rb.position.y) ;
           
        }
        AnimationLoop();

    }

    private void AnimationLoop()
    {
        
        if (hasRespawned && saw.GetComponent<Transform>().position.x >= 3.23f && previousSawPosX < 3.23f)
        {
            hasRespawned= false;
            StartToRun();
            Invoke("Jump", 1);
            Invoke("DoubleJump", 1.5f);
            Invoke("Jump", 2.5f);
            Invoke("DoubleJump", 3.5f);
            Invoke("Jump", 5f);
            Invoke("DoubleJump", 5.2f);
            Invoke("Jump", 6f);
            Invoke("Jump", 8f);
            Invoke("DoubleJump", 8.2f);
        }

        previousSawPosX =  saw.GetComponent<Transform>().position.x;

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Trap"))
        {
            deathSound.Play();
            animator.SetTrigger("FakeDeath");
            rb.bodyType = RigidbodyType2D.Static;
        }
    }


    private void FixedUpdate()
    {
        CharacterAnimation();
    }

    private void StartToRun()
    {
        isRunning= true;
        animator.SetBool("IsAppearing", false);

    }

    private void Run()
    {
        rb.velocity = new Vector2(7f, rb.velocity.y);
    }

    private void Jump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, 14);
    }

    private void DoubleJump()
    {
        jumpSound.Play();
        rb.velocity = new Vector2(rb.velocity.x, 14);
        animator.SetBool("isDoubleJumping", true);
        dust.Play();
    }

    private void CharacterAnimation()
    {
        //Debug.Log(rb.velocity.y);
        if (rb.velocity.x < -0.1)
        {
            spriteRenderer.flipX = true;
        }
        else if (rb.velocity.x > 0.1)
        {
            spriteRenderer.flipX = false;
        }

        if (isRunning)
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
    public void Respawn()
    {
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.velocity = Vector2.zero;
        rb.position = new Vector2(-5, -1);
        CancelInvoke();
        Invoke("IsAppearing", 1f);
        isRunning = false;
    }

    public void SetHasRespawned()
    {
        hasRespawned = true;
    }

    public void GetStopped()
    {
        CancelInvoke();
        rb.bodyType = RigidbodyType2D.Static;
    }

    private void IsAppearing()
    {
        spriteRenderer.enabled = true;
        animator.SetBool("IsAppearing", true);
    }
}
