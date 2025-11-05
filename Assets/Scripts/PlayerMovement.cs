using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 dir;
    public Rigidbody2D rb;
    public float speed;
    [SerializeField] private Animator animator;
    public float jumpForce;
    public bool canJump;
    public bool canDash;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canJump = true;
    }

    // Update is called once per frame
    void Update() {

        if(Input.GetKeyDown(KeyCode.R))
        {
            ResetScene();
        }

        void ResetScene()
        {
            SceneManager.LoadScene(1);
        }

        if (Input.GetButtonDown("Jump")) {
            if (canJump == true)
            {
                dir.y = 0;
                rb.AddForce(Vector2.up * (jumpForce));
                canJump = false;
            }
        }

        dir.x = Input.GetAxisRaw("Horizontal") * speed;
        dir.y = rb.linearVelocity.y;
        
        // Animações
        if(dir.x != 0) {
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        
        // Virar o sprite
        if (dir.x > 0) sr.flipX = false;
        else if (dir.x < 0) sr.flipX = true;

        
        animator.SetBool("isJumping", !canJump);
    }
    
    void ResetScene()
    {
        SceneManager.LoadScene(1);
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        switch (other.gameObject.tag)
        {
            case "Solid":
                canJump = true;
                canDash = true;
                
                break;
            case "Enemy":
                ResetScene();
                
                break;
            case "Respawn":
                ResetScene();
                
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        switch (other.gameObject.tag) {
            case "Enemy":
                dir.y = 0;
                rb.linearVelocity = Vector2.zero;
                rb.AddForce(Vector2.up * (jumpForce));
                canJump = false; 
                break;
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = dir;
    }
}