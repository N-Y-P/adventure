using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityManager : MonoBehaviour
{
    public static GravityManager instance;

    [SerializeField] TestPlayerMove _playermove;
    

    [Header("Gravity")]
    [SerializeField] private Vector2 gravity_range; // 중력 범위 설정 -28, -9.8
    [SerializeField] private float gravity_accel = 0.5f; // 초당 중력 가속도 증가량
    [SerializeField] float gravity_grounded = -4.9f;
    [Header("Gravity - 수치확인")]
    [SerializeField] private float gravity = -9.8f;

    [Header("글라이딩 계산")]
    [SerializeField] float gliding_pow = 0.3f;
    [SerializeField] float glidingOn_timespeed = 0.5f;
    [SerializeField] float glidingOff_timespeed = 0.01f;

    [Header("글라이딩 계산 - 수치확인용")]
    [SerializeField] float now_gliding_Pow;

    private float gravity_accel_time = 0f; // 중력 가속도 증가를 위한 시간 변수
    private void Awake()
    {
        instance = this;
    }


    public Vector3 Gravity()
    {
        if (_playermove._isJumping)// && !_playermove.isFalling)
        {
            //Debug.Log("일반점프 _ GM");
            // 점프 중일 때는 점프력을 중력 값으로 설정
            gravity = _playermove.jump_pow;
            _playermove._isJumping = false;//이 코드 없으면 추락 시 중력가속도 적용안됨

            gravity_accel_time = 0;
        }

        gravity -= gravity_accel * Time.deltaTime; // 점차 중력을 강하게 적용 | 1초에 -9.8만큼 아래에 힘을 균일하게 주고있다.

        //Debug.Log("일반점프 _ gravity : " + gravity);

        gravity = Mathf.Clamp(gravity, gravity_range.x, gravity_range.y);

        //Debug.Log("일반점프 _ gravity Clamp계산 : " + gravity);

        if (!_playermove.now_jumping && _playermove.Get_m_CC().isGrounded)
        {
            gravity = gravity_grounded;
        }

        return Vector3.up * gravity * GlidingPowCalculation();

    }

    //최종 중력값에 추가적으로 계산되는 활강 감속 정도치
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
