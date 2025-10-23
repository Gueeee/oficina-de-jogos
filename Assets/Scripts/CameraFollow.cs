using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    private Vector3 offset = new Vector3(0f, 0f, -10f);
    private float smoothTime =  0.25f;
    private Vector3 velo = Vector3.zero;
    
    [SerializeField] private Transform target;
        

    // Update is called once per frame
    void Update()
    {
        Vector3 TargetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, TargetPosition, ref velo, smoothTime);
    }
}
