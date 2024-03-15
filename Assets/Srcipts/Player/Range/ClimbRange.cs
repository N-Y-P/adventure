using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Windows;

public class ClimbRange : MonoBehaviour
{
    //인스턴스
    StarterAssetsInputs m_input;


    public static ClimbRange instance;
    TestPlayerMove _playermove;//스크립트 가져오기

    [Header("벽 감지")]
    public float Climbray = 1f; //벽 감지 길이 
    public Vector3 ClimbRangeOffset; //중심 위치 조절
    public Color ClimbRangeColor = Color.green;//범위 색상 : 초록색

    [Header("벽 레이어")]
    [SerializeField] LayerMask WallLayer;

    [Header("기즈모 표시 설정")]
    public bool showClimbGizmos = true; // 벽 감지 범위 기즈모

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        //인스턴스
        m_input = StarterAssetsInputs.instance;

        _playermove = TestPlayerMove.instance;
    }



    private void Update()
    {
        if (_playermove.Get_PlayerState() == CharacterState.Climbing)
        {
            input_time = 0;
            return;
        }

        if (IsClimbRange()) // 벽 감지
        {
            if (InputForward())
                _playermove.ClimbingStart();

            /*
             * 
            if(InputForward())
                _playermove.Set_PlayerState(CharacterState.Climbing);
            */
        }
        else
        {
            input_time = 0;
        }
    }

    //앞으로 입력값

    [Header("앞으로 입력값 주기")]

    [SerializeField] float input_time_set = 0.2f;

    [Header("앞으로 입력값 주기 - 수치확인")]

    [SerializeField] float input_time;


    bool InputForward()
    {
        if (m_input.move.y > 0.5)
        {
            input_time += Time.deltaTime;
        }
        else
        {
            input_time = 0;
        }

        if (input_time > input_time_set)
            return true;
        else
            return false;
    }


    void OnDrawGizmos()//기즈모 보이게하기
    {
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, ClimbRangeOffset);

        // 벽 감지 범위 기즈모
        if (showClimbGizmos)
        {
            Gizmos.color = ClimbRangeColor;
            Gizmos.DrawRay(offsetPosition, transform.forward * Climbray);
        }
    }

    public bool IsClimbRange()
    {
        RaycastHit hit;
        // Raycast 시작점 설정 (캐릭터의 위치)
        Vector3 start = PublicRangePosition.CalculateOffsetPosition(transform, ClimbRangeOffset);
        // Raycast 방향 설정 (캐릭터의 전방)
        Vector3 direction = transform.forward;

        // Scene 뷰에서 Raycast 시각화
        Debug.DrawRay(start, direction * Climbray, ClimbRangeColor);

        // Raycast 실행
        if (Physics.Raycast(start, direction, out hit, Climbray))
        {
            // Raycast가 벽과 충돌했는지 확인
            if (hit.collider.CompareTag("Wall"))
            {
                // 벽과 충돌했으면 true 반환
                return true;
            }
        }
        // 벽과 충돌하지 않았으면 false 반환
        return false;
    }
}
