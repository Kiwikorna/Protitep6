using UnityEngine;

public class HundleCamra : MonoBehaviour
{
    [SerializeField] private Transform target;
     [SerializeField] private float smooth = 5.0f;
    [SerializeField] private Vector3 offset;
    private Vector3 targetPos;
    private Vector3 velocity = Vector3.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        MoveCamera();   
    }

    public void MoveCamera()
    {
        targetPos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref velocity, smooth);
    }
}
