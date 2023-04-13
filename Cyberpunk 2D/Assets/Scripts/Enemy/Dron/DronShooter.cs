using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronShooter : MonoBehaviour
{
    [SerializeField] private EnemyBullet _template;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Detector _detector;

    [SerializeField] private float _delayBetweenShoot;
    [SerializeField] private float _rotationSpeed;

    private bool _canShoot;

    private float _timeAfterShoot = 0;

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
        if(_canShoot == true)
        {
            _timeAfterShoot += Time.deltaTime;

            TryShoot();
        }
    }

    private void TryShoot()
    {
        if (_timeAfterShoot >= _delayBetweenShoot)
        {
            Instantiate(_template, _shootPoint.position, _shootPoint.rotation);

            _timeAfterShoot = 0;
        }
    }

    private void OnDetected(Player target)
    {
        _canShoot = true;

        Destroy(gameObject.GetComponent<Animator>());
    }
}
