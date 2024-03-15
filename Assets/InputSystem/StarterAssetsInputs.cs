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
    public bool isaltkey = false;//altŰ ��Ȱ��ȭ

    //�ν��Ͻ�
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
        tjm.JumpStart(); //�����޴����� ȣ��
        //JumpInput(value.isPressed);
    }
    public void OnSprint(InputValue value)
    {
        SprintInput(value.isPressed);
    }

    public void OnAltKey(InputValue value)
    {//������ ���� �� Ŀ������ (ȭ�� ȸ�� ����)
        altKeyPressed = value.isPressed;

        if (altKeyPressed && !isaltkey)
        {
            // Ŀ�� ���̱�
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else if(!altKeyPressed)
        {
            // Ŀ�� �����
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
        //�ٸ� inputsystem��ũ��Ʈ���� ������ �� ����
        //��� : Ŀ���� ���̰� �ǰ�, ī�޶� �������� ���� �� ����
        if (enable)
        {//Ŀ���� ���̰� ȭ�� ��
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
