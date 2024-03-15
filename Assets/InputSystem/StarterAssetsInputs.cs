using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class StarterAssetsInputs : MonoBehaviour
{

    [Header("Character Input Values")]
    public Vector2 move;
    public Vector2 look;
    public bool jump;
    public bool sprint;

    [Header("Mouse Cursor Settings")]
    public bool cursorLocked = true;
    public bool cursorInputForLook = true;
    public bool altKeyPressed = false;
    public bool isaltkey = false;//alt키 비활성화

    //인스턴스
    TestJumpManager tjm;
    public static StarterAssetsInputs instance;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        tjm = TestJumpManager.instance;
    }
    public void OnMove(InputValue value)
    {
        MoveInput(value.Get<Vector2>());
    }

    public void OnLook(InputValue value)
    {
        if (cursorInputForLook && !altKeyPressed)
        {
            LookInput(value.Get<Vector2>());
        }
    }

    public void OnJump(InputValue value)
    {
        tjm.JumpStart(); //점프메니저를 호출
        //JumpInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnAltKey(InputValue value)
    {//누르고 있을 때 커서보임 (화면 회전 가능)
        altKeyPressed = value.isPressed;

        if (altKeyPressed && !isaltkey)
        {
            // 커서 보이기
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!altKeyPressed)
        {
            // 커서 숨기기
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    public void MoveInput(Vector2 newMoveDirection)
    {
        move = newMoveDirection;
    }

    public void LookInput(Vector2 newLookDirection)
    {
        look = newLookDirection;
    }

    public void SprintInput(bool newSprintState)
    {
        sprint = newSprintState;
    }

    public void OnApplicationFocus(bool hasFocus)
    {
        SetCursorState(cursorLocked);
    }

    public void SetCursorState(bool newState)
    {
        Cursor.lockState = newState ? CursorLockMode.Locked : CursorLockMode.None;
    }
    public void UpdateCursorAndLookState(bool enable)
    {
        //다른 inputsystem스크립트에서 참조할 수 있음
        //기능 : 커서가 보이게 되고, 카메라 움직임을 막을 수 있음
        if (enable)
        {//커서가 보이고 화면 락
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            cursorInputForLook = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            cursorInputForLook = true;
        }
    }
}
