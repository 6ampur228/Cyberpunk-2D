using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerMoverController))]
[RequireComponent(typeof(PlayerShooterController))]
public class PlayerAnimationsSwitcher : MonoBehaviour
{
    private const string Speed = "Speed";
    private const string IsJumping = "IsJumping";
    private const string IsShooting = "IsShooting";
    private const string OnWall = "OnWall";

    private Animator _animator;
    private PlayerMoverController _playerMoverController;
    private PlayerShooterController _playerShooterController;
    private Coroutine _currentCoroutine;

    private float _delayTime = 0.2f;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerMoverController = GetComponent<PlayerMoverController>();
        _playerShooterController = GetComponent<PlayerShooterController>();
    }

    private void OnEnable()
    {
        _playerMoverController.PlayerCurrentSpeedChanged += OnPlayerCurrentSpeedChanged;
        _playerShooterController.PlayerShooted += OnPlayerShooted;
    }

    private void OnDisable()
    {
        _playerMoverController.PlayerCurrentSpeedChanged -= OnPlayerCurrentSpeedChanged;
        _playerShooterController.PlayerShooted -= OnPlayerShooted;
    }

    private void Update()
    {
        TryTurnJumpAnimation();
        TryTurnHangingOnWallAnimation();
    }

    private void OnPlayerCurrentSpeedChanged(float currentSpeed)
    {
        _animator.SetFloat(Speed, currentSpeed);
    }

    private void OnPlayerShooted()
    {
        _animator.SetBool(IsShooting, true);

        if (_currentCoroutine != null)
            StopCoroutine(_currentCoroutine);

        StartCoroutine(DisableShootAnimation(_delayTime));
    }

    private void TryTurnJumpAnimation()
    {
        if (_playerMoverController.PlayerOnGround)
        {
            _animator.SetBool(IsJumping, false);
        }
        else
        {
            _animator.SetBool(IsJumping, true);
        }
    }

    private void TryTurnHangingOnWallAnimation()
    {
        if (_playerMoverController.PlayerOnWall)
        {
            _animator.SetBool(OnWall, true);
        }
        else
        {
            _animator.SetBool(OnWall, false);
        }
    }

    private IEnumerator DisableShootAnimation(float timeDelay)
    {
        yield return new WaitForSeconds(timeDelay);

        _animator.SetBool(IsShooting, false);
    }
}
