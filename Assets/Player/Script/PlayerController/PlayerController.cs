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
        _move = PlayerInput.Instance.GetDirection();
        if (_move == Vector2.zero) return;

        Vector3 moveDir = new Vector3(_move.x, 0f, _move.y); // Нормализация направления движения
        _body.AddForce(moveDir * (speed * Time.fixedDeltaTime), ForceMode.Acceleration);
        
        var velocity = _body.linearVelocity;
        Quaternion rotate = Quaternion.LookRotation(velocity, Vector3.up);
        _body.rotation = Quaternion.Slerp(_body.rotation, rotate, rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        isRun = _body.linearVelocity != Vector3.zero;
    }

    public bool IsRun() => isRun;

    
}