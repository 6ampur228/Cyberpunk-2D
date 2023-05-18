using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordsmanAttacker : MonoBehaviour
{
    private const string IsAttacking = "IsAttacking";

    [SerializeField] private Swordsman _swordsman;
    [SerializeField] private SwordsmanMover _swordsmanMover;
    [SerializeField] private Animator _animator;

    [SerializeField] private float _timeBetweenAttack;

    private float _timeAfterAttack;

    private void Start()
    {
        _timeAfterAttack = _timeBetweenAttack;
    }

    private void Update()
    {
        if(_timeAfterAttack >= _timeBetweenAttack)
        {
            if (_swordsmanMover.NeedAttack)
            {
                _animator.SetBool(IsAttacking, true);
                _swordsman.Target.GetComponent<Player>().TakeDamage(_swordsman.Damage);
            }
            else
            {
                _animator.SetBool(IsAttacking, false);
            }

            _timeAfterAttack = 0;
        }

        _timeAfterAttack += Time.deltaTime;
    }
}
