using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// public class PlyaerInputController : MonoBehaviour

// Input Controller는 TopDownCharacterController를 상속받기 때문이다!
public class PlayerInputController : TopDownCharacterController
{
    private Camera _camera;
    
    protected override void Awake()
    {
        base.Awake();
        _camera = Camera.main;
    }


    // 샌드메시지
    public void OnMove(InputValue value)
    {
        //Debug.Log("OnMove" + value.ToString());
        // noremalized x+y -> 피타고라스, 없으면 직선은 느린데 대각선은 빠름. 
        Vector2 moveInput = value.Get<Vector2>().normalized;
        // 옵저버, 구독
        CallMoveEvent(moveInput);
    }

    public void OnLook(InputValue value)
    {
        // Debug.Log("OnLook" + value.ToString());
        // UI상의 좌표는 좌상단. 월드 좌표는 중심으로부터의 좌표. 즉, Screen좌표를 월드 좌표로 변환해준다.
        Vector2 newAim = value.Get<Vector2>();
        Vector2 worldPos = _camera.ScreenToWorldPoint(newAim);
        // 나에게서 마우스 포인터가 바라보는 방향을 알려준다.
        newAim = (worldPos- (Vector2)transform.position).normalized;

        if(newAim.magnitude >= .9f)
        {
            CallLookEvent(newAim);
        }
    }

    public void OnFire(InputValue value)
    {
        //Debug.Log("OnFire" + value.ToString());

        // 키가 눌리고 있는지, true -> HandleAttackDelay로 넘어감. 
        IsAttacking = value.isPressed;
    }
}
