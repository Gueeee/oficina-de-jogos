using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyBehaviour : MonoBehaviour
{
    bool movingDirection = false;
    [SerializeField] float speed;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        transform.Translate(((movingDirection ? -1 : 1) * Vector3.left * Time.deltaTime * speed));
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            ResetScene();
        }

        void ResetScene()
        {
            SceneManager.LoadScene(1);
        }

        if (other.gameObject.tag == "Edge" || other.gameObject.tag == "Enemy")
        {
            movingDirection = !movingDirection;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}
