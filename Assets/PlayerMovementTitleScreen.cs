using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class PlayerMovementTitleScreen : MonoBehaviour
{
    [SerializeField] private ParticleSystem dust;
    [SerializeField] private AudioSource deathSound;
    [SerializeField] private GameObject saw;

    private Rigidbody2D rb;
    private bool isRunning = false;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    private bool hasRespawned = false;

    private void Start()
    { 
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer= GetComponent<SpriteRenderer>();
        Invoke("StartToRun", 4);
        Invoke("Jump", 5);
        Invoke("DoubleJump", 5.5f);
        Invoke("Jump", 6.5f);
        Invoke("DoubleJump", 7.5f);
        Invoke("Jump", 9f);
        Invoke("DoubleJump", 9.2f);
        Invoke("Jump", 10f);
        Invoke("Jump", 12f);
        Invoke("DoubleJump", 12.2f);

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
        if (hasRespawned && saw.GetComponent<Transform>().position.x >= 3.23)
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
    }

    private void Run()
    {
        rb.velocity = new Vector2(7f, rb.velocity.y);
    }

    private void Jump()
    {
        rb.velocity = new Vector2(rb.velocity.x, 14);
    }

    private void DoubleJump()
    {
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
        rb.position = new Vector2(-5, -1);
        hasRespawned = true;
        isRunning = false;
    }
}
