using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class AnimationChange : MonoBehaviour
{
    private Animator _animator;
    private Rigidbody2D _rigidbody2D;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }
}
