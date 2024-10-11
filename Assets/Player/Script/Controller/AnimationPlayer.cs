using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animator _animation;

    [SerializeField] private PlayerMovements controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _animation = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _animation.SetBool("IsRun",controller.IsRun());
    }
}
