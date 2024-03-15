using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.XR;

public class CPosition : MonoBehaviour
{
    [SerializeField] TestPlayerMove _playermove;
    [SerializeField] CharacterController _controller;
    [SerializeField] public Transform characterModel; // 캐릭터 모델의 Transform

    void Start()
    {
        _playermove = TestPlayerMove.instance;

    }
    void Ani_UpStart()
    {
        //캐릭터 이동 키 입력 안됨
        _playermove.DisableMovement();
    }
    void Ani_UpEnd()
    {
        _playermove.EnableMovement();
        //StartCoroutine(MoveModelToEndPosition());
    }
    // 애니메이션 이벤트 또는 다른 방법으로 호출
    
    IEnumerator MoveModelToEndPosition()
    {
        float duration = 1f; // 이동에 걸리는 시간
        Vector3 startPosition = characterModel.position;
        Vector3 endPosition = _controller.transform.position; // 최종 목표 위치

        for (float t = 0; t < duration; t += Time.deltaTime)
        {
            characterModel.position = Vector3.Lerp(startPosition, endPosition, t / duration);
            yield return null;
        }
        characterModel.position = endPosition; // 최종 위치 확정
        _playermove.EnableMovement();
        
    }
}
