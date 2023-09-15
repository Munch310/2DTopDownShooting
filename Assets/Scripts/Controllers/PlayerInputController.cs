using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// public class PlyaerInputController : MonoBehaviour

// Input Controller�� TopDownCharacterController�� ��ӹޱ� �����̴�!
public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;
    
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }


    // ����޽���
    public void OnMove(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        // noremalized x+y -> ��Ÿ���, ������ ������ ������ �밢���� ����. 
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // ������, ����
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        // Debug.Log("OnLook" + value.ToString());
        // UI���� ��ǥ�� �»��. ���� ��ǥ�� �߽����κ����� ��ǥ. ��, Screen��ǥ�� ���� ��ǥ�� ��ȯ���ش�.
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        // �����Լ� ���콺 �����Ͱ� �ٶ󺸴� ������ �˷��ش�.
        newAim = (worldPos- (Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        //Debug.Log("OnFire" + value.ToString());

        // Ű�� ������ �ִ���, true -> HandleAttackDelay�� �Ѿ. 
        IsAttacking = value.isPressed;
    }
}
