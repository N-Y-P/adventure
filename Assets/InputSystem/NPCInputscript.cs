using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class NPCInputscript : MonoBehaviour
{

    [Header("NPC input")]
    public bool NPCLook; //f 누르면 npc와 대화 시작
    public bool LeftMouseClick;//왼쪽 마우스 클릭 시 대화 넘기기

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
