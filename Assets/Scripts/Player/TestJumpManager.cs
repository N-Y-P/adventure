using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJumpManager : MonoBehaviour
{
    //테스트 점프 매니저의 목적 : 어떤 점프를 불러올지 관리하는 스크립트.

    public static TestJumpManager instance;

    private void Awake()
    {
        instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        _playermove = TestPlayerMove.instance;
    }

    [Header("불러오기")]
    [SerializeField] CharacterController _controller;
    //[SerializeField] TestRange _testRange;
    [SerializeField] FlyRange _flyrange;
    [SerializeField] ClimbRange _climbrange;
    //점프종류
    //기본점프
    TestPlayerMove _playermove;


    public void JumpStart()
    {
       
        switch (_playermove.Get_PlayerState())
        {
            default:
                Debug.Log("할당된 점프요소가 없습니다");
                break;
            case CharacterState.Normal:
                if (_controller.isGrounded)//만약 캐릭터가 땅에 닿아있다면
                {
                    _playermove.Normal_JumpInput(); // 일반 점프
                }
                else if (!_controller.isGrounded && _flyrange.IsGroundInFlyRange()) {//만약 캐릭터가 땅에 닿아있지 않는데 점프를 한다면
                    _playermove.Gliding_JumpInput();//활강점프!
                    _playermove.Set_PlayerState(CharacterState.Gliding); // 현재상태를 활강상태로 만들기
                }
                break;
            case CharacterState.Gliding:
                if(!_controller.isGrounded && _flyrange.IsGroundInFlyRange())
                {
                    _playermove.Gliding_JumpInput2();
                }
                break;
            case CharacterState.Climbing:
                //
                break;

        }
    }

}
