using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInputscript : MonoBehaviour
{

    [Header("NPC input")]
    public bool NPCLook; //f ������ npc�� ��ȭ ����
    public bool LeftMouseClick;//���� ���콺 Ŭ�� �� ��ȭ �ѱ��

    public void OnDialog(InputValue value)
    {
        DialogInput(value.isPressed);
    }
    public void DialogInput(bool newDialogState)
    {
        NPCLook = newDialogState;
    }

    public void OnLeftMouse(InputValue value)
    {
        LeftMouseInput(value.isPressed);
    }
    public void LeftMouseInput(bool newLeftMouseState)
    {
        LeftMouseClick = newLeftMouseState;
    }
    

}
