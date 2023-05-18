using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Detector : MonoBehaviour
{
    [SerializeField] private AudioSource _detectorSound;

    [SerializeField] private Color _colorDetectorAction;
    [SerializeField] private float _timeForDestruction;
     
    private SpriteRenderer _spriteRenderer;
    private bool _isAlreadyWorked = false;

    public event Action Detected;

    private void Start()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(_isAlreadyWorked == false)
        {
            if (collision.TryGetComponent(out Player player))
            {
                _spriteRenderer.color = _colorDetectorAction;
                _detectorSound.Play();

                Detected?.Invoke();

                Destroy(gameObject, _timeForDestruction);

                _isAlreadyWorked = true;
            }
        }        
    }
}
