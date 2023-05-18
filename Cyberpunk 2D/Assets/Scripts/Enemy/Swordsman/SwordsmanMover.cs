using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Swordsman))]
public class SwordsmanMover : MonoBehaviour
{
    private const string IsRunning = "IsRunning";

    [SerializeField] private Detector _detector;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _distanceBetweenTarget;
    [SerializeField] private float _speed;

    private Swordsman _swordsman;

    private bool _canMove;

    public bool NeedAttack { get; private set; }

    private void Start()
    {
        _swordsman = GetComponent<Swordsman>();
        _canMove = false;
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
        if (_canMove)
        {
            Vector3 direction = _swordsman.Target.transform.position - transform.position;

            direction.y = 0f; 

            float distance = direction.magnitude;
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0f, angle - 180f, 0);

            if (distance > _distanceBetweenTarget)
            {
                NeedAttack = false;

                Vector3 movement = direction.normalized * _speed * Time.deltaTime;

                transform.Translate(movement, Space.World);
            }
            else 
            {
                NeedAttack = true;
            }
        }
    }

    private void OnDetected()
    {
        _canMove = true;
        _animator.SetBool(IsRunning, true);
    }
}
