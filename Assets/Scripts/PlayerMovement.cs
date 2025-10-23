using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public Vector2 dir;
    public Rigidbody2D rb;
    public float speed;
    public Transform groundPivot;
    public float radius;
    public LayerMask layer;
    public float jumpForce;
    public SpriteRenderer sr;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
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
            if (Physics2D.OverlapCircle(groundPivot.position, radius, layer))
            {
                dir.y = 0;
                rb.AddForce(Vector2.up * jumpForce );
            }
        }

        dir.x = Input.GetAxisRaw("Horizontal") * speed;
        dir.y = rb.linearVelocity.y;

        // É pra virar o sprite, mas por enquanto é só um quadrado

        //if (dir.x > 0) sr.flipX = true;
        //if (dir.x < 0) sr.flipX = true;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            dir.y = 0;
            rb.AddForce(Vector2.up * (jumpForce));
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = dir;
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(groundPivot.position, radius);
    }
}
