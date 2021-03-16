using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Animator))]
public class Movement : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    [SerializeField] private float _arrivalDistance;
    [SerializeField] private GameObject _pathContainer;

    private Rigidbody2D _rigidbody2D;
    private Animator _animator;
    private Waypoint[] _waypoints;
    private Queue<Waypoint> _path = new Queue<Waypoint>();
    private Waypoint _currentWaypoint;
    private bool _isFacingRight = true;

    private void Awake()
    {
        _waypoints = _pathContainer.GetComponentsInChildren<Waypoint>();

        foreach (Waypoint currentWaypoint in _waypoints)
        {
            _path.Enqueue(currentWaypoint);
        }

        _currentWaypoint = _path.Dequeue();
    }

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
    }


    private void Update()
    {
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
    }

    private void FixedUpdate()
    {
        MoveToWaypoint();
    }

    private void MoveToWaypoint()
    {
        float distanceToWaypoint = _currentWaypoint.Position - transform.position.x;
        float direction = Mathf.Sign(distanceToWaypoint);
        if (Mathf.Abs(distanceToWaypoint) <= _arrivalDistance)
        {
            _rigidbody2D.MovePosition(new Vector2(_currentWaypoint.Position, transform.position.y));
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            ChangeWaypoint();
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(direction * _movementSpeed, _rigidbody2D.velocity.y);
            ChangeDirection();
        }
    }

    private void ChangeDirection()
    {
        if (_rigidbody2D.velocity.x > 0 && !_isFacingRight || _rigidbody2D.velocity.x < 0 && _isFacingRight)
        {
            RotateSprite();
        }
    }

    private void RotateSprite()
    {
        transform.Rotate(new Vector2(0f, 180f));
        _isFacingRight = !_isFacingRight;
    }

    private void ChangeWaypoint()
    {
        if (_path.Count > 0)
        {
            _movementSpeed = _currentWaypoint.CurrentSpeed;
            _currentWaypoint = _path.Dequeue();
        }
    }
}
