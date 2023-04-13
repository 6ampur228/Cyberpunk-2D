using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMoverController))]
public class PlayerShooter : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private PlayerBullet[] _templates;

    [SerializeField] private float _shootDelay;

    private PlayerMoverController _playerMoverController;

    private float _timeAfterShoot;

    private void Start()
    {
        _timeAfterShoot = _shootDelay;
        _playerMoverController = GetComponent<PlayerMoverController>();
    }

    private void Update()
    {
        _timeAfterShoot += Time.deltaTime;
    }

    public bool TryShoot()
    {
        if (_timeAfterShoot >= _shootDelay && _playerMoverController.PlayerOnWall == false) 
        {
            int templateIndex = Random.Range(0, _templates.Length);

            Instantiate(_templates[templateIndex], _shootPoint.position, _shootPoint.rotation);

            _timeAfterShoot = 0;

            return true;
        }

        return false;
    }
}
