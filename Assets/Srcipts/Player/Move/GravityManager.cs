using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager instance;

    [SerializeField] TestPlayerMove _playermove;
    

    [Header("Gravity")]
    [SerializeField] private Vector2 gravity_range; // �߷� ���� ���� -28, -9.8
    [SerializeField] private float gravity_accel = 0.5f; // �ʴ� �߷� ���ӵ� ������
    [SerializeField] float gravity_grounded = -4.9f;
    [Header("Gravity - ��ġȮ��")]
    [SerializeField] private float gravity = -9.8f;

    [Header("�۶��̵� ���")]
    [SerializeField] float gliding_pow = 0.3f;
    [SerializeField] float glidingOn_timespeed = 0.5f;
    [SerializeField] float glidingOff_timespeed = 0.01f;

    [Header("�۶��̵� ��� - ��ġȮ�ο�")]
    [SerializeField] float now_gliding_Pow;

    private float gravity_accel_time = 0f; // �߷� ���ӵ� ������ ���� �ð� ����
    private void Awake()
    {
        instance = this;
    }


    public Vector3 Gravity()
    {
        if (_playermove._isJumping)// && !_playermove.isFalling)
        {
            //Debug.Log("�Ϲ����� _ GM");
            // ���� ���� ���� �������� �߷� ������ ����
            gravity = _playermove.jump_pow;
            _playermove._isJumping = false;//�� �ڵ� ������ �߶� �� �߷°��ӵ� ����ȵ�

            gravity_accel_time = 0;
        }

        gravity -= gravity_accel * Time.deltaTime; // ���� �߷��� ���ϰ� ���� | 1�ʿ� -9.8��ŭ �Ʒ��� ���� �����ϰ� �ְ��ִ�.

        //Debug.Log("�Ϲ����� _ gravity : " + gravity);

        gravity = Mathf.Clamp(gravity, gravity_range.x, gravity_range.y);

        //Debug.Log("�Ϲ����� _ gravity Clamp��� : " + gravity);

        if (!_playermove.now_jumping && _playermove.Get_m_CC().isGrounded)
        {
            gravity = gravity_grounded;
        }

        return Vector3.up * gravity * GlidingPowCalculation();

    }

    //���� �߷°��� �߰������� ���Ǵ� Ȱ�� ���� ����ġ
    float GlidingPowCalculation()
    {
        if (_playermove.Get_PlayerState() == CharacterState.Gliding)
        {
            now_gliding_Pow = Mathf.Lerp(now_gliding_Pow, 0.2f, glidingOn_timespeed);
        }
        else
        {
            now_gliding_Pow = Mathf.Lerp(now_gliding_Pow, 1f, glidingOff_timespeed);
        }

        return now_gliding_Pow;
    }

    public void GlidingOff()
    {
        gravity *= GlidingPowCalculation();
    }

}
