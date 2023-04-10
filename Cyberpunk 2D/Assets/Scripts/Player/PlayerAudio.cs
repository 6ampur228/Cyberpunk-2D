using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoverController))]
[RequireComponent(typeof(PlayerShooterController))]
public class PlayerAudio : MonoBehaviour
{
    [Header ("Components")]
    private PlayerMoverController _playerMoverController;
    private PlayerShooterController _playerShooterController;

    [Header ("Run")]
    [SerializeField] private AudioSource _runSound;
    [SerializeField] private float _speedForSoundRun;

    [Header ("Jump")]
    [SerializeField] private AudioSource _jumpSound;

    [Header ("Shoot")]
    [SerializeField] private AudioSource _shootSound;

    private void Awake()
    {
        _playerMoverController = GetComponent<PlayerMoverController>();
        _playerShooterController = GetComponent<PlayerShooterController>();
    }

    private void OnEnable()
    {
        _playerShooterController.PlayerShooted += OnPlayerShooted;
        _playerMoverController.PlayerCurrentSpeedChanged += OnPlayerCurrentSpeedChanged;
        _playerMoverController.PlayerJumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        _playerShooterController.PlayerShooted -= OnPlayerShooted;
        _playerMoverController.PlayerCurrentSpeedChanged -= OnPlayerCurrentSpeedChanged;
        _playerMoverController.PlayerJumped -= OnPlayerJumped;
    }

    private void OnPlayerCurrentSpeedChanged(float changedSpeed)
    {
        if(changedSpeed > _speedForSoundRun && _playerMoverController.PlayerOnGround)
        {
            if(_runSound.isPlaying == false)
                _runSound.Play();
            
        }
        else 
        {
            if (_runSound.isPlaying)
                _runSound.Stop();           
        }
    }

    private void OnPlayerJumped()
    {
        _jumpSound.Play();
    }

    private void OnPlayerShooted()
    {
        _shootSound.Play();
    }
}
