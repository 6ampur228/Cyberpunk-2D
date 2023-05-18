using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : MonoBehaviour
{
    [SerializeField] private float _health;

    private AudioSource _dieSound;

    public Player Target { get; private set; }

    protected virtual void TakeDamage(float damage)
    {
        if (damage < 0)
            throw new ArgumentException("Damage value cannot be negative");

        _health -= damage;

        if (_health <= 0)
        {
            Die();
        }
    }

    protected virtual void Die()
    {
        _dieSound.Play();
        Destroy(gameObject);
    }

    public void SetDieSound(AudioSource dieSound)
    {
        _dieSound = dieSound;
    }

    public void SetTarget(Player target)
    {
        Target = target;
    }
}
