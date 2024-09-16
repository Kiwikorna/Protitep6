using System;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Vector2 _move;
    private Rigidbody _body;

    private bool isRun = false;
    [SerializeField] private float speed = 7.0f; // Скорость в метрах в секунду
    [SerializeField] private float rotationSpeed = 10.0f;


    private void Awake()
    {
        _body = GetComponent<Rigidbody>();
        if (_body != null)
        {
            _body.freezeRotation = true;
        }
        else
        {
            Debug.LogError("Rigidbody component not found on this game object.");
        }
    }


    private void FixedUpdate()
    {
        HandlePlayer();
    }
 
    private void HandlePlayer()
    {
        _move = Controller.Instance.GetDirection();
        Vector3 moveDir = new Vector3(_move.x, 0f, _move.y); // Нормализация направления движения

        float playerRadius = 0.5f;
        float playerHeight = 2f;
        float distance = speed * Time.deltaTime;
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDir, distance);

        if (!canMove)
        {
            // Проверка движения только по оси X
            Vector3 dirX = new Vector3(moveDir.x, 0f, 0f);
            canMove = (moveDir.x <-.5f || moveDir.x > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirX, distance);
            if (canMove)
            {
                moveDir = dirX;
            }
            else
            {
                // Проверка движения только по оси Z
                Vector3 dirZ = new Vector3(0f, 0f, moveDir.z);
                canMove = (dirZ.z <-.5f || dirZ.z > +.5f) && !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, dirZ, distance);
                if (canMove)
                {
                    moveDir = dirZ;
                }
                else
                {
                    
                }
            }
            
           
            
        }

        if (canMove)
        {
            _body.linearVelocity = new Vector3(moveDir.x, _body.linearVelocity.y, moveDir.z) * speed;
            if (moveDir != Vector3.zero)
            {
                Quaternion rotate = Quaternion.LookRotation(moveDir, Vector3.up);
                _body.rotation = Quaternion.Slerp(_body.rotation, rotate, rotationSpeed * Time.deltaTime);
            }
        }
        else
        {
            _body.linearVelocity = new Vector3(0, _body.linearVelocity.y, 0);
            

        }

        isRun = _body.linearVelocity != Vector3.zero;
    }

    public bool GetIsRun() => isRun;
    public void SetSpeed(float value)
    {
         speed = value;
    }
}
