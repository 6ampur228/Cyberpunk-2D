using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    private float _currentHealth;

    protected virtual void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage value cannot be negative");

        _currentHealth -= damage;
    }

    protected void TryDie()
    {
        if(_currentHealth <= 0)
        {
            Destroy(gameObject);
        }
    }
}
