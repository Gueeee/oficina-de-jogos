using UnityEngine;

public class MoedaBehaviour : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player") {
            Destroy(this.gameObject);
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
