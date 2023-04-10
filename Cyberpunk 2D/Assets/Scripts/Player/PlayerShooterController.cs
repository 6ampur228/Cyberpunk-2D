using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerShooter))]
public class PlayerShooterController : MonoBehaviour
{
    private PlayerInput _playerInput;
    private PlayerShooter _playerShooter;

    public event Action PlayerShooted;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerShooter = GetComponent<PlayerShooter>();

        _playerInput.Player.Shoot.performed += context => OnShoot();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnShoot()
    {
        if (_playerShooter.TryShoot())
            PlayerShooted?.Invoke();
    }
}
