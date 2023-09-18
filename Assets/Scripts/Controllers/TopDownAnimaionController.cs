using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TopDownAnimaionController : TopDownAnimations
{
    // 해싱하는 이유? 키 값을 스트링으로 제공하면, 스트링을 비교하는 연산이 일어난다. 문자열을 비교하는 연산은 비용이 높다.
    private static readonly int IsWalking = Animator.StringToHash("IsWalking");
    private static readonly int Attack = Animator.StringToHash("Attack");
    private static readonly int IsHit = Animator.StringToHash("IsHit");

    private HealthSystem _healthSystem;
    protected override void Awake()
    {
        base.Awake();
        _healthSystem = GetComponent<HealthSystem>();
    }

    void Start()
    {
        controller.OnAttackEvent += Attacking;
        controller.OnMoveEvent += Move;

        if (_healthSystem != null)
        {
            _healthSystem.OnDamage += Hit;
            _healthSystem.OnInvincibilityEnd += InvincibilityEnd;
        }
    }

    private void Move(Vector2 obj)
    {
        animator.SetBool(IsWalking, obj.magnitude > .5f);
    }

    private void Attacking(AttackSO sO)
    {
        animator.SetTrigger(Attack);
    }

    private void Hit()
    {
        animator.SetBool(IsHit, true);
    }

    private void InvincibilityEnd()
    {
        animator.SetBool(IsHit, false);
    }
}
