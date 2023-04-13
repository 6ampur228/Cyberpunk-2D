using System; 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _health;

    private float _currentHealth;

    public event Action Hurted;

    private void Start()
    {
        _currentHealth = _health;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out EnemyBullet bullet))
        {
            TakeDamage(bullet.Damage);
            TryDie();
        }
    }

    private void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage value cannot be negative");

        _currentHealth -= damage;

        Hurted?.Invoke();
    }

    private void TryDie()
    {
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
