using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CPosition : MonoBehaviour
{
    [SerializeField] TestPlayerMove _playermove;
    [SerializeField] CharacterController _controller;
    [SerializeField] public Transform characterModel; // ĳ���� ���� Transform

    void Start()
    {
        _playermove = TestPlayerMove.instance;

    }
    void Ani_UpStart()
    {
        //ĳ���� �̵� Ű �Է� �ȵ�
        _playermove.DisableMovement();
    }
    void Ani_UpEnd()
    {
        _playermove.EnableMovement();
        //StartCoroutine(MoveModelToEndPosition());
    }
    // �ִϸ��̼� �̺�Ʈ �Ǵ� �ٸ� ������� ȣ��
    
    IEnumerator MoveModelToEndPosition()
    {
        float duration = 1f; // �̵��� �ɸ��� �ð�
        Vector3 startPosition = characterModel.position;
        Vector3 endPosition = _controller.transform.position; // ���� ��ǥ ��ġ

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            characterModel.position = Vector3.Lerp(startPosition, endPosition, t / duration);
            yield return null;
        }
        characterModel.position = endPosition; // ���� ��ġ Ȯ��
        _playermove.EnableMovement();
        
    }
}
