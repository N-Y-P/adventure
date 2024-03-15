using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestJumpManager : MonoBehaviour
{
    //�׽�Ʈ ���� �Ŵ����� ���� : � ������ �ҷ����� �����ϴ� ��ũ��Ʈ.

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

    [Header("�ҷ�����")]
    [SerializeField] CharacterController _controller;
    //[SerializeField] TestRange _testRange;
    [SerializeField] FlyRange _flyrange;
    [SerializeField] ClimbRange _climbrange;
    //��������
    //�⺻����
    TestPlayerMove _playermove;


    public void JumpStart()
    {
       
        switch (_playermove.Get_PlayerState())
        {
            default:
                Debug.Log("�Ҵ�� ������Ұ� �����ϴ�");
                break;
            case CharacterState.Normal:
                if (_controller.isGrounded)//���� ĳ���Ͱ� ���� ����ִٸ�
                {
                    _playermove.Normal_JumpInput(); // �Ϲ� ����
                }
                else if (!_controller.isGrounded && _flyrange.IsGroundInFlyRange()) {//���� ĳ���Ͱ� ���� ������� �ʴµ� ������ �Ѵٸ�
                    _playermove.Gliding_JumpInput();//Ȱ������!
                    _playermove.Set_PlayerState(CharacterState.Gliding); // ������¸� Ȱ�����·� �����
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
