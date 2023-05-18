using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dron : Enemy
{
    [SerializeField] private ParticleSystem _explosionTemplate;

    private void OnDestroy()
    {
        Instantiate(_explosionTemplate, transform.position, Quaternion.identity);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerBullet bullet))
        {
            TakeDamage(bullet.Damage);
        }
    }
}
