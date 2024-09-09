using System;
using UnityEngine;

public class BaseSpall : MonoBehaviour
{
    private const float LifeTime = 3f;
    private void Awake()
    {
        Destroy(gameObject,LifeTime);
    }

    private void OnCollisionEnter(Collision other)
    {
        Destroy(other.gameObject);
        Destroy(gameObject);
    }
}
