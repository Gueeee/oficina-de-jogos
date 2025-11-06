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
    
    [Header("Movimentação")]
    public float speed;
    public float jumpForce;
    public bool canJump;

    [Header("Dash")]
    public float dashSpeed = 20f;
    public float dashDuration = 0.1f;
    public float dashCooldown = 0.1f;
    public bool canDash = true;
    public bool isDashing;
    TrailRenderer trailRenderer;
    
    [Header("Animação")]
    [SerializeField] private Animator animator;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        canJump = true;
        canDash = true;
    }
    
    // Reset na 'Scene'
    void ResetScene() {
        SceneManager.LoadScene(1);
    }

    public void Dash()
    {
        StartCoroutine(DashCoroutine());
    }

    private IEnumerator DashCoroutine() 
    {
        canDash = false;
        isDashing = true;
        //trailRenderer.emitting = true;
        
        float dashDirection = sr.flipX ? -1f : 1f;
        
        rb.AddForce(new Vector2(dashDirection * dashSpeed, rb.linearVelocity.y)); // O dash em si
        
        yield return new WaitForSeconds(dashDuration);
        
        rb.linearVelocity = new Vector2(0f, rb.linearVelocity.y); // reseta a velocidade horizontal
        
        isDashing = false;
        //trailRenderer.emitting = false;

        yield return new WaitForSeconds(dashCooldown);
        
        canDash = true;
    }
    
    // Update is called once per frame
    void Update() {
        // Animações
        if(dir.x != 0) { // Se está andando
            animator.SetBool("isRunning", true);
        } else {
            animator.SetBool("isRunning", false);
        }
        
        // Pulo
        animator.SetBool("isJumping", !canJump);
        // Dash
        animator.SetBool("isDashing", isDashing);
        
        // Vira o sprite
        if (dir.x > 0) sr.flipX = false;
        else if (dir.x < 0) sr.flipX = true;
        
        if (isDashing)
        {
            return;
        }
        
        // Definindo botões
        var inputX = Input.GetAxisRaw("Horizontal");
        var inputY = Input.GetAxisRaw("Vertical");
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
        if (dashInput && canDash)
        {
            //Dash();
        }
        
        if (isDashing) {
        }
        
    }
    
    // Tratando evento das colisões
    private void OnCollisionEnter2D(Collision2D other) {
        switch (other.gameObject.tag) {
            case "Solid":
                canJump = true;
                canDash = true;
                isDashing = false;
                
                break;
            case "Finish":
                SceneManager.LoadScene(2);
                
                break;
            case "Enemy":
                ResetScene();
                
                break;
            case "Respawn":
                ResetScene();
                
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
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