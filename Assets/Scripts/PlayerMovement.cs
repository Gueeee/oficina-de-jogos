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
    
    // Reset na 'Scene'
    void ResetScene() {
        SceneManager.LoadScene(1);
    }
    // Update is called once per frame
    void Update() {
        // Definindo botões
        var inputX = Input.GetAxisRaw("Horizontal");
        var jumpInput = Input.GetButtonDown("Jump");
        var dashInput = Input.GetButtonDown("Dash");
        var resetInput = Input.GetKeyDown(KeyCode.R);
        
        // Define que X e Y são modificados ao apertar os botões
        dir.x = inputX * speed;
        dir.y = rb.linearVelocity.y;
        
        // Reinicia a fase se apertar o botão
        if(resetInput) ResetScene();
        
        // Pulo
        if (jumpInput && canJump) {
            dir.y = 0;
            canJump = false;
            rb.AddForce(Vector2.up * (jumpForce));
        }
        
        // Dash
        if (dashInput && canDash) {
            // À fazer
        }
        
        // Animações
        if(dir.x != 0) { // Se está andando
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        
        // Pulo
        animator.SetBool("isJumping", !canJump);
        // Dash
        // animator.SetBool("isDashing", _isDashing);
        
        // Vira o sprite
        if (dir.x > 0) sr.flipX = false;
        else if (dir.x < 0) sr.flipX = true;
    }
    
    // Tratando evento das colisões
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