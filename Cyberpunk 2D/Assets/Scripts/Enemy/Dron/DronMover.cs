using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Dron))]
public class DronMover : MonoBehaviour
{
    [SerializeField] private Detector _detector;

    [SerializeField] private float _distanceBetweenTarget;
    [SerializeField] private float _speed;

    private Dron _dron;
    private Rigidbody2D _dronRigidbody2D;

    private Vector2 _movement;
    private float _distanceToPlayer;
    private bool _canMove;

    private void Start()
    {
        _dron = GetComponent<Dron>();
        _dronRigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnEnable()
    {
        _detector.Detected += OnDetected;
    }

    private void OnDisable()
    {
        _detector.Detected -= OnDetected;
    }

    private void Update()
    {
        if (_dron.Target == null)
            return;

        if (_canMove)
        {
            _distanceToPlayer = Vector2.Distance(_dron.Target.transform.position, transform.position);

            if(_distanceToPlayer > _distanceBetweenTarget)
            {
                Vector3 direction = (_dron.Target.transform.position - transform.position).normalized * _distanceBetweenTarget;
                Vector3 targetPosition = _dron.Target.transform.position - direction;

                _movement = targetPosition - transform.position;
                _movement.Normalize();                
            }

            transform.LookAt(_dron.Target.transform.position);
            transform.Rotate(0, -90, 0);
        }
    }

    private void FixedUpdate()
    {
        if (_dron.Target == null)
            return;

        if (_distanceToPlayer > _distanceBetweenTarget)
            _dronRigidbody2D.MovePosition(_dronRigidbody2D.position + _movement * _speed * Time.fixedDeltaTime);
    }

    private void OnDetected()
    {
        _canMove = true;
    }
}
