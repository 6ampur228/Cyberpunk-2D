using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordsman : Enemy
{
    [SerializeField] private Player _target;

    private float _damage = 1f;

    public float Damage => _damage;

    private void Start()
    {
        SetTarget(_target);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out PlayerBullet bullet))
        {
            TakeDamage(bullet.Damage);
        }
    }

    protected override void Die()
    {
        Destroy(gameObject);
    }
}
