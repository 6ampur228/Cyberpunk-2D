using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMover : MonoBehaviour
{
    [Header("Components")]
    [SerializeField] private LayerMask _whatIsWall;
    [SerializeField] private LayerMask _whatIsGround;
    [SerializeField] private Transform _groundChecker;
    [SerializeField] private Transform _wallChecker;
    [SerializeField] private Transform _playerTransform;

    [Header ("HorizontallMove")]
    [SerializeField] private float _deltaSpeed;
    [SerializeField] private float _maxSpeed;

    [Header ("Jump")]
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _groundCheckerRadius;

    [Header("JumpWall")]
    [SerializeField] private float _wallSlideSpeed;
    [SerializeField] private float _wallJumpForceX;
    [SerializeField] private float _wallJumpForceY;

    [Header ("HangingOnWall")]
    [SerializeField] private float _wallCheckerRadius;

    private Rigidbody2D _player;

    private float _currentSpeed = 0;
    private bool _isWallSliding = false;

    public event Action<float> CurrentSpeedChanged;

    public bool OnGround { get; private set; }
    public bool OnWall { get; private set; }

    private void Awake()
    {
        _player = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        OnGround = Physics2D.OverlapCircle(_groundChecker.position, _groundCheckerRadius, _whatIsGround);
        OnWall = Physics2D.OverlapCircle(_wallChecker.position, _wallCheckerRadius, _whatIsWall);

        if(OnWall && OnGround == false)
        {
            _isWallSliding = true;
            _player.velocity = new Vector2(_player.velocity.x, -_wallSlideSpeed);
        }
        else
        {
            _isWallSliding = false;
        }
    }

    public void MoveHorizontally(float horizontalMove)
    {
        ChangeSpeed(horizontalMove);

        if (horizontalMove == 0)
            return;

        Vector2 movement = new Vector2(horizontalMove, 0f);

        _player.position += movement * _currentSpeed * Time.deltaTime;
    }

    public bool TryJump()
    {
        if (OnGround)
        {           
            _player.velocity = new Vector2(_player.velocity.x, _jumpForce);

            return true;
        }

        return false;
    }

    public bool TryJumpWall()
    {
        if (_isWallSliding)
        {
            float angle = Quaternion.Angle(transform.rotation, Quaternion.Euler(0, 180, 0));

            if (Mathf.Approximately(angle, 0))
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
                _player.velocity = new Vector2(_wallJumpForceX, _wallJumpForceY);
            }
            else if(Mathf.Approximately(angle, 180))
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
                _player.velocity = new Vector2(-_wallJumpForceX, _wallJumpForceY);
            }

            return true;
        }

        return false;
    }

    private void ChangeSpeed(float horizontalMove)
    {
        if (horizontalMove != 0)
        {
            _currentSpeed += _deltaSpeed;
            _currentSpeed = Mathf.Clamp(_currentSpeed, 0, _maxSpeed);
        }
        else 
        {
            _currentSpeed = 0;
        }

        CurrentSpeedChanged?.Invoke(_currentSpeed);
    }
}