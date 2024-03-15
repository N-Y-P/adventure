using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClimbTopRange : MonoBehaviour
{
    public static ClimbTopRange instance;
    TestPlayerMove _playermove;

    [Header("벽 등반 완료")]
    public float rayLength = 1.0f; // Raycast 길이
    public Vector3 ClimbTopOffset;
    public Color ClimbTopColor = Color.red; // Raycast 색상

    [Header("기즈모 표시 설정")]
    public bool showClimbTopGizmos = true;

    private void Awake()
    {
        instance = this;
    }
    void Start()
    {
        _playermove = TestPlayerMove.instance;

    }
    void OnDrawGizmos()//기즈모 보이게하기
    {
        // 플레이어의 전방을 향하는 방향과 오프셋을 결합하여 최종 위치 계산
        Vector3 offsetPosition = PublicRangePosition.CalculateOffsetPosition(transform, ClimbTopOffset);

        if (showClimbTopGizmos)
        {
            Gizmos.color = ClimbTopColor;
            Gizmos.DrawRay(offsetPosition, transform.forward * rayLength); // rayLength는 Ray의 길이
        }
    }
    public bool IsClimbTop()
    {
        RaycastHit hit;
        // Raycast 시작점 설정 (캐릭터의 위치)
        Vector3 start = PublicRangePosition.CalculateOffsetPosition(transform, ClimbTopOffset);
        // Raycast 방향 설정 (캐릭터의 전방)
        Vector3 direction = transform.forward;

        // Scene 뷰에서 Raycast 시각화
        Debug.DrawRay(start, direction * rayLength, ClimbTopColor);

        // Raycast 실행
        if (!Physics.Raycast(start, direction, out hit, rayLength))
        {

            return true;
        }
        // 벽과 충돌하지 않았으면 false 반환
        return false;
    }
}
