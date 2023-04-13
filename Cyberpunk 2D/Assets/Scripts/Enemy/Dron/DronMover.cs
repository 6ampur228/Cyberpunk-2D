using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DronMover : MonoBehaviour
{
    [SerializeField] private Detector _detector;

    [SerializeField] private float _distanceBetweenTarget;
    [SerializeField] private float _speed;

    private Rigidbody2D _dron;
    private Player _target;

    private Vector2 _movement;
    private float _distanceToPlayer;

    private void Start()
    {
        _dron = GetComponent<Rigidbody2D>();
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
        if (_target != null)
        {
            _distanceToPlayer = Vector2.Distance(_target.transform.position, transform.position);

            if(_distanceToPlayer > _distanceBetweenTarget)
            {
                Vector3 direction = (_target.transform.position - transform.position).normalized * _distanceBetweenTarget;
                Vector3 targetPosition = _target.transform.position - direction;

                _movement = targetPosition - transform.position;
                _movement.Normalize();

                transform.LookAt(_target.transform.position);
                transform.Rotate(0, -90, 0); 
            }
            
            //float angle = Mathf.Atan2(_movement.y, _movement.x) * Mathf.Rad2Deg;

            //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void FixedUpdate()
    {
        if(_distanceToPlayer > _distanceBetweenTarget)
            _dron.MovePosition(_dron.position + _movement * _speed * Time.fixedDeltaTime);
    }

    private void OnDetected(Player target)
    {
        _target = target;
    }
}
