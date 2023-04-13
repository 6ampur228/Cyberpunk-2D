using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SlowMotion))]
public class SlowMotionController : MonoBehaviour
{
    private SlowMotion _slowMotion;
    private PlayerInput _playerInput;

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _slowMotion = GetComponent<SlowMotion>();
    }

    private void OnEnable()
    {
        _playerInput.Enable();
        _playerInput.Player.SlowMotion.performed += context => OnSlowMotion();
    }

    private void OnDisable()
    {
        _playerInput.Disable();
    }

    private void OnSlowMotion()
    {
        _slowMotion.Play();
    }
}
