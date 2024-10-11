using UnityEngine;
using UnityEngine.Serialization;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float smooth = 5.0f;
    [SerializeField] private Vector3 offset;
    private Vector3 _targetPosition;
    private Vector3 _velocityCamera = Vector3.zero;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        MoveCamera();   
    }

    public void MoveCamera()
    {
        _targetPosition = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, _targetPosition, ref _velocityCamera, smooth);
    }
}
