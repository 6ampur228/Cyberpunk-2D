using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
[RequireComponent(typeof(PlayerMoverController))]
[RequireComponent(typeof(PlayerShooterController))]
public class PlayerAudio : MonoBehaviour
{
    [Header ("Run")]
    [SerializeField] private AudioSource _runSound;
    [SerializeField] private float _speedForSoundRun;

    [Header ("Jump")]
    [SerializeField] private AudioSource _jumpSound;

    [Header ("Shoot")]
    [SerializeField] private AudioSource _shootSound;

    [Header("Hurt")]
    [SerializeField] private AudioSource _hurtSound;

    private Player _player;
    private PlayerMoverController _playerMoverController;
    private PlayerShooterController _playerShooterController;

    private void Awake()
    {
        _player = GetComponent<Player>();
        _playerMoverController = GetComponent<PlayerMoverController>();
        _playerShooterController = GetComponent<PlayerShooterController>();
    }

    private void OnEnable()
    {
        _player.Hurted += OnHurted;
        _playerShooterController.PlayerShooted += OnPlayerShooted;
        _playerMoverController.PlayerCurrentSpeedChanged += OnPlayerCurrentSpeedChanged;
        _playerMoverController.PlayerJumped += OnPlayerJumped;
    }

    private void OnDisable()
    {
        _player.Hurted -= OnHurted;
        _playerShooterController.PlayerShooted -= OnPlayerShooted;
        _playerMoverController.PlayerCurrentSpeedChanged -= OnPlayerCurrentSpeedChanged;
        _playerMoverController.PlayerJumped -= OnPlayerJumped;
    }

    private void OnHurted()
    {
        _hurtSound.Play();
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
