using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMover))]
public class PlayerMoverController : MonoBehaviour
{
    private PlayerMover _playerMover;
    private PlayerInput _playerInput;

    public event Action<float> PlayerCurrentSpeedChanged;
    public event Action PlayerJumped;

    public bool PlayerOnGround => _playerMover.OnGround;
    public bool PlayerOnWall => _playerMover.OnWall;

    private void Awake()
    {
        _playerInput = new PlayerInput();       
        _playerMover = GetComponent<PlayerMover>();

        _playerInput.Player.Jump.performed += context => OnJump();
        _playerInput.Player.Jump.performed += context => OnJumpWall();
    }
    private void OnEnable()
    {
        _playerInput.Enable();

        _playerMover.CurrentSpeedChanged += OnCurrentSpeedChanged;
    }

    private void OnDisable()
    {
        _playerInput.Disable();

        _playerMover.CurrentSpeedChanged -= OnCurrentSpeedChanged;
    }

    private void FixedUpdate()
    {
        float horizontalMove = _playerInput.Player.MoveHorizontally.ReadValue<float>();

        ChangeRotation(horizontalMove);

        _playerMover.MoveHorizontally(horizontalMove);
    }

    private void ChangeRotation(float horizontalMove)
    {
        if (horizontalMove < 0)
            transform.rotation = Quaternion.Euler(0, 180, 0);       
        else if (horizontalMove > 0)
            transform.rotation = Quaternion.Euler(0, 0, 0); ;       
    }

    private void OnJump()
    {
        if (_playerMover.TryJump())
            PlayerJumped?.Invoke();
    }

    private void OnJumpWall()
    {
        if (_playerMover.TryJumpWall())
            PlayerJumped?.Invoke();
    }

    private void OnCurrentSpeedChanged(float currentSpeed)
    {
        PlayerCurrentSpeedChanged?.Invoke(currentSpeed);
    }
}
