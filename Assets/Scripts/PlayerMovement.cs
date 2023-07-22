using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float playerSpeed = 6f;
    [SerializeField] private float climbSpeed = 3f;
    [SerializeField] private float jumpSpeed = 13f;
    [SerializeField] private float gravity = 3;
    [SerializeField] private float deathJump = 10;
    [SerializeField] private AudioClip deathSFX;
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform gun;
    private Vector2 moveInput;
    Rigidbody2D myrigidbody2D;
    private CapsuleCollider2D myBodyCollider;
    private BoxCollider2D myFeetCollider;
    Animator _animator;
    bool startedClimbing = false;
    private bool isAlive;
    
    void Start()
    {
        myrigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        myBodyCollider = GetComponent<CapsuleCollider2D>();
        myFeetCollider = GetComponent<BoxCollider2D>();
        myrigidbody2D.gravityScale = gravity;
        isAlive = true;
    }

    void Update()
    {
        if (!isAlive)
        {
            return;
        }
        Run();
        FLipSprite();
        ClimbLadder();
        Die();
    }

    private void Die()
    {
        if (myBodyCollider.IsTouchingLayers(LayerMask.GetMask("Enemy", "Hazards")))
        {
            isAlive = false;
            _animator.SetTrigger("Dying");
            myrigidbody2D.velocity = new Vector2(0, deathJump);
            AudioSource.PlayClipAtPoint(deathSFX,Camera.main.transform.position);
            GameSession gameSession = FindObjectOfType<GameSession>();
            StartCoroutine(gameSession.ProcessPlayerDeath());
        }
    }

    private void ClimbLadder()
    {
        if (myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ladder")))
        {
            if (Mathf.Abs(moveInput.y) > Mathf.Epsilon)
            {
                startedClimbing = true;
            }

            if (startedClimbing)
            {
                myrigidbody2D.velocity = new Vector2(myrigidbody2D.velocity.x, moveInput.y*climbSpeed);
                myrigidbody2D.gravityScale = 0;
            }
            _animator.SetBool("isClimbing", Mathf.Abs(moveInput.y) > Mathf.Epsilon);
        }
        else
        {
            _animator.SetBool("isClimbing", false);
            startedClimbing = false;
            myrigidbody2D.gravityScale = gravity;
        }
    }

    void FLipSprite()
    {
        if (Mathf.Abs(moveInput.x) > Mathf.Epsilon)
        {
            transform.localScale = new Vector2(Mathf.Sign(moveInput.x), 1);
        }
    }

    void OnMove(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        moveInput = value.Get<Vector2>();
        Debug.Log(moveInput);
    }

    void OnJump(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        if (value.isPressed && myFeetCollider.IsTouchingLayers(LayerMask.GetMask("Ground")))
        {
            myrigidbody2D.velocity += new Vector2(0,jumpSpeed);
        }
    }

    void OnFire(InputValue value)
    {
        if (!isAlive)
        {
            return;
        }
        Instantiate(bullet,gun.position,transform.rotation);
    }

    void Run()
    {
        Vector2 playerVelocity = new Vector2(moveInput.x*playerSpeed,myrigidbody2D.velocity.y);
        myrigidbody2D.velocity = playerVelocity;
        _animator.SetBool("isRunning", Mathf.Abs(moveInput.x) > Mathf.Epsilon);
        
    }
}
