using UnityEngine;

public class AnimationPlayer : MonoBehaviour
{
    private Animator _anim;

    [SerializeField] private PlayerController controller;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        _anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        _anim.SetBool("IsRun",controller.GetIsRun());
    }
}
