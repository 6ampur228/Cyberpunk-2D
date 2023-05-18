using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrol : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    [SerializeField] private float _speed;

    private Vector3 _pointAPosition;
    private Vector3 _pointBPosition;
    private Vector3 _currentTarget;

    protected bool _canPatrol = true;

    private void Start()
    {
        _pointAPosition = _pointA.position;
        _pointBPosition = _pointB.position;

        _currentTarget = _pointBPosition;
    }

    private void Update()
    {
        if (_canPatrol)
        {
            if (transform.position == _pointAPosition)
            {
                _currentTarget = _pointBPosition;
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else if (transform.position == _pointBPosition)
            {
                _currentTarget = _pointAPosition;
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }

            transform.position = Vector3.MoveTowards(transform.position, _currentTarget, _speed * Time.deltaTime);
        }       
    }

    protected void ForbidPatrol()
    {
        _canPatrol = false;
    }
}